using BlogApp.Models;

namespace BlogApp.Data;

public interface ICategoryRepository
{
    IEnumerable<Category> GetListOfCategories();
    Category GetCategoryDetails(int id);
    void AddCategory(Category category);
    void ModifyCategory(Category category);
    void DeleteCategory(int id);
    void Save();

}