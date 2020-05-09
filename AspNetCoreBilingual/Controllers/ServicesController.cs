using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreBilingual.Controllers
{
    public class ServicesController : BaseController
    {
        // GET
        [HttpGet("/{culture:regex(en)}/dashboard/services")]
        [HttpGet("/{culture:regex(fr)}/tableaudebord/services")]
        public IActionResult Index()
        {
            return View();
        }
    }
}