using BuisnessLogic;
using Common;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;

namespace Notebook.Controllers
{
    public class NoteController : Controller
    {
        private readonly IUserService userService = new UserService();

        private readonly INoteService noteService = new NoteService();

        private readonly ICategoryService categoryService = new CategoryService();

        [HttpGet]
        [Authorize]
        public ActionResult Notes()
        {
            ViewBag.Title = "Notes";

            string login = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;

            int? userRoleId = userService.GetUserRoleIdByLogin(login);

            if (userRoleId != null)
            {
                if (userRoleId < 3)
                {
                    ViewBag.Layout = "~/Views/Shared/AdvLayout.cshtml";

                    ViewBag.SearchType = "AdvSearch";
                }
                else
                {
                    ViewBag.Layout = "~/Views/Shared/Layout.cshtml";

                    ViewBag.SearchType = "Search";
                }

                List<NoteInfoModel> model = noteService.GetNotesInfoByUserLogin(login);

                if (model!=null)
                {
                    return View(model);
                }

                return RedirectToAction("Index", "Error");
            }

            return RedirectToAction("Index", "Error");
        }

        [HttpGet]
        [Authorize]
        public ActionResult AddNote()
        {
            ViewBag.Title = "AddNote";

            string login = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;

            int? userRoleId = userService.GetUserRoleIdByLogin(login);

            if (userRoleId != null)
            {
                if (userRoleId < 3)
                {
                    ViewBag.Layout = "~/Views/Shared/AdvLayout.cshtml";
                }
                else
                {
                    ViewBag.Layout = "~/Views/Shared/Layout.cshtml";
                }

                List<Category> categories = categoryService.GetCategories();

                if (categories != null)
                {
                    return View(new NoteAddModel()
                    {
                        Notes = noteService.GetNotesInfoByUserLogin(login),
                        Categories = categories,
                        User = new UserInfoModel()
                        {
                            Login = login
                        }
                    });
                }

                return RedirectToAction("Index", "Error");
            }

            return RedirectToAction("Index", "Error");
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddNote(NoteAddModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (noteService.AddNote(model))
            {
                return RedirectToAction("Notes");
            }

            return RedirectToAction("Index", "Error");
        }

        [HttpGet]
        [Authorize]
        public ActionResult EditNote(int? noteId)
        {
            ViewBag.Title = "EditNote";

            if (noteId != null)
            {
                string login = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;

                int? userRoleId = userService.GetUserRoleIdByLogin(login);

                if (userRoleId != null)
                {
                    if (userRoleId < 3)
                    {
                        ViewBag.Layout = "~/Views/Shared/AdvLayout.cshtml";
                        ViewBag.ShowUser = true;
                    }
                    else
                    {
                        ViewBag.Layout = "~/Views/Shared/Layout.cshtml";
                        ViewBag.ShowUser = false;
                    }

                    Note note = noteService.GetNoteById((int)noteId);

                    if (note != null)
                    {
                        List<Category> categories = categoryService.GetCategories();

                        if (categories != null)
                        {
                            return View(new NoteEditModel()
                            {
                                Id = note.Id,
                                Name = note.Name,
                                Date = note.Date,
                                Description = note.Description,
                                Imglink = note.Imglink,
                                CategoryId = note.Category.Id,
                                Notes = noteService.GetNotesInfoByUserLogin(login),
                                LinkedNotesIds = noteService.GetLinkedNotesIds((int)note.Id),
                                Categories = categories,
                                User = note.User
                            });
                        }

                        return RedirectToAction("Index", "Error");
                    }

                    return RedirectToAction("Index", "Error");
                }

                return RedirectToAction("Index", "Error");
            }

            return RedirectToAction("Index", "Error");
        }

        [HttpPost]
        [Authorize]
        public ActionResult EditNote(NoteEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (noteService.EditNote(model))
            {
                return RedirectToAction("Notes");
            }

            return RedirectToAction("Index", "Error");
        }

        [HttpGet]
        [Authorize]
        public ActionResult ShowNote(int? noteId)
        {
            ViewBag.Title = "EditNote";

            if (noteId != null)
            {
                string login = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;

                int? userRoleId = userService.GetUserRoleIdByLogin(login);

                if (userRoleId != null)
                {
                    if (userRoleId < 3)
                    {
                        ViewBag.Layout = "~/Views/Shared/AdvLayout.cshtml";
                        ViewBag.ShowUser = true;
                    }
                    else
                    {
                        ViewBag.Layout = "~/Views/Shared/Layout.cshtml";
                        ViewBag.ShowUser = false;
                    }

                    Note model = noteService.GetNoteById((int)noteId);

                    if (model != null)
                    {
                        return View(model);
                    }

                    return RedirectToAction("Index", "Error");
                }

                return RedirectToAction("Index", "Error");
            }

            return RedirectToAction("Index", "Error");
        }

        [HttpGet]
        [Authorize]
        public ActionResult DeleteNote(int? id)
        {
            if (noteService.DeleteNote(id))
            {
                return RedirectToAction("Notes");
            }

            return RedirectToAction("Index", "Error");
        }
    }
}