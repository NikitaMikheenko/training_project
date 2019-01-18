using BuisnessLogic;
using Common;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;

namespace Notebook.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService = new UserService();

        [HttpGet]
        [Authorize]
        public ActionResult Users()
        {
            ViewBag.Title = "Users";

            string login = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;

            int roleId = (int)userService.GetUserRoleIdByLogin(login);

            if (roleId < 3)
            {
                List<UserInfoModel> model = userService.GetUsersInfo();

                if (roleId == 1)
                {
                    ViewBag.AllowEdit = true;
                }
                else
                {
                    ViewBag.AllowEdit = false;
                }

                if (model != null)
                {
                    return View(model);
                }

                return RedirectToAction("Index", "Error");
            }

            return RedirectToAction("Index", "Error");
        }

        [HttpGet]
        [Authorize]
        public ActionResult AddUser()
        {
            ViewBag.Title = "AddUser";

            string login = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;

            int? roleId = userService.GetUserRoleIdByLogin(login);

            if (roleId == 1)
            {
                UserEditModel model = new UserEditModel()
                {
                    Roles = userService.GetRoles()
                };

                if (model.Roles != null)
                {
                    return View(model);
                }

                return RedirectToAction("Index", "Error");
            }

            return RedirectToAction("Index", "Error");
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddUser(UserEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model != null)
            {
                if (userService.AddUser(model))
                {
                    return RedirectToAction("Users");
                }

                ModelState.AddModelError("", "This login is already taken!");

                return View(model);
            }

            return RedirectToAction("Index", "Error");
        }

        [HttpGet]
        [Authorize]
        public ActionResult EditUser(string editUserLogin)
        {
            ViewBag.Title = "EditUser";

            string login = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;

            int? roleId = userService.GetUserRoleIdByLogin(login);

            if (roleId == 1)
            {
                UserEditModel model = userService.GetUserByLogin(editUserLogin);

                if (model != null)
                {
                    model.Roles = userService.GetRoles();

                    if (model.Roles != null)
                    {
                        return View(model);
                    }

                    return RedirectToAction("Index", "Error");
                }

                return RedirectToAction("Index", "Error");
            }

            return RedirectToAction("Index", "Error");
        }

        [HttpPost]
        [Authorize]
        public ActionResult EditUser(UserEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model != null)
            {
                if (userService.EditUser(model))
                {
                    return RedirectToAction("Users");
                }

                ModelState.AddModelError("", "This login is already taken!");

                return View(model);
            }

            return RedirectToAction("Index", "Error");
        }

        [HttpGet]
        [Authorize]
        public ActionResult DeleteUser(int? userId)
        {
            string login = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;

            int? roleId = userService.GetUserRoleIdByLogin(login);

            if (roleId == 1)
            {
                if (userService.DeleteUserById((int)userId))
                {
                    return RedirectToAction("Users");
                }

                return RedirectToAction("Index", "Error");
            }

            return RedirectToAction("Index", "Error");
        }
    }
}