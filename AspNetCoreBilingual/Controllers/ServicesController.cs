using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreBilingual.Controllers
{
    [Route("services")]
    public class ServicesController : BaseController
    {
        // GET
        [HttpGet("dashboard-tableaudebord")]
        public IActionResult Index()
        {
            SetPageTitle("Services");
            return View();
        }
    }
}