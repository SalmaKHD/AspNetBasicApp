using FirstWebApplication;
using FirstWebApplication.CustomMiddleware;
using FirstWebApplication.MiddleWare;
using Microsoft.AspNetCore.Builder;
using System;

var builder = WebApplication.CreateBuilder(args); // creates a builder that confgures initial set up for a web app
// method 1 add a custom middleware
builder.Services.AddTransient<CustomMiddleware>();

var app = builder.Build(); // creates an instance of a web app

app.UseMiddleware<CustomMiddleware>();

// method 2 add a custom middleware
app.Use(async (HttpContext context, RequestDelegate next) =>
{

    Util.printValue("From Middleware: Hello from middleware 2 ");
    await next(context); // pass to next middleware
    Util.printValue("From Middleware: coming back from middleware 2 ");
});

// method 3 add a custom middleware
app.ConfigureCustomMiddlewares();

// method 4 add a custom middleware
app.UseMiddleware<Middleware>();

// method 5 add a custom middleware: executed only under ceratin conditions
app.UseWhen(context => context.Request.Method == "GET",
    app =>
    {
        app.Use((async (HttpContext context, RequestDelegate next) =>
        {

            Util.printValue("From Middleware: Hello from middleware 5 ");
            await next(context); // pass to next middleware
            Util.printValue("From Middleware: coming back from middleware 5 ");
        }));
    });

// method 6 add a custom middleware
// executed for every single request -> a way to create a middleware
// Run() does not forward requests to any other middlewares
//app.Run(async (HttpContext context) =>
//{   // terminating middleware, endpoints won't work if this set

//    // endpoint returns info endpoint -> if run before routing middleware -> null will be returned
//    Util.printValue($"Display name is: {context.GetEndpoint()?.DisplayName ?? ""}");

//    await context.Response.WriteAsync(" Hello from middleware 6 ");
//});

app.MapGet("/salma", async (HttpContext context) =>
{
    // add a status code to response
    context.Response.StatusCode = 400;
    // add headers
    context.Response.Headers["MyKey"] = "Salma";
    // get info about the request
    Util.printValue(context.Request.Path);
    Util.printValue(context.Request.Method);

    if (context.Request.Method == "GET")
    {
        if (context.Request.Query.ContainsKey("id"))
        {
            Util.printValue(context.Request.Query["id"]);
        }
    }

    // check for requests headers
    if (context.Request.Headers.ContainsKey("User-Agent"))
    {
        Util.printValue(context.Request.Headers["User-Agent"]);
    }

    // read body
    StreamReader sr = new StreamReader(context.Request.Body);
    Util.printValue(await sr.ReadToEndAsync());

    /**
     * output
     *  Hello from middleware 1  Hello from middleware 2  Hello middlewre 3  Hello from middleware 4  Hello from middleware 5  Hello from middleware 6  coming back from middleware 5  coming back from middleware 4  coming back from middleware 3  coming back from middleware 2  coming back from middleware 1 Hello Salma
     */

    await context.Response.WriteAsync("Hello Salma");
}); // when getting a request to root, return "Hello World!"

app.UseRouting(); // enable routing middleware
app.UseEndpoints(endpoints =>
{
    // an endpoint
    endpoints.MapGet("/hello", (HttpContext context) => context.Response.WriteAsync("Hello Salma"));
});


app.Run();