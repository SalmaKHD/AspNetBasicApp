
namespace FirstWebApplication.EndpointFilters
{
    public class CustomCountryEndpointFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            string? id = context.Arguments.OfType<string>().First();
            if (id == null)
            {
                return Results.BadRequest("Id must be provided");
            }
            // execute subsequent filter or call action
            return await next(context);
        }
    }
}
