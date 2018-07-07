using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using MvcAddUserExample.Core.Interfaces.Services;
using NUnit.Framework;

namespace MvcAddUserExample.Services.Tests.UserServiceTests
{
    [TestFixture]
    public class WhenUserIsAdded
    {
        private const string EMAIL = "user@domain.com";
        private const string PASSWORD = "password";
        private const string MOCK_HASH = "salt and hash";

        private Mock<IPasswordService> mockPasswordService;

        [SetUp]
        public async Task Setup()
        {
            mockPasswordService = new Mock<IPasswordService>();
            mockPasswordService.Setup(s => s.SaltAndHashPassword(It.IsAny<string>()))
                .Returns(MOCK_HASH);

            var userService = new UserService(mockPasswordService.Object);

            await userService.AddUserAsync(EMAIL, PASSWORD);
        }

        [Test]
        public void ThenPasswordIsHashed()
        {
            mockPasswordService.Verify(s => s.SaltAndHashPassword(PASSWORD));
        }
    }
}
