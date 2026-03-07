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

                await context.Response.WriteAsync("Add successful");
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
                    await context.Response.WriteAsync("No country found");
                }

            });

            return builder;
        }
    }
}
