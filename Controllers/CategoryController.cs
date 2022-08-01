using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using BlogApp.Data;
using BlogApp.Models;

namespace BlogApp.Controllers;

public class CategoryController : Controller
{
    public CategoryController(IWebHostEnvironment environment, ICategoryRepository categoryRepository)
    {     
        _env = environment;
        _categoryRep = categoryRepository;
    }

    private readonly IWebHostEnvironment _env; 
    private readonly ICategoryRepository _categoryRep;

    public IActionResult Index()
    {
        IEnumerable<Category> category = _categoryRep.GetListOfCategories();
        return View(category);
    }

    public ViewResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Category category, IFormFile thumbnail)
    {
        if(ModelState.IsValid)
        {
            if(thumbnail != null)
            {
                category.Thumbnail = await Helpers.Helpers.UploadFile(thumbnail, _env, "Category");
            }
            _categoryRep.AddCategory(category);
            _categoryRep.Save();
            TempData["Message"] = "Category Created  Successfully";

            return RedirectToAction(nameof(Index));
        }

        return View(category);
    }

    public IActionResult Edit(int id)
    {

        Category category =_categoryRep.GetCategoryDetails(id);

        if(category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id", "Title", "Description", "Thumbnail")]Category category, IFormFile thumbnail)
    {
        if (id != category.Id)
        {
            return NotFound();
        }

        if(ModelState.IsValid)
        {
            if(thumbnail != null)
            {
                category.Thumbnail = await Helpers.Helpers.UploadFile(thumbnail, _env, "Category");
            }

            _categoryRep.ModifyCategory(category);
            _categoryRep.Save();
            TempData["Message"] = "Category Updated  Successfully";

            return RedirectToAction(nameof(Index));

        }
        return View(category);
    }


    public IActionResult DeleteConfirm(int id)
    {

        Category category = _categoryRep.GetCategoryDetails(id);

        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {

        _categoryRep.DeleteCategory(id);
        _categoryRep.Save();
        TempData["Message"] = "Category Deleted  Successfully";

        return RedirectToAction(nameof(Index));
    }

}