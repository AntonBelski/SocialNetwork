using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

public class ContactsTagHelper : TagHelper
{
    private const string address = "https://vk.com/anton_belsky";
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "a";
        output.Attributes.SetAttribute("href", address);
        output.Content.SetContent("Страница в вк");
    }
}