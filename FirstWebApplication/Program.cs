var builder = WebApplication.CreateBuilder(args); // creates a builder that confgures initial set up for a web app
var app = builder.Build(); // creates an instance of a web app

app.MapGet("/", () => "Hello World!"); // when getting a request to root, return "Hello World!"

app.Run();
