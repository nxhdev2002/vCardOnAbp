namespace VCardOnAbp.Models;

public class ResponseModel
{
    public string Message { get; set; }
    public object? Data { get; set; }
    public bool Success { get; set; }
    public ResponseModel(string message, object? data, bool success)
    {
        Message = message;
        Data = data;
        Success = success;
    }

    // static
    public static ResponseModel SuccessResponse(string message, object? data = null)
    {
        return new ResponseModel(message, data, true);
    }

    public static ResponseModel ErrorResponse(string message, object? data = null)
    {
        return new ResponseModel(message, data, false);
    }

    public static ResponseModel ErrorResponse(string message)
    {
        return new ResponseModel(message, null, false);
    }
}
