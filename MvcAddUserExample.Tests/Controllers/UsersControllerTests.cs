using System.Web.Mvc;
using FluentAssertions;
using MvcAddUserExample.Controllers;
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
                .Which.ViewName.Should().Be("RegistrationForm");
        }
    }
}
