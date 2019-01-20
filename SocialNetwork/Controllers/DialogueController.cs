using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Models;
using Microsoft.EntityFrameworkCore;


namespace SocialNetwork.Controllers
{
    [Authorize]
    public class DialogueController : Controller
    {
        SocialNetworkContext db;
        public DialogueController(SocialNetworkContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult UserDialogue(string login)
        {
            var user1 = db.Users.Include("Dialogues").Include("Friends").First(u => u.Login == HttpContext.User.Identity.Name);
            ViewBag.auth_user_id = user1.Id;
            ViewBag.auth_user_name = user1.Name;
            var user2 = db.Users.Include("Dialogues").Include("Friends").First(u => u.Login == login);
            var user_dialogue = db.Dialogues.Include("Content").FirstOrDefault(d => (d.User1_Id == user1.Id && d.User2_Id == user2.Id)
                                                                 || (d.User1_Id == user2.Id && d.User2_Id == user1.Id));
            if (user1.Dialogues == null)
            {
                user1.Dialogues = new List<DialogueModel>();
            }
            if (user2.Dialogues == null)
            {
                user2.Dialogues = new List<DialogueModel>();
            }
            if (user_dialogue == null)
            {
                DialogueModel new_dialogue = new DialogueModel
                {
                    User1_Id = user1.Id,
                    User2_Id = user2.Id,
                    Content = new List<MessageModel>()
                };
                db.Dialogues.Add(new_dialogue);
                user1.Dialogues.Add(new_dialogue);
                user2.Dialogues.Add(new_dialogue);
                db.Users.Update(user1);
                db.Users.Update(user2);
                db.SaveChanges();
                return View(new_dialogue);
            }
            return View(user_dialogue);
        }
        [HttpPost]
        public IActionResult UserDialogue(int user1_id, int user2_id, string message)
        {
            var auth_user = db.Users.First(u => u.Login == HttpContext.User.Identity.Name);
            var user1 = db.Users.Include("Dialogues").Include("Friends").First(u => u.Id == user1_id);
            var user2 = db.Users.Include("Dialogues").Include("Friends").First(u => u.Id == user2_id);
            var user_dialogue = db.Dialogues.Include("Content").FirstOrDefault(d => (d.User1_Id == user1.Id && d.User2_Id == user2.Id)
                                                                 || (d.User1_Id == user2.Id && d.User2_Id == user1.Id));
            var new_message = new MessageModel { UserId = auth_user.Id, Message = message };
            db.Messages.Add(new_message);
            user_dialogue.Content.Add(new_message);
            db.Dialogues.Update(user_dialogue);
            db.Users.Update(user1);
            db.Users.Update(user2);
            db.SaveChanges();
            if (auth_user.Id == user1.Id)
            {
                return RedirectToAction("UserDialogue", new { login = user2.Login });
            }
            else
            {
                return RedirectToAction("UserDialogue", new { login = user1.Login });
            }
        }
    }
}
