using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using BlogApp.Data;
using BlogApp.Models;

namespace BlogApp.Controllers
{
    [Route("[controller]")]
    public class CommentController : Controller
    {
        private readonly BlogAppDbContext _ctx;

        public CommentController(BlogAppDbContext context)
        {
            _ctx = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Create(Comment comment)
        {
            if(_ctx.Comments == null)
            {
                return Json(new { success=false});
            }

            if (ModelState.IsValid)
            {
                await _ctx.Comments.AddAsync(comment);
                await _ctx.SaveChangesAsync();
                var model = JsonConvert.SerializeObject(comment);

                return Json( new { success=true, model=model});
            }

            var errors = ModelState.Values.SelectMany(m => m.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return Json(new { success = false, model=errors });
        }

    }
}