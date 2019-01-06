using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Models;

namespace SocialNetwork.Controllers
{
    public class HomeController : Controller
    {
        SocialNetworkContext db;
        public HomeController(SocialNetworkContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View(db.Users.ToList());
        }

        [HttpGet]
        public IActionResult Add(int Id)
        {
            ViewBag.UserId = Id;
            return View();
        }
        [HttpPost]
        public string Add(UserModel user)
        {
            db.Users.Add(user);
            db.SaveChanges();
            return "Пользователь " + user.Name + " добавлен";
        }
    }
}
