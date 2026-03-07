using Entities;
using FirstWebApplication;
using FirstWebApplication.Filters.ActionFilters;
using FirstWebApplication.Middleware;
using FirstWebApplication.MonthCustomConstraint;
using FirstWebApplication.Properties;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions;
using ServiceContracts;
using Services;
using System;

var builder = WebApplication.CreateBuilder(args); // creates a builder that confgures initial set up for a web app
// method 1 add a custom middleware
builder.Services.AddTransient<CustomMiddleware>();
builder.Services.AddControllersWithViews(options =>
{
    var logger = builder.Services.BuildServiceProvider().GetRequiredService<ILogger<GlobalActionFilter>>();

    options.Filters.Add(new GlobalActionFilter(logger, "Global-Filter-Key", "value", 0));
});

// add services
builder.Services.Add(new ServiceDescriptor(
    typeof(IUsersService),
    typeof(UsersService),
    /**
     * Transient: an object will be created every time, destructed at the end of browser request -> for every injection
     * scoped: once the browser requests till the end
     * singleton: for entire app runtime
     */
    ServiceLifetime.Scoped) // lifetime of service
    ); // inversion of control -> when IUsersService is asked, provide UsersService object
/*
 * alternatice way
 * builder.Services.AddTransient<IUsersService, UsersService>();
 */

builder.Services.AddScoped<IStockService, StockService>();

builder.Services.Add(new ServiceDescriptor(
    typeof(ICountriesService),
    typeof(CountriesService),
    ServiceLifetime.Singleton)
    );

// add config options to IOC
builder.Services.Configure<ApiConfigOptions>(builder.Configuration.GetSection("ApiConfig"));

// add seperate settings file
builder.Host.ConfigureAppConfiguration((hostingContek0xt, config) =>
{
    config.AddJsonFile("apiconfigsettings.json", optional: true, reloadOnChange: true);
});
// add HttpClient service
builder.Services.AddHttpClient();

// add a custom constraint
builder.Services.AddRouting(options =>
{
    options.ConstraintMap.Add("months", typeof(MonthCustomConstraint));
});

// add controllers
builder.Services.AddControllers();

// add db config
//builder.Services.AddDbContext<CountriesDbContext>(OptionsBuilderConfigurationExtensions =>
//{
//    OptionsBuilderConfigurationExtensions.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]); // choose server to use
//});

// add log providers
builder.Host.ConfigureLogging(logProvider =>
{
    logProvider.ClearProviders();
    logProvider.AddDebug(); // print in debug only
});

// add Http logging
builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestProperties |
    Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponsePropertiesAndHeaders;
});

// enable CORS policy for all requests
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        // only allow requests coming from this domain to access server
        policyBuilder
        .WithOrigins(builder.Configuration.GetSection("AllowedOrigins").Get<string[]>())
        .WithHeaders("Authorization", "origin", "accept", "content-type"); // add allowed headers
    });
});

var app = builder.Build(); // creates an instance of a web app

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandlingMiddleware();
    app.UseExceptionHandler("/Error");
}

// force clients to use https
app.UseHsts();

// add controller mappings
app.MapControllers();

// add static files middleware
app.UseStaticFiles();

app.UseMiddleware<CustomMiddleware>();

// method 2 add a custom middleware
app.Use(async (HttpContext context, RequestDelegate next) =>
{

    Util.printValue("From Middleware: Hello from middl:['eware 2 ");
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

// add CORS
app.UseCors();

app.UseEndpoints(endpoints =>
{
    // an endpoint
    endpoints.MapGet("/hello", (HttpContext context) =>
    context.Response.WriteAsync("Hello Salma")
    );
});

// add a route parameter
// add a default value for route param, add an optional param, add param constraint
app.MapGet("/salma/{lastname=Khodaei}/{age:int?}/{date-of-birth:months}", (HttpContext context) =>
{
    context.Response.WriteAsync($"Hello Salma {context.Request.RouteValues["lastname"] ?? ""}");
    if (context.Request.RouteValues.ContainsKey("date-of-borth"))
    {
        context.Response.WriteAsync($"Month is: {context.Request.RouteValues["date-of-borth"]}");
    }
    /**
     * output
     * Hello Salma khodaei
     */
});

// add logs
app.Logger.LogDebug("LOGGING DEBUG...");
app.Logger.LogInformation("LOGGING INFORMATION...");
app.Logger.LogError("LOGGING ERROR...");
app.Logger.LogCritical("LOGGING CRITICAL...");
app.Logger.LogWarning("LOGGING WARNING...");

app.Run();