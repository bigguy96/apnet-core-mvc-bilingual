using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.WebUtilities;

namespace AspNetCoreBilingual.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var requestCulture = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            var currentCulture = requestCulture.RequestCulture.Culture.TwoLetterISOLanguageName;
            var alternateCulture = currentCulture.Equals("en") ? "fr" : "en";
            var uri = new Uri(Request.GetDisplayUrl());
            var queryString = QueryHelpers.ParseQuery(uri.Query);
            var items = queryString.SelectMany(keyValuePair => keyValuePair.Value, (col, value) => new KeyValuePair<string, string>(col.Key, value)).ToList();
            var fullUri = uri.GetLeftPart(UriPartial.Path);

            items.RemoveAll(x => x.Key == "lang"); //Remove all values for language
            
            var queryBuilder = new QueryBuilder(items) {{"lang", alternateCulture}};
            fullUri += queryBuilder.ToQueryString();

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