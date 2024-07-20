using AuthApi.Domain.Dto;
using AuthApi.Domain.Dto.Auth;
using AuthApi.Interfaces;
using AuthApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;


        public HomeController(IUserService userService, IAuthService authService)
        {
            _authService = authService;
            _userService = userService;
        }

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

        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> LoginPost([FromForm] LoginRequestDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.Login(loginDto);
                loginDto.UserName = result.Token!;
            }
            return View("Login", loginDto);
        }


    }
}
