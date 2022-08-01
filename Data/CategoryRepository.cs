using BlogApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data;

public class CategoryRepository: ICategoryRepository
{
    public CategoryRepository(BlogAppDbContext ctx, IWebHostEnvironment environment){
        _context = ctx;
        _env = environment;
    }

    private readonly BlogAppDbContext _context;
    private readonly IWebHostEnvironment _env;
    public IEnumerable<Category> GetListOfCategories()
    {
        return _context.Categories.Include(posts => posts.Posts).ToList();
    }

    public Category GetCategoryDetails(int id)
    {
        return _context.Categories.Include(p => p.Posts).FirstOrDefault(c => c.Id == id);
    }
    public void AddCategory(Category category)
    {
        _context.Categories.Add(category);
    }
    public void ModifyCategory(Category category)
    {
        _context.Entry(category).State = EntityState.Modified;
    }
    public void DeleteCategory(int id)
    {
        Category category = _context.Categories.Find(id);
        if (category.Thumbnail != null)
        {
            if (System.IO.File.Exists($"{_env.WebRootPath}/{category.Thumbnail}")) System.IO.File.Delete($"{_env.WebRootPath}/{category.Thumbnail}");
        }
        _context.Categories.Remove(category);
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}