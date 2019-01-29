using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Localization;

namespace SocialNetwork.Components
{
    public class MenuPartsViewComponent : ViewComponent
    {
        private readonly IStringLocalizer<MenuPartsViewComponent> _localizer;

        public MenuPartsViewComponent(IStringLocalizer<MenuPartsViewComponent> localizer)
        {
            _localizer = localizer;
        }

        public IViewComponentResult Invoke()
        {
            if (HttpContext.User.Identity.Name == null)
            {
                var log_in = Url.Action("Login", "Account");
                var html = @"<li><a href=""{0}"" class=""navbar-brand"">" + _localizer["login"] + "</a></li>";
                return new HtmlContentViewComponentResult(
                    new HtmlString(string.Format(html, log_in)));
            }
            else
            {
                var log_out = Url.Action("Logout", "Account");
                var html = @"<li><a href=""{0}"" class=""navbar-brand"">" + _localizer["logout"] + "</a></li>";
                return new HtmlContentViewComponentResult(
                    new HtmlString(string.Format(html, log_out)));
            }
        }
    }
}
