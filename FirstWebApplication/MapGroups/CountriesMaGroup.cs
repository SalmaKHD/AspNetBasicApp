using Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FirstWebApplication.MapGroups
{
    public static class CountriesMaGroup
    {
        public static RouteGroupBuilder CountriesApi(this RouteGroupBuilder builder)
        {
            List<Country> countries = new List<Country> {
            new Country("Brazil"),
            new Country("Canada")
            };

            builder.MapGet("/", async (HttpContext context) =>
            {
                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(countries));
            });


            builder.MapPost("/", async (HttpContext context, string name) =>
            {
                countries.Add(new Country(name: name));

                return Results.Ok("Add successful");
            });

            builder.MapGet("/{id:string}", async (HttpContext context, string id) =>
            {
                Country country = countries.First(country => country.CountryID == Guid.Parse(id));
                if (country != null)
                {
                    await context.Response.WriteAsync(
                                JsonSerializer.Serialize(countries));
                }
                else
                {
                    return Results.BadRequest(new
                    {
                        Message = "No countries found."
                    });
                }

            });

            return builder;
        }
    }
}
using Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FirstWebApplication.MapGroups
{
    public static class CountriesMaGroup
    {
        public static RouteGroupBuilder CountriesApi(this RouteGroupBuilder builder)
        {
            List<Country> countries = new List<Country> {
            new Country("Brazil"),
            new Country("Canada")
            };

            builder.MapGet("/", async (HttpContext context) =>
            {
                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(countries));
            });


            builder.MapPost("/", async (HttpContext context, string name) =>
            {
                countries.Add(new Country(name: name));

                return Results.Ok("Add successful");
            });

            builder.MapGet("/{id:string}", async (HttpContext context, string id) =>
            {
                Country country = countries.First(country => country.CountryID == Guid.Parse(id));
                if (country != null)
                {
                    await context.Response.WriteAsync(
                                JsonSerializer.Serialize(countries));
                }
                else
                {
                    return Results.BadRequest(new
                    {
                        Message = "No countries found."
                    });
                }

            });

            return builder;
        }
    }
}
using Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FirstWebApplication.MapGroups
{
    public static class CountriesMaGroup
    {
        public static RouteGroupBuilder CountriesApi(this RouteGroupBuilder builder)
        {
            List<Country> countries = new List<Country> {
            new Country("Brazil"),
            new Country("Canada")
            };

            builder.MapGet("/", async (HttpContext context) =>
            {
                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(countries));
            });


            builder.MapPost("/", async (HttpContext context, string name) =>
            {
                countries.Add(new Country(name: name));

                return Results.Ok("Add successful");
            });

            builder.MapGet("/{id:string}", async (HttpContext context, string id) =>
            {
                Country country = countries.First(country => country.CountryID == Guid.Parse(id));
                if (country != null)
                {
                    await context.Response.WriteAsync(
                                JsonSerializer.Serialize(countries));
                }
                else
                {
                    return Results.BadRequest(new
                    {
                        Message = "No countries found."
                    });
                }

            });

            return builder;
        }
    }
}
using Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FirstWebApplication.MapGroups
{
    public static class CountriesMaGroup
    {
        public static RouteGroupBuilder CountriesApi(this RouteGroupBuilder builder)
        {
            List<Country> countries = new List<Country> {
            new Country("Brazil"),
            new Country("Canada")
            };

            builder.MapGet("/", async (HttpContext context) =>
            {
                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(countries));
            });


            builder.MapPost("/", async (HttpContext context, string name) =>
            {
                countries.Add(new Country(name: name));

                // string format
                return Results.Ok("Add successful"); // can be string or object
            });

            builder.MapGet("/{id:string}", async (HttpContext context, string id) =>
            {
                Country country = countries.First(country => country.CountryID == Guid.Parse(id));
                if (country != null)
                {
                    return Results.Json(country);
                }
                else
                {
                    // json format
                    return Results.BadRequest(new
                    {
                        Message = "No countries found."
                    });
                }

            })
                .AddEndpointFilter(async (EndpointFilterInvocationContext context, EndpointFilterDelegate next) =>
                {
                    string? id = context.Arguments.OfType<string>().First();
                    if(id == null)
                    {
                        return Results.BadRequest("Id must be provided");
                    }
                    // execute subsequent filter or call action
                    return await next(context);
                });

            return builder;
        }
    }
}
