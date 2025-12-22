// 21/12/2025 - 16:56:28
// DANGTHUY

namespace KnowledgeHub.Models.Base;

public class BaseResponse<T>
{
    public bool Success { get; init; }
    public string Code { get; init; } = ResponseCode.Success;
    public string? Message { get; init; }
    public T? Data { get; init; }

    public static BaseResponse<T> Ok(T data, string? message = null)
    {
        return new BaseResponse<T>
        {
            Success = true,
            Code = ResponseCode.Success,
            Message = message,
            Data = data
        };
    }

    public static BaseResponse<T> Fail(string code, string message)
    {
        return new BaseResponse<T>
        {
            Success = false,
            Code = code,
            Message = message,
            Data = default
        };
    }
}
