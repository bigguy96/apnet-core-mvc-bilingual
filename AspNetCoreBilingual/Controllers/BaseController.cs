using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.WebUtilities;

namespace AspNetCoreBilingual.Controllers
{
    public class BaseController : Controller
    {
        // GET
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var requestCulture = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            var currentCulture = requestCulture.RequestCulture.Culture.TwoLetterISOLanguageName;
            var alternateCulture = currentCulture.Equals("en") ? "fr" : "en";
            var baseUri = Request.GetDisplayUrl();
            var uri = new Uri(Request.GetDisplayUrl());
            var query = QueryHelpers.ParseQuery(uri.Query);
            var items = query.SelectMany(x => x.Value, (col, value) => new KeyValuePair<string, string>(col.Key, value)).ToList();
            var fullUri = baseUri;

            if (Request.Query.ContainsKey("lang"))
            {
                items.RemoveAll(x => x.Key == "lang"); // Remove all values for key
                var qb = new QueryBuilder(items) { { "lang", alternateCulture } };

                fullUri += qb.ToQueryString();
            }
            else
            {
                var qb = new QueryBuilder { { "lang", alternateCulture } };
                fullUri += qb.ToQueryString();
            }

            //if (context.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
            //{
            //    //var actionAttributes = controllerActionDescriptor.MethodInfo.GetCustomAttributes(inherit: true)
            //    //    .Cast<HttpGetAttribute>()
            //    //    .SingleOrDefault(x => !x.Template.Contains(culture));

            //    //var regex = new Regex(@"/{(.*?)}");
            //    //if (actionAttributes != null)
            //    //{
            //    //    path = regex.Replace(actionAttributes.Template, "");
            //    //}
            //}

            ViewData["Toggle"] = fullUri;

            base.OnActionExecuting(context);
        }

        protected void SetPageTitle(string title)
        {
            ViewData["PageTitle"] = title;
        }
    }
}


//https://benjii.me/2017/04/parse-modify-query-strings-asp-net-core/