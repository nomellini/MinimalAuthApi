using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
