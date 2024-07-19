using AuthApi.Domain.Dto;
using AuthApi.Domain.Dto.Auth;
using AuthApi.Domain.Entities;
using AuthApi.Domain.Identity;
using AuthApi.Interfaces;
using AuthApi.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace AuthApi.Services
{
    public class AuthService : IAuthService
    {

        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;


        public AuthService(ApplicationDbContext db,
            IJwtTokenGenerator jwtTokenGenerator,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this._jwtTokenGenerator = jwtTokenGenerator;
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._db = db;
        }

        public async Task<ResponseDto> AssignRole(string email, string roleName)
        {
            ResponseDto response = new ResponseDto();

            ApplicationUser? user =
                _db.Users.FirstOrDefault(u =>
                    u.Email!.ToLower() == email.ToLower());

            if (user == null)
            {
                response.IsSuccess = false;
                response.Message = "Tenant não encontrado";
                return response;
            }



            if (!_roleManager.RoleExistsAsync(roleName.ToUpper()).GetAwaiter().GetResult())
                _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();

            var result =  await _userManager.AddToRoleAsync(user, roleName.ToUpper());
            if (result.Succeeded) { 
                response.IsSuccess = true;                
            } else
            {
                response.IsSuccess = false;                
            }
            return response;

        }

        public async Task<List<ApplicationUserDto>> GetUsers(bool withRoles = false)
        {
            var users = await _db.Users.ToListAsync();            

            //var users = _userManager.Users.ToList();

            List<ApplicationUserDto> result = new List<ApplicationUserDto>();
            foreach (var user in users)
            {
                var userDto = new ApplicationUserDto()
                {
                    UserName = user.Id,
                    Email = user.Email,
                    FullName = user.FullName,
                };
                if (withRoles)
                {
                    userDto.Roles = await _userManager.GetRolesAsync(user); 
                }
                result.Add(userDto); 
            }
            return result;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            ApplicationUser? user =
                _db.Users.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower());

            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if (user == null || !isValid)
            {
                return new LoginResponseDto()
                {
                    User = null,
                    Token = ""
                };

            }

            var roles = await _userManager.GetRolesAsync(user);

            var token = _jwtTokenGenerator.GenerateToken(user, roles);

            UserDto userDto = new UserDto()
            {
                Email = user.Email,
                Id = user.Id,
                FullName = user.FullName
            };
            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                User = userDto,
                Token = token
            };

            return loginResponseDto;

        }



        public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
        {
            ApplicationUser user = new ApplicationUser()
            {
                UserName = registrationRequestDto.Email,
                Email = registrationRequestDto.Email,
                NormalizedEmail = registrationRequestDto.Email?.ToUpper(),
                FullName = registrationRequestDto.FullName
            };

            try
            {
                var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);
                if (result.Succeeded)
                {
                    var userToReturn = _db.Users.First(u => u.UserName == registrationRequestDto.Email);
                    UserDto userDto = new UserDto()
                    {
                        Email = userToReturn.Email,
                        Id = userToReturn.Id,
                        FullName = userToReturn.FullName
                    };

                    return "";

                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
    }
}
