using System;
using System.Collections.Generic;
using BowlingLeague.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BowlingLeague.Infrastructure
{

    [HtmlTargetElement("div", Attributes = "page-info")]
    public class PaginationTagHelper : TagHelper
    {
        private IUrlHelperFactory urlInfo;

        public PaginationTagHelper(IUrlHelperFactory uhf)
        {
            urlInfo = uhf;
        }


        //tag helpers that will set the team as highlighted
        public bool PageClassesEnabled { get; set; } = false;
        public string PageClass { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }
        public string TeamCategory { get; set; }


        public PageNumberingInfo pageInfo { get; set; }

        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]

        public Dictionary<string, object> KeyValuePairs { get; set; } = new Dictionary<string, object>();

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlInfo.GetUrlHelper(ViewContext);

            TagBuilder finishedTag = new TagBuilder("div");

            for (int i = 1; i <= pageInfo.NumPages; i++)
            {
                TagBuilder individualTag = new TagBuilder("a");

                KeyValuePairs["pageNum"] = 1;
                
                individualTag.Attributes["href"] = urlHelper.Action(i.ToString());

                //This is my attempt to getting the teams names to be selected :(
                if (PageClassesEnabled)
                {
                    individualTag.AddCssClass(PageClass);

                    //This highlights the page that is selected. if true selected else normal -
                    //the PageClassSelected is a class that we have created and is set in the Index.
                    //In the Index page we made the PageClassSelected = "btn-light"
                    // and in the Index page we made the PageClassNormal = "btn-outline-light"
                    //Then the buttons change depending on where you are/ what the i is equal to
                    individualTag.AddCssClass(i == pageInfo.CurrentPage ? PageClassSelected : PageClassNormal);
                };

                //Add all the parts together through the append method

                individualTag.InnerHtml.Append(i.ToString());

                finishedTag.InnerHtml.AppendHtml(individualTag);
            }
            output.Content.AppendHtml(finishedTag.InnerHtml);
        }
    }
}
