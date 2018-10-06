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
            List<Photo> photos = _iContext.photos
                .Include(u => u.User)
                .Where(u => u.user_id == user_id)
                .ToList();
            User user = _iContext.users
                .Where(u => u.user_id == user_id)
                .SingleOrDefault();
            List<Job> jobs = _iContext.jobs
                .Include(u => u.User)
                .Where(u => u.user_id == user_id)
                .ToList();
            List<Skills> skills = _iContext.skills
                .Include(u => u.User)
                .Where(u => u.user_id == user_id)
                .ToList();
            ViewBag.photos = photos;
            ViewBag.skills = skills;
            ViewBag.jobs = jobs;
            ViewBag.theUser = user;
            ViewBag.user = ActiveUser;
            return View();
        }

        [HttpGet("AddProfile")]
        public IActionResult AddProfile()
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.user = ActiveUser;
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
            return Redirect("/Profile/" + user_id);
        }

        [HttpGet("Profile/{user_id}/EditProfile")]
        public IActionResult EditProfile(int user_id, string profile_img, string location, string background_img, string occupation, string skills, string recent_jobs)
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Login", "Home");
            }
            User user = _iContext.users.Where(u => u.user_id == user_id).SingleOrDefault();
            ViewBag.user = ActiveUser;
            ViewBag.theUser = user;
            return View();
        }

        [HttpPost("Profile/{user_id}/EditProfile/ProcessEditProfile")]
        public IActionResult ProcessEditProfile(int user_id, string profile_img, string location, string background_img, string occupation, string skills, string recent_jobs)
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
            return Redirect("/Profile/" + user_id);
        }

        [HttpGet("Profile/{user_id}/AddJobToProfile")]
        public IActionResult AddJobToProfile(int user_id)
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Login", "Home");
            }
            User user = _iContext.users.Where(u => u.user_id == user_id).SingleOrDefault();
            ViewBag.current_user = user;
            ViewBag.user = ActiveUser;
            return View();
        }
        [HttpPost("ProcessAddJob")]
        public IActionResult ProcessAddJob(AddJob job)
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if(ModelState.IsValid)
            {
                if(job.to_date >= DateTime.Now)
                {
                    ViewBag.to_date = "Present";
                }
                Job newJob = new Job
                {
                    user_id = ActiveUser.user_id,
                    job_title = job.job_title,
                    job_rating = job.job_rating,
                    job_description = job.job_description,
                    company = job.company,
                    from_date = job.from_date,
                    to_date = job.to_date,
                    skills = job.skills
                };
                _iContext.jobs.Add(newJob);
                _iContext.SaveChanges();
                ViewBag.user = ActiveUser;
                return Redirect("/Profile/"+ActiveUser.user_id);
            }
            return View("AddJobToProfile", "Profile");
        }
        [HttpGet("Profile/{user_id}/AddSkillsToProfile")]
        public IActionResult AddSkillsToProfile(int user_id)
        {
            User user = _iContext.users.Where(u => u.user_id == user_id).SingleOrDefault();
            ViewBag.current_user = user;
            return View();
        }
        [HttpPost("ProcessAddSkill")]
        public IActionResult ProcessAddSkill(AddSkill skill)
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if(ModelState.IsValid)
            {
                Skills newSkill = new Skills
                {
                    user_id = ActiveUser.user_id,
                    skill_title = skill.skill_title,
                    skill_description = skill.skill_description,
                    skill_level = skill.skill_level
                };
                _iContext.skills.Add(newSkill);
                _iContext.SaveChanges();
                return Redirect("/Profile/"+ActiveUser.user_id);
            }
            return View("AddSkillsToProfile", "Profile");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}