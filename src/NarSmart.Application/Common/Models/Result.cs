namespace NarSmart.Application.Common.Models;

public class Result<T>
{
    public bool IsSuccess { get; private set; }
    public T? Data { get; private set; }
    public List<string> Errors { get; private set; } = new();
    public int StatusCode { get; private set; }

    private Result() { }

    public static Result<T> Success(T data, int statusCode = 200)
    {
        return new Result<T>
        {
            IsSuccess = true,
            Data = data,
            StatusCode = statusCode
        };
    }

    public static Result<T> Failure(List<string> errors, int statusCode = 400)
    {
        return new Result<T>
        {
            IsSuccess = false,
            Errors = errors,
            StatusCode = statusCode
        };
    }

    public static Result<T> Failure(string error, int statusCode = 400)
    {
        return new Result<T>
        {
            IsSuccess = false,
            Errors = new List<string> { error },
            StatusCode = statusCode
        };
    }

    public static Result<T> NotFound(string error)
    {
        return Failure(error, 404);
    }

    public static Result<T> Created(T data)
    {
        return Success(data, 201);
    }
}
