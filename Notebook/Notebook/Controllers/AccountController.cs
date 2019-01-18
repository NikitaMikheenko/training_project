using BuisnessLogic;
using Common;
using System.Web.Mvc;
using System.Web.Security;

namespace Notebook.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthentificationService authentificationService = new AuthentificationService();

        private readonly IUserService userService = new UserService();

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            ViewBag.Title = "Login";
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!authentificationService.LoginValidate(model))
            {
                ModelState.AddModelError("", "Invalid password!");

                return View(model);
            }

            FormsAuthentication.SetAuthCookie(model.Login, true);

            return RedirectToAction("Notes", "Note");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Registration()
        {
            ViewBag.Title = "Registration";

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(RegistrationModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!authentificationService.Registration(model))
            {
                ModelState.AddModelError("", "This login is already taken!");

                return View(model);
            }

            FormsAuthentication.SetAuthCookie(model.Login, true);

            return RedirectToAction("Notes", "Note");
        }

        [HttpGet]
        [Authorize]
        public ActionResult AccountEdit()
        {
            ViewBag.Title = "AccountEdit";

            string login = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;

            switch (userService.GetUserRoleIdByLogin(login))
            {
                case 1:
                    ViewBag.Layout = "~/Views/Shared/AdvLayout.cshtml";
                    ViewBag.Admin = true;
                    break;
                case 2:
                    ViewBag.Layout = "~/Views/Shared/AdvLayout.cshtml";
                    ViewBag.Admin = false;
                    break;
                case 3:
                    ViewBag.Layout = "~/Views/Shared/Layout.cshtml";
                    ViewBag.Admin = false;
                    break;
                default:
                    return RedirectToAction("");
            }

            UserEditModel model = userService.GetUserByLogin(login);

            if (model != null)
            {
                return View(model);
            }

            return RedirectToAction("Index", "Error");
        }

        [HttpPost]
        [Authorize]
        public ActionResult AccountEdit(UserEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model != null)
            {
                if (userService.EditUser(model))
                {
                    return RedirectToAction("Notes", "Note");
                }
            }

            return RedirectToAction("Index", "Error");
        }

        [HttpGet]
        [Authorize]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login");
        }
    }
}