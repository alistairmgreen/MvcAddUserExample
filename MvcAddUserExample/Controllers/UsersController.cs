using System.Web.Mvc;
using MvcAddUserExample.Models;
using MvcAddUserExample.Constants;
using MvcAddUserExample.Core.Interfaces.Services;
using System.Threading.Tasks;
using MvcAddUserExample.Core.Exceptions;

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

            try
            {
                await userService.AddUserAsync(viewModel.Email, viewModel.Password);
                return RedirectToAction(ActionNames.REGISTRATION_SUCCESS);
            }
            catch (DuplicateEmailException e)
            {
                ModelState.AddModelError("Email", e.Message);
                return View(ViewNames.REGISTRATION_FORM, viewModel);
            }
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