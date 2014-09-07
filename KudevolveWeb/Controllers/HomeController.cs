using System.Web.Mvc;

namespace KudevolveWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Uzito()
        {
          
            return View();
        }
        public ActionResult Sample()
        {
            return View();
        }
    }
}