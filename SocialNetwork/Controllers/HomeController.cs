using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Models;

namespace SocialNetwork.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        SocialNetworkContext db;
        public HomeController(SocialNetworkContext context)
        {
            db = context;
        }
        
        public IActionResult Index()
        {
            var user = db.Users.First(u => u.Name == HttpContext.User.Identity.Name);
            return RedirectPermanent($"~/Home/User/{user.Id}");
        }

        public IActionResult User(int Id)
        {
            var user = db.Users.FirstOrDefault(u => u.Id == Id);
            return View(user);
        }
        
        public IActionResult Users()
        {
            return View(db.Users.ToList());
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var user = db.Users.FirstOrDefault(u => u.Id == Id);
            return View(user);
        }
        [HttpPost]
        public IActionResult Edit(UserModel user)
        {
            db.Users.Update(user);
            db.SaveChanges();
            return RedirectPermanent($"~/Home/User/{user.Id}");
        }
    }
}
