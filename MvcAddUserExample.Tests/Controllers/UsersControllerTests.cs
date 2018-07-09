using System.Threading.Tasks;
using System.Web.Mvc;
using FluentAssertions;
using Moq;
using MvcAddUserExample.Constants;
using MvcAddUserExample.Controllers;
using MvcAddUserExample.Core.Exceptions;
using MvcAddUserExample.Core.Interfaces.Services;
using MvcAddUserExample.Core.Models;
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
            mockUserService.Setup(s => s.AddUserAsync(It.IsAny<UserToCreate>()))
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
        public async Task WhenUserIsSubmittedThenUserServiceIsCalled()
        {
            var user = ValidUser();
            await controller.PostRegistrationForm(user);

            mockUserService.Verify(s => s.AddUserAsync(
                It.Is<UserToCreate>(u => u.Email == user.Email && u.Password == user.Password)));
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

        [Test]
        public void AntiForgeryTokenIsCheckedForAllPostRequests()
        {
            typeof(UsersController).Methods()
              .ThatAreDecoratedWith<HttpPostAttribute>()
              .Should()
              .BeDecoratedWith<ValidateAntiForgeryTokenAttribute>(
                "because all Actions with HttpPost require ValidateAntiForgeryToken");
        }

        [Test]
        public async Task WhenEmailIsNotValidThenFormIsRedisplayedWithError()
        {
            var user = new UserRegistrationViewModel
            {
                Email = "invalid email",
                Password = "good password",
                ConfirmPassword = "good password"
            };

            mockUserService.Setup(s => s.AddUserAsync(It.IsAny<UserToCreate>()))
                .ThrowsAsync(new InvalidEmailException(user.Email));

            ActionResult result = await controller.PostRegistrationForm(user);

            controller.ModelState.IsValid.Should().BeFalse("because the email address is not valid");

            controller.ModelState.Should().ContainKey("Email", "because the email address is not valid")
                .WhichValue.Errors.Should().NotBeEmpty("because there was a validation error");

            result.Should().BeOfType<ViewResult>()
               .Which.ViewName.Should().Be(ViewNames.REGISTRATION_FORM, "because the registration form should be redisplayed");
        }

        [Test]
        public async Task WhenPasswordIsTooWeakThenFormIsRedisplayedWithError()
        {
            var user = new UserRegistrationViewModel
            {
                Email = "user@example.com",
                Password = "123",
                ConfirmPassword = "123"
            };

            mockUserService.Setup(s => s.AddUserAsync(It.IsAny<UserToCreate>()))
                .ThrowsAsync(new InvalidPasswordException());

            ActionResult result = await controller.PostRegistrationForm(user);

            controller.ModelState.IsValid.Should().BeFalse("because the password is not valid");

            controller.ModelState.Should().ContainKey("Password", "because the password is not valid")
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
