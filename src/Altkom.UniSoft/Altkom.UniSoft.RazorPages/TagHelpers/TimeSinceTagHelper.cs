using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.UniSoft.RazorPages.TagHelpers
{
    public interface ITimeSinceService
    {
        string TimeSince(DateTime dateTime);
    }

    public class TimeSinceService : ITimeSinceService
    {
        public string TimeSince(DateTime dateTime)
        {
            return PeriodOfTimeOutput(DateTime.Now.Subtract(dateTime));
        }

        private string PeriodOfTimeOutput(TimeSpan timeSpan)
        {
            // C# 8.0
            string how_long_ago = timeSpan switch
            {
                var t when t.Days > 1 => $"{t.Days} days ago",
                var t when t.Days == 1 => $"{t.Days} days ago",
                var t when t.Hours >= 1 => $"{t.Hours} hours ago",
                var t when t.Minutes >= 1 => $"{t.Minutes} min ago",
                var t when t.Seconds >= 1 => $"{t.Minutes} sec ago",
                _ => "ago" // default value
            };

            return how_long_ago;

        }
    }

    [HtmlTargetElement("time-since")]
    public class TimeSinceTagHelper : TagHelper
    {
        public string CompareDateTime { get; set; }

        private readonly ITimeSinceService timeSinceService;

        public TimeSinceTagHelper(ITimeSinceService timeSinceService)
        {
            this.timeSinceService = timeSinceService;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";

            output.Content
                .SetContent(timeSinceService.TimeSince(
                    Convert.ToDateTime(CompareDateTime)));
        }
    }
}
