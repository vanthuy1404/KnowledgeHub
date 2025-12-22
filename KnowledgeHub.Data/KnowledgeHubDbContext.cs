using KnowledgeHub.Data.Entities;
using KnowledgeHub.Data.Entities.Auth;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeHub.Data;

public class KnowledgeHubDbContext : DbContext
{
    public KnowledgeHubDbContext(DbContextOptions<KnowledgeHubDbContext> options) : base(options) {}

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }

    public DbSet<Document> Documents { get; set; }
    public DbSet<DocumentVersion> DocumentVersions { get; set; }
    public DbSet<DocumentChunk> DocumentChunks { get; set; }
    public DbSet<DocumentEmbedding> DocumentEmbeddings { get; set; }

    public DbSet<SearchQuery> SearchQueries { get; set; }
    public DbSet<SearchResult> SearchResults { get; set; }

    public DbSet<ChatSession> ChatSessions { get; set; }
    public DbSet<ChatMessage> ChatMessages { get; set; }
    public DbSet<ChatMessageSource> ChatMessageSources { get; set; }

    public DbSet<EventLog> EventLogs { get; set; }
    public DbSet<UserActivity> UserActivities { get; set; }

    public DbSet<SystemSetting> SystemSettings { get; set; }
    public DbSet<BackgroundJob> BackgroundJobs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserRole>().HasKey(x => new { x.UserId, x.RoleId });
        modelBuilder.Entity<ChatMessageSource>().HasKey(x => new { x.MessageId, x.ChunkId });

        modelBuilder.Entity<User>().HasIndex(x => x.Email).IsUnique();
        modelBuilder.Entity<User>().HasIndex(x => x.UserName).IsUnique();
        modelBuilder.Entity<Role>().HasIndex(x => x.Name).IsUnique();

        modelBuilder.Entity<DocumentVersion>().HasIndex(x => new { x.DocumentId, x.VersionNumber }).IsUnique();
        modelBuilder.Entity<DocumentChunk>().HasIndex(x => new { x.DocumentId, x.VersionNumber, x.ChunkIndex }).IsUnique();

        modelBuilder.Entity<Document>()
            .HasOne(x => x.Creator)
            .WithMany(x => x.Documents)
            .HasForeignKey(x => x.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<DocumentVersion>()
            .HasOne(x => x.Document)
            .WithMany(x => x.Versions)
            .HasForeignKey(x => x.DocumentId);

        modelBuilder.Entity<DocumentVersion>()
            .HasOne(x => x.Creator)
            .WithMany()
            .HasForeignKey(x => x.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<DocumentChunk>()
            .HasOne(x => x.Document)
            .WithMany(x => x.Chunks)
            .HasForeignKey(x => x.DocumentId);

        modelBuilder.Entity<DocumentEmbedding>()
            .HasOne(x => x.Chunk)
            .WithOne(x => x.Embedding)
            .HasForeignKey<DocumentEmbedding>(x => x.ChunkId);

        modelBuilder.Entity<SearchQuery>()
            .HasOne(x => x.User)
            .WithMany(x => x.SearchQueries)
            .HasForeignKey(x => x.UserId);

        modelBuilder.Entity<SearchResult>()
            .HasOne(x => x.SearchQuery)
            .WithMany(x => x.Results)
            .HasForeignKey(x => x.SearchQueryId);

        modelBuilder.Entity<SearchResult>()
            .HasOne(x => x.Chunk)
            .WithMany(x => x.SearchResults)
            .HasForeignKey(x => x.ChunkId);

        modelBuilder.Entity<ChatSession>()
            .HasOne(x => x.User)
            .WithMany(x => x.ChatSessions)
            .HasForeignKey(x => x.UserId);

        modelBuilder.Entity<ChatMessage>()
            .HasOne(x => x.Session)
            .WithMany(x => x.Messages)
            .HasForeignKey(x => x.SessionId);

        modelBuilder.Entity<ChatMessageSource>()
            .HasOne(x => x.Message)
            .WithMany(x => x.Sources)
            .HasForeignKey(x => x.MessageId);

        modelBuilder.Entity<ChatMessageSource>()
            .HasOne(x => x.Chunk)
            .WithMany(x => x.ChatMessageSources)
            .HasForeignKey(x => x.ChunkId);

        modelBuilder.Entity<EventLog>()
            .HasOne(x => x.Creator)
            .WithMany(x => x.EventLogs)
            .HasForeignKey(x => x.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<UserActivity>()
            .HasOne(x => x.User)
            .WithMany(x => x.UserActivities)
            .HasForeignKey(x => x.UserId);

        modelBuilder.Entity<SystemSetting>().HasKey(x => x.Key);
    }

    public override int SaveChanges()
    {
        ApplyAudit();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyAudit();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void ApplyAudit()
    {
        foreach (var e in ChangeTracker.Entries<BaseEntity>())
        {
            if (e.State == EntityState.Added) { e.Entity.CreatedAt = DateTime.UtcNow; }
            if (e.State == EntityState.Modified) { e.Entity.UpdatedAt = DateTime.UtcNow; }
        }
    }
}
