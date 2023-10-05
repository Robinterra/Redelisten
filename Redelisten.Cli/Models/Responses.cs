using ApiService;

public class ApiResponse : IApiResponse
{
    public Exception? Exception;

    public int HttpCode;

    public bool IsSuccess;

    public string? Status
    {
        get;
        set;
    }

    public string? Name
    {
        get;
        set;
    }

    public bool SetException(Exception exception)
    {
        this.Exception = exception;

        return true;
    }

    public bool SetHttpCode(int httpCode)
    {
        this.HttpCode = httpCode;
        return true;
    }

    public bool SetIsSuccess(bool Success)
    {
        this.IsSuccess = Success;
        return true;
    }

    public bool SetStatus(string status)
    {
        this.Status = status;
        return true;
    }
}