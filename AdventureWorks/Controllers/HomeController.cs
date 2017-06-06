using System.Web.Mvc;

namespace AdventureWorks.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Beispielanwendung zur Adventure Works Cycles Datenbank.";

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}