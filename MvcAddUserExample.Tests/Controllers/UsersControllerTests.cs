using System.Web.Mvc;
using FluentAssertions;
using MvcAddUserExample.Constants;
using MvcAddUserExample.Controllers;
using MvcAddUserExample.Models;
using NUnit.Framework;

namespace MvcAddUserExample.Tests.Controllers
{
    [TestFixture]
    public class UsersControllerTests
    {
        private UsersController controller;

        [SetUp]
        public void SetUp()
        {
            controller = new UsersController();
        }

        [Test]
        public void ShowRegistrationFormReturnsTheRegistrationForm()
        {
            ActionResult result = controller.ShowRegistrationForm();
            result.Should().BeOfType<ViewResult>()
                .Which.ViewName.Should().Be(ViewNames.REGISTRATION_FORM);
        }

        [Test]
        public void RegistrationSuccessReturnsSuccessConfirmationPage()
        {
            controller.RegistrationSuccess()
                .Should().BeOfType<ViewResult>()
                .Which.ViewName.Should().Be(ViewNames.REGISTRATION_SUCCESS);
        }

        [Test]
        public void WhenValidUserIsSubmittedThenSuccessPageIsReturned()
        {
            ActionResult result = controller.PostRegistrationForm(ValidUser());
            result.Should().BeOfType<RedirectToRouteResult>()
                .Which.RouteValues["action"].Should().Be(ActionNames.REGISTRATION_SUCCESS);
        }

        private UserRegistrationViewModel ValidUser()
        {
            return new UserRegistrationViewModel
            {
                Email = "user@example.com",
                Password = "valid passphrase",
                ConfirmPassword = "valid passphrase"
            };
        }
    }
}
