using AuthApi.Domain.Dto.Auth;
using AuthApi.Domain.Identity;
using AuthApi.Interfaces;
using AuthApi.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthApi.Services
{
    public class UserService : IUserService
    {

        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(ApplicationDbContext db,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public async Task<List<ApplicationUserDto>> GetUsers(bool addRole = false)
        {
            var users = await _db.Users.ToListAsync();


            List<ApplicationUserDto> result = new List<ApplicationUserDto>();
            foreach (var user in users)
            {
                var userDto = new ApplicationUserDto()
                {
                    UserName = user.Id,
                    Email = user.Email,
                    FullName = user.FullName,
                };
                if (addRole)
                {
                    userDto.Roles = await _userManager.GetRolesAsync(user);
                }
                result.Add(userDto);
            }
            return result;
        }
    }
}
