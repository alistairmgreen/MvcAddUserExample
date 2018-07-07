using System.Threading.Tasks;
using System.Web.Mvc;
using FluentAssertions;
using Moq;
using MvcAddUserExample.Constants;
using MvcAddUserExample.Controllers;
using MvcAddUserExample.Core.Interfaces;
using MvcAddUserExample.Models;
using NUnit.Framework;

namespace MvcAddUserExample.Tests.Controllers
{
    [TestFixture]
    public class UsersControllerTests
    {
        private UsersController controller;
        private Mock<IUserService> mockUserService;

        [SetUp]
        public void SetUp()
        {
            mockUserService = new Mock<IUserService>();
            mockUserService.Setup(s => s.AddUserAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            controller = new UsersController(mockUserService.Object);
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
        public async Task WhenValidUserIsSubmittedThenSuccessPageIsReturned()
        {
            ActionResult result = await controller.PostRegistrationForm(ValidUser());
            result.Should().BeOfType<RedirectToRouteResult>()
                .Which.RouteValues["action"].Should().Be(ActionNames.REGISTRATION_SUCCESS);
        }

        [Test]
        public async Task WhenPasswordsDoNotMatchThenFormIsRedisplayedWithError()
        {
            var user = new UserRegistrationViewModel
            {
                Email = "user@example.com",
                Password = "valid passphrase",
                ConfirmPassword = "mistyped passphrase"
            };

            ActionResult result = await controller.PostRegistrationForm(user);

            controller.ModelState.IsValid.Should().BeFalse("because the passwords do not match");

            controller.ModelState.Should().ContainKey("ConfirmPassword", "because the Confirm Password field is incorrect")
                .WhichValue.Errors.Should().NotBeEmpty("because there was a validation error");

            result.Should().BeOfType<ViewResult>()
               .Which.ViewName.Should().Be(ViewNames.REGISTRATION_FORM, "because the registration form should be redisplayed");
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
