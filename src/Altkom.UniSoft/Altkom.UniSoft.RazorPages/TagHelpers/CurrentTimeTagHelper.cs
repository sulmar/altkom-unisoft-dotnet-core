using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.UniSoft.RazorPages.TagHelpers
{
    public class CurrentTimeTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "strong";
            output.Content.SetHtmlContent($"Date: {DateTime.Now}");
        }
    }

     
}
