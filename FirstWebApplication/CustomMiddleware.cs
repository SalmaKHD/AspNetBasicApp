namespace FirstWebApplication.CustomMiddleware
{
    public class CustomMiddleware: IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("Hello ");
            await next(context);
            await context.Response.WriteAsync("coming back");
        }
    }
}
