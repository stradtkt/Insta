using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Insta.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Insta.Controllers
{
    public class MessageController : Controller
    {
        private InstaContext _iContext;
        public MessageController(InstaContext context)
        {
            _iContext = context;
        }
        private User ActiveUser 
        {
            get 
            {
                return _iContext.users.Where(u => u.user_id == HttpContext.Session.GetInt32("user_id")).FirstOrDefault();
            }
        }

        [HttpGet("Messages")]
        public IActionResult Messages()
        {
            List<UsersMessages> messages = _iContext.user_has_messages
                .Include(u => u.User)
                .ThenInclude(u => u.UsersMessages)
                .Include(m => m.Message)
                .ThenInclude(m => m.UsersMessages)
                .OrderBy(m => m.Message)
                .ToList();
            ViewBag.messages = messages;
            return View();
        }
        [HttpGet("Message/{message_id}")]
        public IActionResult Message(int message_id)
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Login", "Home");
            }
            UsersMessages message = _iContext.user_has_messages
                .Include(m=> m.Message)
                .Where(m => m.message_id == message_id)
                .Include(m => m.User)
                .SingleOrDefault();
            ViewBag.user = ActiveUser;
            ViewBag.message = message;
            return View();
        }
        [HttpPost("Message/{message_id}/PostMessage")]
        public IActionResult PostMessage(int message_id, int user_id, string msg)
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Login", "Home");
            }
            Message newMessage = new Message
            {
                message = msg
            };
            _iContext.messages.Add(newMessage);
            UsersMessages message = new UsersMessages
            {
                message_id = message_id,
                user_id = user_id
            };
            _iContext.user_has_messages.Add(message);
            _iContext.SaveChanges();
            ViewBag.user = ActiveUser;
            return Redirect("/Message/" + message_id);
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}