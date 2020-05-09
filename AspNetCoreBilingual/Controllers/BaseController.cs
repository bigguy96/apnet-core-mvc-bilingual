using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetCoreBilingual.Controllers
{
    public class BaseController : Controller
    {
        // GET
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var culture = context.RouteData.Values["culture"].ToString();
            var alternateCulture = culture.Equals("en") ? "fr" : "en";
            var path = string.Empty;

            if (context.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
            {
                var actionAttributes = controllerActionDescriptor.MethodInfo.GetCustomAttributes(inherit: true)
                    .Cast<HttpGetAttribute>()
                    .SingleOrDefault(x => !x.Template.Contains(culture));

                var regex = new Regex(@"/{(.*?)}");
                if (actionAttributes != null)
                {
                    path = regex.Replace(actionAttributes.Template, "");
                }
            }

            ViewData["Toggle"] = $"/{alternateCulture}{path}";

            base.OnActionExecuting(context);
        }
    }
}