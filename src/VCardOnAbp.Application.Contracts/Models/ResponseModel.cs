namespace VCardOnAbp.Models;

public class ResponseModel<T>
{
    public string Message { get; set; }
    public T Data { get; set; }
    public bool Success { get; set; }
    public ResponseModel(string message, T data, bool success)
    {
        Message = message;
        Data = data;
        Success = success;
    }

    // static
    public static ResponseModel<T> SuccessResponse(string message, T data)
    {
        return new ResponseModel<T>(message, data, true);
    }

    public static ResponseModel<T> ErrorResponse(string message, T data)
    {
        return new ResponseModel<T>(message, data, false);
    }

    public static ResponseModel<object> ErrorResponse(string message)
    {
        return new ResponseModel<object>(message, null, false);
    }
}
