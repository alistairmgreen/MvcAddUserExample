using System.Web.Mvc;
using MvcAddUserExample.Models;
using MvcAddUserExample.Constants;
using MvcAddUserExample.Core.Interfaces;
using System.Threading.Tasks;

namespace MvcAddUserExample.Controllers
{
    [Route("~/")]
    public class UsersController : Controller
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        [Route("")]
        [ActionName(ActionNames.SHOW_REGISTRATION_FORM)]
        public ActionResult ShowRegistrationForm()
        {
            return View(ViewNames.REGISTRATION_FORM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("")]
        public async Task<ActionResult> PostRegistrationForm(UserRegistrationViewModel viewModel)
        {
            if (!viewModel.Password.Equals(viewModel.ConfirmPassword, System.StringComparison.Ordinal))
            {
                ModelState.AddModelError("ConfirmPassword", "The two passwords do not match.");
                return View(ViewNames.REGISTRATION_FORM, viewModel);
            }

            await userService.AddUserAsync(viewModel.Email, viewModel.Password);

            return RedirectToAction(ActionNames.REGISTRATION_SUCCESS);
        }

        [HttpGet]
        [ActionName(ActionNames.REGISTRATION_SUCCESS)]
        [Route("success")]
        public ActionResult RegistrationSuccess()
        {
            return View(ViewNames.REGISTRATION_SUCCESS);
        }
    }
}