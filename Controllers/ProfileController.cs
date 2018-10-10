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
        [HttpGet("Profile/{user_id}/DeleteSkill/{skill_id}")]
        public IActionResult DeleteSkill(int skill_id, int user_id)
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Login", "Home");
            }
            Skills skill = _iContext.skills.Where(s => s.skill_id == skill_id).SingleOrDefault(s => s.user_id == user_id);
            _iContext.skills.Remove(skill);
            _iContext.SaveChanges();
            return Redirect("/Profile/"+user_id);
        }
        [HttpGet("Profile/{user_id}/DeleteJob/{job_id}")]
        public IActionResult DeleteJob(int job_id, int user_id)
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Login", "Home");
            }
            Job job = _iContext.jobs.Where(j => j.job_id == job_id).SingleOrDefault(j => j.user_id == user_id);
            _iContext.jobs.Remove(job);
            _iContext.SaveChanges();
            return Redirect("/Profile/"+user_id);
        }
        [HttpGet("Profile/{user_id}/EditJob/{job_id}")]
        public IActionResult EditJob(int job_id, int user_id)
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Login", "Home");
            }
            Job job =_iContext.jobs
                .Include(u => u.User)
                .Where(j => j.job_id == job_id)
                .Where(j => j.user_id == user_id)
                .SingleOrDefault();
            ViewBag.EditJob = job;
            ViewBag.user = ActiveUser;
            return View();
        }
        [HttpGet("Profile/{user_id}/EditSkill/{skill_id}")]
        public IActionResult EditSkill(int skill_id, int user_id)
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Login", "Home");
            }
            Skills skill =_iContext.skills
                .Include(u => u.User)
                .Where(s => s.skill_id == skill_id)
                .Where(s => s.user_id == user_id)
                .SingleOrDefault();
            ViewBag.EditSkill = skill;
            ViewBag.user = ActiveUser;
            return View();
        }
        [HttpPost("Profile/{user_id}/EditSkill/{skill_id}/ProcessEditSkill")]
        public IActionResult ProcessEditSkill(int skill_id, int user_id, string skill_title, string skill_description, int skill_level)
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Login", "Home");
            }
            Skills skill = _iContext.skills
                .Include(s => s.User)
                .Where(s => s.skill_id == skill_id)
                .SingleOrDefault(s => s.user_id == user_id);
            skill.skill_title = skill_title;
            skill.skill_description = skill_description;
            skill.skill_level = skill_level;
            _iContext.SaveChanges();
            return Redirect("/Profile/"+user_id);
        }
        [HttpPost("Profile/{user_id}/EditJob/{job_id}/ProcessEditJob")]
        public IActionResult ProcessEditJob(int user_id, int job_id, int job_rating, string job_title, string job_description, DateTime from_date, DateTime to_date)
        {
            Job job = _iContext.jobs.Include(u => u.User).Where(j => j.job_id == job_id).SingleOrDefault(j => j.user_id == user_id);
            job.job_rating = job_rating;
            job.job_title = job_title;
            job.job_description = job_description;
            job.from_date = from_date;
            job.to_date = to_date;
            _iContext.SaveChanges();
            return Redirect("/Profile/" + user_id);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}