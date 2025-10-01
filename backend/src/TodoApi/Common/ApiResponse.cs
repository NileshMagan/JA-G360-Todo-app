namespace TodoApi.Common;

public class ApiResponse<T>
{
    public T? Data { get; set; }
    public bool Success { get; set; }
    public string? Message { get; set; }
    public Dictionary<string, string[]>? Errors { get; set; }
    public IDictionary<string, object>? Metadata { get; set; }

    public static ApiResponse<T> Ok(T data, IDictionary<string, object>? metadata = null) =>
        new() { Success = true, Data = data, Metadata = metadata };

    public static ApiResponse<T> Fail(string message, Dictionary<string, string[]>? errors = null) =>
        new() { Success = false, Message = message, Errors = errors };
}
