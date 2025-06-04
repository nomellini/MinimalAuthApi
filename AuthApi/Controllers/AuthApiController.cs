using AuthApi.Domain.Dto.Auth;
using AuthApi.Domain.Dto;
using AuthApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace AuthApi.Controllers
{
    [Route("api/auth/[controller]")]
    [ApiController]
    public class AuthApiController : ControllerBase
    {

        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        protected ResponseDto _response;

        public AuthApiController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
            _response = new();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
        {
            var errorMessage = await _authService.Register(model);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                _response.IsSuccess = false;
                _response.Message = errorMessage;
                return BadRequest(_response);
            }

            return Ok(_response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {

            var loginResponse = await _authService.Login(model);
            if (loginResponse.User == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Usuário ou senha incorretos";
                return BadRequest(_response);
            }
            _response.Result = loginResponse;



            return Ok(_response);
        }

        [HttpPost("AssignRole")]
        [Authorize(Roles = "system-admin")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto model)
        {

            var assignRole = await _authService.AssignRole(model.Email!, model.Role!);
            if (!assignRole.IsSuccess)
            {
                _response.IsSuccess = false;
                _response.Message = "Erro ao negociar o papel";
                return BadRequest(_response);
            }

            return Ok(_response);
        }


        [HttpGet("Users")]
        [Authorize(Roles = "system-admin")]
        public async Task<IActionResult> Users()
        {
            var lista = await _userService.GetUsers(true);
            return Ok(lista);
        }


    }
}
