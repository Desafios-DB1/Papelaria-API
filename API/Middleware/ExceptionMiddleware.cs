using System.Net;
using System.Text.Json;
using Crosscutting.Erros;
using Crosscutting.Exceptions;
using Microsoft.Data.SqlClient;

namespace Papelaria.API.Middleware;

public class ExceptionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = exception switch
        {
            NaoEncontradoException => HttpStatusCode.NotFound,
            RegraDeNegocioException => HttpStatusCode.UnprocessableEntity,
            SqlException => HttpStatusCode.InternalServerError,
            Exception => HttpStatusCode.BadRequest
        };

        var response = new ErrorResponse
        {
            Message = exception.Message,
            Status = (int)statusCode,
            Error = exception.GetType().Name
        };
        
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;
        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}