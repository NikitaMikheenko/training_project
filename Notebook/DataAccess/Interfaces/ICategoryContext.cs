using Common;
using System.Collections.Generic;

namespace DataAccess
{
    public interface ICategoryContext
    {
        bool AddCategory(Category model);

        bool EditCategory(Category model);

        Category GetCategoryById(int id);

        List<Category> GetCategories();

        bool DeleteCategoryById(int id);

        int? GetCategoryIdByName(string name);
    }
}
