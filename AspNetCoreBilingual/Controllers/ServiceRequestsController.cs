using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreBilingual.Controllers
{
    [Route("servicerequests-demandesdeservice")]
    public class ServiceRequestsController : BaseController
    {
        // GET
        [HttpGet("dashboard-tableaudebord/{id:int}")]
        public IActionResult Index(int id, string name, string other)
        {
            SetPageTitle("Service Requests");
            return View();
        }
    }
}