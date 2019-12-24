using System.Web.Mvc;

namespace Nibss.EmailClientWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "NIBSS EMAIL API";

            return View();
        }
    }
}
