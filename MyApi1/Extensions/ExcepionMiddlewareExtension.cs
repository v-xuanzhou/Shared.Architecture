using UserApi.Msic.ExceptionHandlerMiddleware;

namespace UserApi.Extensions
{
    public static class ExcepionMiddlewareExtension
    {
        public static void UseMyExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<MyExceptonMiddleware>();
        }
    }
}   
