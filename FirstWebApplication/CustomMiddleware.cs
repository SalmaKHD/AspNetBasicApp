namespace FirstWebApplication.CustomMiddleware
{
    public class CustomMiddleware: IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("Hello from middleware 1");
            await next(context);
            await context.Response.WriteAsync("coming back from middleware 1");
        }
    }

    public static class CustomMiddleWareConfig
    {
        public static IApplicationBuilder ConfigureCustomMiddlewares(this IApplicationBuilder app)
        {
            return app.Use(async (HttpContext context, RequestDelegate next) =>
            {

                await context.Response.WriteAsync("Hello middlewre 3");
                await next(context); // pass to next middleware
                await context.Response.WriteAsync("coming back from middleware 3");
            });
        }
    }
}
