using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcAddUserExample.Controllers
{
    public class UsersController : Controller
    {
        public ActionResult Index()
        {
            return View("RegistrationForm");
        }
    }
}