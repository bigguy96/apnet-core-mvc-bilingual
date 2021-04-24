using AspNetCoreBilingual.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreBilingual.Controllers
{
    [Route("services")]
    public class ServicesController : BaseController
    {
        [HttpGet("dashboard-tableaudebord")]
        public IActionResult Index()
        {
            SetPageTitle("Services");
            return View(new ServiceViewModel());
        }

        [HttpPost("dashboard-tableaudebord")]
        public IActionResult Index(ServiceViewModel serviceViewModel)
        {
            SetPageTitle("Services");
            return View(serviceViewModel);
        }
    }
}