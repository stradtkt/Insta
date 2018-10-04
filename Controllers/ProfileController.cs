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
    public class ProfileController : Controller
    {
        private InstaContext _iContext;
        public ProfileController(InstaContext context)
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

        [HttpGet("Profile/{user_id}")]
        public IActionResult Profile(int user_id)
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Login", "Home");
            }
            User user = _iContext.users
                .Where(u => u.user_id == user_id)
                .SingleOrDefault();
            ViewBag.theUser = user;
            ViewBag.user = ActiveUser;
            return View();
        }

        [HttpGet("AddProfile")]
        public IActionResult AddProfile()
        {
            return View();
        }
        [HttpPost("AddProfile/{user_id}/ProcessAddProfile")]
        public IActionResult ProcessAddProfile(int user_id, string profile_img, string location, string background_img, string occupation, string skills, string recent_jobs)
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Login", "Home");
            }
            User user = _iContext.users.Where(u => u.user_id == user_id).SingleOrDefault();
            user.profile_img = profile_img;
            user.location = location;
            user.background_img = background_img;
            user.occupation = occupation;
            user.skills = skills;
            user.recent_jobs = recent_jobs;
            _iContext.SaveChanges();
            return Redirect("Profile" + user_id);
        }



        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}