
using System.Text.RegularExpressions;

namespace FirstWebApplication.MonthCustomConstraint
{
    public class MonthCustomConstraint : IRouteConstraint
    {

        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            var regex = new Regex("^(January|February|March|April|May|June|July|August|September|October|November|December)$");
            return values.ContainsKey(routeKey) && 
                regex.IsMatch(Convert.ToString(values[routeKey]));
        }
    }
}
