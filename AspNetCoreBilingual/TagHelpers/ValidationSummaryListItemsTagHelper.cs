using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AspNetCoreBilingual.TagHelpers
{
    public class ValidationSummaryListItemsTagHelper : TagHelper
    {
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var errors = ViewContext.ModelState
                .Where(x => x.Value.Errors.Any())
                .Select(s => new
                {
                    ControlId = s.Key,
                    s.Value.Errors.FirstOrDefault()?.ErrorMessage
                }).ToList();

            output.Content.AppendHtml(@"<div class=""alert alert-danger mrgn-tp-xl"" tabindex=""-1"">");
            output.Content.AppendHtml("<section>");

            output.Content.AppendFormat(
                errors.Count == 1
                    ? "<h2>The form could not be submitted because {0} error was found.</h2>"
                    : "<h2>The form could not be submitted because {0} errors were found.</h2>", errors.Count);

            output.Content.AppendHtml("<ul>");

            foreach (var error in errors)
            {
                output.Content.AppendFormat(@"<li><a href=""#{0}"">Error: {1}</a></li>", error.ControlId, error.ErrorMessage);
            }

            output.Content.AppendHtml("</ul>");
            output.Content.AppendHtml("</section>");
            output.Content.AppendHtml("</div>");

            if (!errors.Any())
            {
                output.SuppressOutput();
            }
        }
    }
}