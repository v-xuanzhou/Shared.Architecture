using UserApi.Extensions;
using UserApi.Msic.CustomerException;

namespace UserApi.Msic.ExceptionHandlerMiddleware
{
    public class MyExceptonMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        public MyExceptonMiddleware(RequestDelegate next)
        {
            _requestDelegate = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (MyException ex)
            {
                string message = ex.Message;
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json; charset=utf-8";
                await context.Response.WriteAsync(new { message, Status = false }.Serialize());
            }
        }
    }
}
