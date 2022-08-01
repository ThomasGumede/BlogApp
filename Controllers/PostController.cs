using BlogApp.Models;
using BlogApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;



namespace BlogApp.Controllers;

public class PostController: Controller
{

    public PostController(IMovieRepository mr, IWebHostEnvironment env, BlogAppDbContext ctx)
    {
        _movierepository = mr;
        _env = env;
        _context = ctx;
    }
    private readonly IWebHostEnvironment _env;
    private readonly BlogAppDbContext _context;
    private readonly IMovieRepository _movierepository;

    public IActionResult Index(int? id)
    {
        IEnumerable<Post> posts;

        if(id == null)
        {
          posts = _movierepository.BlogList(null);
        }
        else{
            ViewData["Category"] = _context.Categories.FirstOrDefault(c => c.Id == id);
            posts = _movierepository.BlogList(id);
        }
        
        return View(posts);
    }
    
    public IActionResult Details(int id)
    {
        Post post = _movierepository.GetBlog(id);

        if(post == null)
        {
            return NotFound();
        }

        return View(post);
    }

    public ViewResult Create()
    {
        ViewBag.category = new SelectList(_context.Categories.ToList(), "Id", "Title");

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Post post, IFormFile photo)
    {
        if(ModelState.IsValid)
        {
            post.Photo = await Helpers.Helpers.UploadFile(photo, _env);
            _movierepository.CreateBlog(post);
            _movierepository.Save();
            TempData["Message"] = "Post Created Successfully";

            return RedirectToAction(nameof(Index));
        }
        ViewBag.category = new SelectList(_context.Categories.ToList(), "Id", "Title");
        return View(post);
    }

    public IActionResult Update(int id)
    {
        ViewBag.category = new SelectList(_context.Categories.ToList(), "Id", "Title");
        Post post = _movierepository.GetBlog(id);
        if(post == null) return NotFound();
        return View(post);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(Post post, IFormFile photo)
    {
        if(ModelState.IsValid)
        {
            if(photo != null)
            {
                post.Photo = await Helpers.Helpers.UploadFile(photo, _env);
            }else
            {
                post.Photo = post.Photo;
            }
            _movierepository.ModifyBlog(post);
            _movierepository.Save();
            TempData["Message"] = "Post Updated Successfully";

            return RedirectToAction(nameof(Details), new { id = post.Id});
        }
        
        ViewBag.category = new SelectList(_context.Categories.ToList(), "Id", "Title");
        return View(post);
    }

    public IActionResult DeleteConfirm(int id)
    {

        Post Post = _movierepository.GetBlog(id);

        if (Post == null)
        {
            return NotFound();
        }

        return View(Post);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int Id)
    {
        

        _movierepository.DeleteBlog(Id);
        _movierepository.Save();
        TempData["Message"] = "Post Deleted  Successfully";

        return RedirectToAction(nameof(Index));
    }

}