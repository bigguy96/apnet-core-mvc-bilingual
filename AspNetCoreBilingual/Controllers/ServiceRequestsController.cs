using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreBilingual.Controllers
{
    [Route("servicerequests-demandesdeservice")]
    public class ServiceRequestsController : BaseController
    {
        // GET
        [HttpGet("dashboard-tableaudebord")]
        public IActionResult Index()
        {
            SetPageTitle("Service Requests");
            return View();
        }
    }
}