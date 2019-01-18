﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
            return Redirect($"~/Home/UserProfile/{user.Id}");
        }

        public IActionResult UserProfile(int Id)
        {
            ViewBag.is_auth_user = IsAuthorizedUser(Id);
            ViewBag.is_friend = IsFriend(Id);
            var user = db.Users.Include("Friends").First(u => u.Id == Id);
            return View(user);
        }
        
        public IActionResult AddFriend(int Id)
        {
            var user = db.Users.Include("Friends").First(u => u.Name == HttpContext.User.Identity.Name);
            var new_friend = db.Users.Include("Friends").First(u => u.Id == Id);
            if (user.Friends == null)
            {
                user.Friends = new List<UserModel>();
            }
            if (new_friend.Friends == null)
            {
                new_friend.Friends = new List<UserModel>();
            }
            if (user.Id != new_friend.Id && !user.Friends.Contains(new_friend))
            {
                user.Friends.Add(new_friend);
                new_friend.Friends.Add(user);
                db.Users.Update(user);
                db.Users.Update(new_friend);
                db.SaveChanges();
            }
            return Redirect($"~/Home/UserProfile/{new_friend.Id}");
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
            return Redirect($"~/Home/UserProfile/{user.Id}");
        }

        protected bool IsAuthorizedUser(int Id)
        {
            var user = db.Users.First(u => u.Name == HttpContext.User.Identity.Name);
            if (user.Id == Id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected bool IsFriend(int Id)
        {
            var user = db.Users.Include("Friends").First(u => u.Name == HttpContext.User.Identity.Name);
            var other_user = db.Users.Include("Friends").First(u => u.Id == Id);
            if (user.Id != Id && !user.Friends.Contains(other_user))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
