using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreBilingual.Controllers
{
    public class HomeController : BaseController
    {
        [HttpGet("/{culture:regex(en)}/home")]
        [HttpGet("/{culture:regex(fr)}/accueil")]
        public IActionResult Index()
        {
            return View();
        }
    }
}