using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KudevolveWeb.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        [Route("register")]
        public ActionResult Index()
        {
            return Redirect("/Accounts/Register");
        }
    }
}