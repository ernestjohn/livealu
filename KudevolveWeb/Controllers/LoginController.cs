using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KudevolveWeb.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [Route("login")]
        public ActionResult Index()
        {
            return Redirect("/Accounts/Login");
        }
    }
}