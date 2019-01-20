using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace SocialNetwork.Components
{
    public class MenuPartsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            if (HttpContext.User.Identity.Name == null)
            {
                var login = Url.Action("Login", "Account");
                var html = @"<li><a href=""{0}"" class=""navbar-brand"">Войти</a></li>";
                return new HtmlContentViewComponentResult(
                    new HtmlString(string.Format(html, login)));
            }
            else
            {
                var logout = Url.Action("Logout", "Account");
                var html = @"<li><a href=""{0}"" class=""navbar-brand"">Выйти</a></li>";
                return new HtmlContentViewComponentResult(
                    new HtmlString(string.Format(html, logout)));
            }
        }
    }
}
