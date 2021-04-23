using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreBilingual.Controllers
{
    [Route("home-accueil")]
    public class HomeController : BaseController
    {
        //[HttpGet("/{culture:regex(en)}/home")]
        //[HttpGet("/{culture:regex(fr)}/accueil")]
        [HttpGet]
        public IActionResult Index()
        {
            SetPageTitle("Home");
            return View();
        }
    }
}