using Entities;
using FirstWebApplication.EndpointFilters;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.DTO;
using System.Text.Json;

namespace FirstWebApplication.MapGroups
{
    public static class CountriesMapGroup
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
            /**
             * sample output
             * 
             * [
    {
        "CountryID": "e8585365-1a3f-4547-a8ba-af2f44945cab",
        "Name": "Brazil"
    },
    {
        "CountryID": "48906205-926d-4a77-a549-3b217e6892c2",
        "Name": "Canada"
    },
    {
        "CountryID": "5c1697b2-266d-4f72-9a78-78eb4b7b5844",
        "Name": "Iran"
    }
]
             */

            builder.MapPost("/", async (HttpContext context, [FromBody] CountryAddRequest countryAddRequest) =>
            {
                countries.Add(new Country(name: countryAddRequest.Name));

                // string format
                return Results.Ok("Add successful"); // can be string or object
            });
            /**
             * sample output
             * 
             * "Add successful"
             */

            builder.MapGet("/{id}", async (HttpContext context, string id) =>
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

            /**
             * sample output
             * 
             * {
    "countryID": "5c1697b2-266d-4f72-9a78-78eb4b7b5844",
    "name": "Iran"
}
             */

            return builder;
        }
    }
}