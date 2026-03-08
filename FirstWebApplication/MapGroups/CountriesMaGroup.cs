using Entities;
using FirstWebApplication.EndpointFilters;
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
                .AddEndpointFilter<CustomCountryEndpointFilter>();

            return builder;
        }
    }
}