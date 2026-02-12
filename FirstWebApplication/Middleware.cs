using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace FirstWebApplication.MiddleWare
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class Middleware
    {
        private readonly RequestDelegate _next;

        public Middleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            Util.printValue("From Middleware: Hello from middleware 4 ");
            await _next(httpContext); // pass to next middleware
            Util.printValue("From Middleware: coming back from middleware 4 ");
        }
    }
}
