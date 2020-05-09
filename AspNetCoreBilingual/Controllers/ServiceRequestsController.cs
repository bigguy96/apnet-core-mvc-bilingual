using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreBilingual.Controllers
{
    public class ServiceRequestsController : BaseController
    {
        // GET
        [HttpGet("/{culture:regex(en)}/dashboard/servicerequests")]
        [HttpGet("/{culture:regex(fr)}/tableaudebord/demandesdeservice")]
        public IActionResult Index()
        {
            SetPageTitle("Service Requests");
            return View();
        }
    }
}