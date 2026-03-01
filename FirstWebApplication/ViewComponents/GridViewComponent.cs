using Microsoft.AspNetCore.Mvc;

namespace FirstWebApplication.ViewComponents
{
    // returns data for a component (partial view)
    [ViewComponent]
    public class GridViewComponent: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            // independent from ViewData of each layout
            ViewData["title"] = "Grid View Component";
            return View(); // -> fills data for a partial view
        }
    }
}
