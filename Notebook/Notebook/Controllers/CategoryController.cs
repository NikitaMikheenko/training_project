using BuisnessLogic;
using Common;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Notebook.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService = new CategoryService(); 

        [HttpGet]
        [Authorize]
        public ActionResult Categories()
        {
            ViewBag.Title = "Categories";

            List<Category> model = categoryService.GetCategories();

            if (model != null)
            {
                return View(model);
            }

            return RedirectToAction("Index", "Error");
        }

        [HttpGet]
        [Authorize]
        public ActionResult AddCategory()
        {
            ViewBag.Title = "Add Category";

            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddCategory(Category model)
        {
            if (ModelState.IsValid)
            {
                if (model != null)
                {
                    if (categoryService.AddCategory(model))
                    {
                        return RedirectToAction("Categories");
                    }
                }
            }

            return RedirectToAction("Index", "Error");
        }

        [HttpGet]
        [Authorize]
        public ActionResult EditCategory(int? id)
        {
            ViewBag.Title = "Edit Category";

            if (id == null)
            {
                return RedirectToAction("Index", "Error");
            }

            Category model = categoryService.GetCategoryById((int)id);

            if (model == null)
            {
                return RedirectToAction("Index", "Error");
            }

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult EditCategory(Category model)
        {
            if (ModelState.IsValid)
            {
                if (model != null)
                {
                    if (categoryService.EditCategory(model))
                    {
                        return RedirectToAction("Categories");
                    }
                }
            }

            return RedirectToAction("Index", "Error");
        }

        [HttpGet]
        [Authorize]
        public ActionResult DeleteCategory(int? id)
        {
            if (id != null)
            {
                if (categoryService.DeleteCategoryById((int)id))
                {
                    return RedirectToAction("Categories");
                }
            }

            return RedirectToAction("Index", "Error");
        }
    }
}