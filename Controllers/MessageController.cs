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

        [HttpGet("Messages/{user_id}")]
        public IActionResult Messages(int user_id)
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Login", "Home");
            }
            User user = _iContext.users.Where(u => u.user_id == user_id).SingleOrDefault();
            List<Message> message_users = _iContext.messages
                .Include(f => f.From)
                .ThenInclude(f => f.Froms)
                .Include(t => t.To)
                .ThenInclude(t => t.Tos)
                .ToList();
            ViewBag.messages = message_users;
            ViewBag.user = ActiveUser;
            return View();
        }
        [HttpGet("/{to_id}/Message/{from_id}")]
        public IActionResult Message(int from_id, int to_id)
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Login", "Home");
            }
            Message message = _iContext.messages
                .Include(f => f.From)
                .Where(f => f.from_id == from_id)
                .SingleOrDefault();
            List<Message> users_messages = _iContext.messages
                .Include(f => f.From)
                .ThenInclude(f => f.Froms)
                .Where(f => f.from_id == from_id)
                .Include(t => t.To)
                .ThenInclude(t => t.Tos)
                .Where(t => t.to_id == to_id)
                .ToList();
            ViewBag.user = ActiveUser;
            ViewBag.all_messages = users_messages;
            ViewBag.message = message;
            return View();
        }
        [HttpPost("/{to_id}/Message/{from_id}/ProcessMessage")]
        public IActionResult ProcessMessage(int to_id, int from_id, string message)
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if(ModelState.IsValid)
            {
                Message msg = new Message
                {
                    to_id = to_id,
                    from_id = from_id,
                    message = message
                };
                _iContext.messages.Add(msg);
                _iContext.SaveChanges();
                return Redirect("/" + to_id + "/Message/" + from_id + "/");
            }
            return Redirect("/" + to_id + "/Message/" + from_id + "/");
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}