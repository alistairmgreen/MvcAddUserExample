using System.Threading.Tasks;
using Moq;
using MvcAddUserExample.Core.Interfaces.Providers;
using MvcAddUserExample.Core.Interfaces.Services;
using MvcAddUserExample.Core.Models;
using NUnit.Framework;

namespace MvcAddUserExample.Services.Tests.UserServiceTests
{
    [TestFixture]
    public class WhenUserIsAdded
    {
        private const string EMAIL = "user@domain.com";
        private const string PASSWORD = "password";
        private const string MOCK_HASH = "salt and hash";

        private Mock<IUserValidationService> mockUserValidationService;
        private Mock<IPasswordService> mockPasswordService;
        private Mock<IAddUserProvider> mockAddUserProvider;
        private UserToCreate userToCreate;

        [SetUp]
        public async Task Setup()
        {
            mockUserValidationService = new Mock<IUserValidationService>();

            mockPasswordService = new Mock<IPasswordService>();
            mockPasswordService.Setup(s => s.SaltAndHashPassword(PASSWORD))
                .Returns(MOCK_HASH);

            mockAddUserProvider = new Mock<IAddUserProvider>();
            mockAddUserProvider.Setup(p => p.AddUserAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var userService = new UserService(
                mockUserValidationService.Object,
                mockPasswordService.Object,
                mockAddUserProvider.Object);

            userToCreate = new UserToCreate(EMAIL, PASSWORD);
            await userService.AddUserAsync(userToCreate);
        }

        [Test]
        public void ThenUserIsValidated()
        {
            mockUserValidationService.Verify(s => s.ValidateUserToCreate(userToCreate));
        }

        [Test]
        public void ThenPasswordIsHashed()
        {
            mockPasswordService.Verify(s => s.SaltAndHashPassword(PASSWORD));
        }

        [Test]
        public void ThenEmailAddressAndHashedPasswordArePassedToAddUserProvider()
        {
            mockAddUserProvider.Verify(p => p.AddUserAsync(EMAIL, MOCK_HASH));
        }
    }
}
