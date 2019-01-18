using System.Collections.Generic;
using Common;
using DataAccess;

namespace BuisnessLogic
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryContext categoryContext = new CategoryContext();

        public bool AddCategory(Category model)
        {
            if (categoryContext.GetCategoryIdByName(model.Name) == null)
            {
                return categoryContext.AddCategory(model);
            }

            return false;
        }

        public bool DeleteCategoryById(int id)
        {
            return categoryContext.DeleteCategoryById(id);
        }

        public bool EditCategory(Category model)
        {
            if (categoryContext.GetCategoryIdByName(model.Name) == null)
            {
                return categoryContext.EditCategory(model);
            }
            
            return false;
        }

        public List<Category> GetCategories()
        {
            return categoryContext.GetCategories();
        }

        public Category GetCategoryById(int id)
        {
            return categoryContext.GetCategoryById(id);
        }
    }
}
