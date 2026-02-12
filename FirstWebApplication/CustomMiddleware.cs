namespace FirstWebApplication.CustomMiddleware
{
    public class CustomMiddleware: IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Util.printValue("From Middleware: Hello from middleware 1 ");
            await next(context);
            Util.printValue("From Middleware: coming back from middleware 1 ");
        }
    }

    public static class CustomMiddleWareConfig
    {
        public static IApplicationBuilder ConfigureCustomMiddlewares(this IApplicationBuilder app)
        {
            return app.Use(async (HttpContext context, RequestDelegate next) =>
            {

                Util.printValue("From Middleware: Hello middlewre 3 ");
                await next(context); // pass to next middleware
                Util.printValue("From Middleware: coming back from middleware 3 ");
            });
        }
    }
}
