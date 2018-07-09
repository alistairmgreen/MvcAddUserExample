using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using MvcAddUserExample.Core.Exceptions;
using MvcAddUserExample.Core.Models;
using NUnit.Framework;

namespace MvcAddUserExample.Services.Tests
{
    [TestFixture]
    public class UserValidationServiceTests
    {
        private UserValidationService userValidationService;

        private const string VALID_EMAIL = "valid@user.com";
        private const string VALID_PASSWORD = "a strong passphrase";

        [SetUp]
        public void SetUp()
        {
            userValidationService = new UserValidationService();
        }

        [Test]
        public void WhenEmailAndPasswordAreValidThenNoExceptionIsThrown()
        {
            userValidationService.Invoking(s => s.ValidateUserToCreate(new UserToCreate(VALID_EMAIL, VALID_PASSWORD)))
            .Should().NotThrow();
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void WhenEmailIsMissingThenInvalidEmailExceptionIsThrown(string missingEmail)
        {
            var invalidUser = new UserToCreate(missingEmail, VALID_PASSWORD);
            userValidationService.Invoking(s => s.ValidateUserToCreate(invalidUser))
                .Should().Throw<InvalidEmailException>()
                .WithMessage("The email address is required.");
        }

        [Test]
        public void WhenEmailHasNoAtSymbolThenInvalidEmailExceptionIsThrown()
        {
            var invalidUser = new UserToCreate("not an email address", VALID_PASSWORD);
            userValidationService.Invoking(s => s.ValidateUserToCreate(invalidUser))
                .Should().Throw<InvalidEmailException>()
                .WithMessage("The email address is not valid.");
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("        ")]
        [TestCase("1234567")]
        public void WhenPasswordDoesNotContainAtLeast8NonWhitespaceCharactersThenInvalidPasswordExceptionIsThrown(string invalidPassword)
        {
            var invalidUser = new UserToCreate(VALID_EMAIL, invalidPassword);
            userValidationService.Invoking(s => s.ValidateUserToCreate(invalidUser))
                .Should().Throw<InvalidPasswordException>();
        }
    }
}
