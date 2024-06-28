using Microsoft.AspNetCore.Mvc;

namespace EMS.API.Helpers;

public class ResponseHelper
{
    public static IActionResult WrapResponse(int statusCode, string status, object? data, string? errorCode = null)
    {
        var response = new
        {
            status,
            data,
            errorCode
        };
        return new JsonResult(response)
        {
            StatusCode = statusCode
        };
    }
}
