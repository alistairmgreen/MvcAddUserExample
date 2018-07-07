﻿using System.Web.Mvc;
using MvcAddUserExample.Models;
using MvcAddUserExample.Constants;

namespace MvcAddUserExample.Controllers
{
    [Route("~/")]
    public class UsersController : Controller
    {
        [HttpGet]
        [Route("")]
        [ActionName(ActionNames.SHOW_REGISTRATION_FORM)]
        public ActionResult ShowRegistrationForm()
        {
            return View(ViewNames.REGISTRATION_FORM);
        }

        [HttpPost]
        [Route("")]
        public ActionResult PostRegistrationForm(UserRegistrationViewModel viewModel)
        {
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