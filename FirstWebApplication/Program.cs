using FirstWebApplication;
using FirstWebApplication.CustomMiddleware;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args); // creates a builder that confgures initial set up for a web app
var app = builder.Build(); // creates an instance of a web app
// add a custom middleware
builder.Services.AddTransient<CustomMiddleware>();

// create a middleware
app.Use(async (HttpContext context, RequestDelegate next) =>
{

    await context.Response.WriteAsync("Hello ");
    await next(context); // pass to next middleware
    await context.Response.WriteAsync("coming back");
});

// executed for every single request -> a way to create a middleware
// Run() does not forward requests to any other middlewares
app.Run(async (HttpContext context) =>
{   // terminating middleware
    // add a status code to response
    context.Response.StatusCode = 400;
    // add headers
    context.Response.Headers["MyKey"] = "Salma";
    // get info about the request
    Util.printValue(context.Request.Path);
    Util.printValue(context.Request.Method);

    if(context.Request.Method == "GET")
    {
        if(context.Request.Query.ContainsKey("id"))
        {
            Util.printValue(context.Request.Query["id"]);
        }
    }

    // check for requests headers
    if(context.Request.Headers.ContainsKey("User-Agent"))
    {
        Util.printValue(context.Request.Headers["User-Agent"]);
    }

    // read body
    StreamReader sr = new StreamReader(context.Request.Body);
    Util.printValue(await sr.ReadToEndAsync());

    await context.Response.WriteAsync("Hello");
});

app.Run();