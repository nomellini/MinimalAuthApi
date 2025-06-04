using System.Threading.Tasks;
using Xunit;
using Moq;
using AuthApi.Services;
using AuthApi.Domain.Dto.Auth;
using AuthApi.Repository;
using Microsoft.EntityFrameworkCore;
using AuthApi.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using AuthApi.Interfaces;

namespace AuthApi.Tests.Services
{
    public class AuthServiceTests
    {
        [Fact]
        public async Task Login_InvalidCredentials_ReturnsNullUser()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "AuthServiceTests")
                .Options;
            var context = new ApplicationDbContext(options);

            var testUser = new ApplicationUser { UserName = "testuser", Email = "t@example.com" };
            context.Users.Add(testUser);
            context.SaveChanges();

            var userStore = new Mock<IUserStore<ApplicationUser>>();
            var userManager = new Mock<UserManager<ApplicationUser>>(userStore.Object, null, null, null, null, null, null, null, null);
            userManager.Setup(um => um.CheckPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(false);

            var roleManager = new Mock<RoleManager<IdentityRole>>(Mock.Of<IRoleStore<IdentityRole>>(), null, null, null, null);
            var jwtTokenGen = new Mock<IJwtTokenGenerator>();
            var userService = new Mock<IUserService>();

            var service = new AuthService(context, jwtTokenGen.Object, userService.Object, userManager.Object, roleManager.Object);

            var request = new LoginRequestDto { UserName = "testuser", Password = "wrong" };

            var result = await service.Login(request);

            Assert.Null(result.User);
        }
    }
}
