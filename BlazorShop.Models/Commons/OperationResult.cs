namespace BlazorShop.Models.Commons;

public class OperationResult
{
    public bool Success { get; protected init; }
    public string Message { get; protected init; } = string.Empty;

    public static OperationResult Ok(string message = "") => new() { Success = true, Message = message };
    public static OperationResult Fail(string message) => new() { Success = false, Message = message };
}

public sealed class OperationResult<T> : OperationResult
{
    public T? Value { get; private init; }

    public static OperationResult<T> Ok(T value, string message = "") =>
        new() { Success = true, Message = message, Value = value };

    public static new OperationResult<T> Fail(string message) =>
        new() { Success = false, Message = message };
}
