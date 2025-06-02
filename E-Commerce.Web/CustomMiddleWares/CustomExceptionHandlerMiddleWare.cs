using System.Text.Json;
using DomainLayer.Exceptions;
using Shared.ErrorModels;

namespace E_Commerce.Web.CustomMiddleWares
{
    public class CustomExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate  _next;
        private readonly ILogger<CustomExceptionHandlerMiddleWare> _logger;

        public CustomExceptionHandlerMiddleWare(RequestDelegate Next , ILogger<CustomExceptionHandlerMiddleWare> logger) 
        {
            _next = Next;
            this._logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
               await _next.Invoke(context: context);
            }
            catch (Exception ex)
            {
                _logger.LogError(exception: ex, message: "Something went wrong");


                //// Set status code for response in header
                //context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                context.Response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError
                };

                //// Set content type  for response in header
                //context.Response.ContentType = "application/json";

                // create Response object 

                var Response = new ErrorToReturn()
                {
                    StatusCode = context.Response.StatusCode,
                    ErrorMessage = ex.Message
                };

                //// Return the response object as json

                // var ResponseToReturn =  JsonSerializer.Serialize(Response);
                // await   context.Response.WriteAsync(ResponseToReturn);

                // This syntax makes the previous syntax of serializing the object to json and write it in the Response and set  the content type of response in header
               
                await context.Response.WriteAsJsonAsync(Response);

            }

        }
    }
}
