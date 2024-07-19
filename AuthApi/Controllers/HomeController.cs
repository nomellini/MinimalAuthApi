using AuthApi.Interfaces;
using AuthApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("/")]
        public async Task<IActionResult> Index()
        {
            return View( await _userService.GetUsers() );
        }
    }
}
