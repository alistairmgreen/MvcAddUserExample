using System.Threading.Tasks;
using Moq;
using MvcAddUserExample.Core.Interfaces.Providers;
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
        private Mock<IAddUserProvider> mockAddUserProvider;

        [SetUp]
        public async Task Setup()
        {
            mockPasswordService = new Mock<IPasswordService>();
            mockPasswordService.Setup(s => s.SaltAndHashPassword(It.IsAny<string>()))
                .Returns(MOCK_HASH);

            mockAddUserProvider = new Mock<IAddUserProvider>();
            mockAddUserProvider.Setup(p => p.AddUserAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var userService = new UserService(mockPasswordService.Object, mockAddUserProvider.Object);

            await userService.AddUserAsync(EMAIL, PASSWORD);
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
