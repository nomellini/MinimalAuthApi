using AuthApi.Domain.Dto;
using AuthApi.Domain.Dto.Auth;
using AuthApi.Interfaces;
using AuthApi.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace AuthApi.Controllers
{

    
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;

        public HomeController(
            ITokenProvider tokenProvider,
            IUserService userService, 
            IAuthService authService)
        {
            _tokenProvider = tokenProvider;
            _authService = authService;            
            _userService = userService;
        }

        [HttpGet]
        [Authorize]
        [Route("/")]
        public async Task<IActionResult> Index()
        {
            return View( await _userService.GetUsers() );
        }

        [HttpGet]
        [Route("/login")]
        public IActionResult Login()
        {
            return View(new LoginRequestDto());
        }


        [HttpGet]
        [Route("/logout")]
        public async Task<IActionResult> Logout()
        {
            _tokenProvider.ClearToken();
            await HttpContext.SignOutAsync("MultiScheme");

            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> LoginPost([FromForm] LoginRequestDto loginDto)
        {

            string token = string.Empty;

            if (ModelState.IsValid)
            {
                var result = await _authService.Login(loginDto);
                token = result.Token!;

                _tokenProvider.SetToken(token);
            
                await SignInUser(result);

            }


            return RedirectToAction("Index");
        }

        private async Task SignInUser(LoginResponseDto loginResponse)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(loginResponse.Token!);
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
                jwt.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value!));

            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
                jwt.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name)?.Value!));

            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email,
                jwt.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email)?.Value!));

            identity.AddClaim(new Claim(ClaimTypes.Name,
                jwt.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email)?.Value!));


            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }


    }
}
