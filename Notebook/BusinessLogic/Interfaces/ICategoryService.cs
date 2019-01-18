using Common;
using System.Collections.Generic;

namespace BuisnessLogic
{
    public interface ICategoryService
    {
        bool AddCategory(Category model);

        bool EditCategory(Category model);

        Category GetCategoryById(int id);

        List<Category> GetCategories();

        bool DeleteCategoryById(int id);
    }
}
