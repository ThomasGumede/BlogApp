using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogApp.Data;
using BlogApp.Models;

namespace BlogApp.Components
{
    public class NavigationMenuViewComponent: ViewComponent
    {
        private readonly BlogAppDbContext _ctx;

        public NavigationMenuViewComponent(BlogAppDbContext ctx) => _ctx = ctx;

        public IViewComponentResult Invoke()
        {
            IEnumerable<Category> Categories = _ctx.Categories;

            return View(Categories);
        }
    }
}