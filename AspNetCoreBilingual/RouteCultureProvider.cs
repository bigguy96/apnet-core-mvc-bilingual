using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace AspNetCoreBilingual
{
    public class RouteCultureProvider : IRequestCultureProvider
    {
        public Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            var cultureQuery = httpContext.Request.Query["lang"].FirstOrDefault();
            if (cultureQuery == null)
            {
                return Task.FromResult((ProviderCultureResult)null);
            }

            var requestedCultureName = cultureQuery;
            var locOptions = httpContext.RequestServices.GetService<IOptions<RequestLocalizationOptions>>();
            var requestedCulture = locOptions.Value.SupportedCultures.FirstOrDefault(x => x.Name.Equals(requestedCultureName, StringComparison.OrdinalIgnoreCase));

            if (requestedCulture == null)
            {
                requestedCultureName = locOptions.Value.DefaultRequestCulture.Culture.Name;
            }

            httpContext.Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(requestedCultureName)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return Task.FromResult(new ProviderCultureResult(requestedCultureName, requestedCultureName));
        }
    }
}