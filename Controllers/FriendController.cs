using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Insta.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Insta.Controllers
{
    public class FriendController : Controller
    {
        private InstaContext _iContext;
        public FriendController(InstaContext context)
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

        [HttpGet("Users")]
        public IActionResult Users()
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Login", "Home");
            }
            List<User> users = _iContext.users
                .OrderBy(u => u.first_name)
                .ToList();
            ViewBag.users = users;
            ViewBag.user = ActiveUser;
            return View();
        }

        [HttpGet("Users/RequestUser/{user_id}")]
        public IActionResult RequestUser(int user_id)
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Login", "Home");
            }
            Friend request = new Friend
            {
                friend_id = ActiveUser.user_id,
                is_friend = 0,
                requested = 1,
                accepted_request = 0,
                user_id = user_id
            };
            _iContext.friends.Add(request);
            _iContext.SaveChanges();
            ViewBag.user = ActiveUser;
            return RedirectToAction("Users", "Friend");
        }
        [HttpGet("Users/Requests")]
        public IActionResult Requests()
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Login", "Home");
            }
            List<Friend> requests = _iContext.friends
                .Where(f => f.accepted_request == 0 &&  f.is_friend == 0)
                .ToList();
            ViewBag.requests = requests;
            ViewBag.user = ActiveUser;
            return View();
        }
        [HttpGet("Users/Requests/AcceptRequest/{user_id}")]
        public IActionResult AcceptRequest(int user_id)
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Login", "Home");
            }
            Friend requested = new Friend
            {
                friend_id = ActiveUser.user_id,
                is_friend = 1,
                requested = 1,
                accepted_request = 1
            };
            _iContext.friends.Add(requested);
            Friends newFriend = new Friends
            {
                friend_id = ActiveUser.user_id,
                user_id = user_id
            };
            _iContext.users_has_friends.Add(newFriend);
            _iContext.SaveChanges();
            return RedirectToAction("Users", "Friend");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}