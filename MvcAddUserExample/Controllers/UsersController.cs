using System.Web.Mvc;

namespace MvcAddUserExample.Controllers
{
    [Route("~/")]
    public class UsersController : Controller
    {
        [Route("")]
        public ActionResult ShowRegistrationForm()
        {
            return View("RegistrationForm");
        }
    }
}