using BuisnessLogic;
using Common;
using System.Web.Mvc;
using System.Web.Security;

namespace Notebook.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearchService searchService = new SearchService();

        [HttpGet]
        [Authorize]
        public ActionResult Search(int? searchType, string requestText)
        {
            ViewBag.Title = "Search";

            if (searchType == null || requestText == null)
            {
                return View(new SearchModel());
            }

            string login = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;

            return View(searchService.GetSearchResponse(new SearchModel()
            {
                UserLogin = login,
                SearchType = (int)searchType,
                RequestText = requestText
            }));
        }

        [HttpPost]
        [Authorize]
        public ActionResult Search(SearchModel model)
        {
            ViewBag.Title = "Search";

            string login = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;

            return View(searchService.GetSearchResponse(new SearchModel()
            {
                UserLogin = login,
                SearchType = model.SearchType,
                RequestText = model.RequestText
            }));
        }

        [HttpGet]
        [Authorize]
        public ActionResult AdvSearch(int? searchType, string requestText)
        {
            ViewBag.Title = "Search";

            if (searchType == null || requestText == null)
            {
                return View(new SearchModel());
            }

            string login = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;

            return View(searchService.GetAdvSearchResponse(new SearchModel()
            {
                UserLogin = login,
                SearchType = (int)searchType,
                RequestText = requestText
            }));
        }

        [HttpPost]
        [Authorize]
        public ActionResult AdvSearch(SearchModel model)
        {
            ViewBag.Title = "Search";

            string login = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;

            return View(searchService.GetAdvSearchResponse(new SearchModel()
            {
                UserLogin = login,
                SearchType = model.SearchType,
                RequestText = model.RequestText
            }));
        }
    }
}