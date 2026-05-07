namespace KazanRealEstate.Api.Common;

public class Result<T>
{
    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }
    public int StatusCode { get; set; }
    public T? Data { get; set; }

    public static Result<T> Success(T data, int statusCode = 200) => new()
    {
        IsSuccess = true,
        Data = data,
        StatusCode = statusCode
    };

    public static Result<T> Failure(string errorMessage, int statusCode = 400) => new()
    {
        IsSuccess = false,
        ErrorMessage = errorMessage,
        StatusCode = statusCode
    };
}

public class Result : Result<object>
{
    public static Result Success(int statusCode = 200) => new()
    {
        IsSuccess = true,
        StatusCode = statusCode
    };

    public new static Result Failure(string errorMessage, int statusCode = 400) => new()
    {
        IsSuccess = false,
        ErrorMessage = errorMessage,
        StatusCode = statusCode
    };
}
