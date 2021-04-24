using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreBilingual.Controllers
{
    [Route("home-accueil")]
    public class HomeController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            SetPageTitle("Home");
            return View();
        }
    }
}