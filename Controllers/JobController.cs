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
    public class JobController : Controller
    {
        private InstaContext _iContext;
        public JobController(InstaContext context)
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

        [HttpGet("JobCategories")]
        public IActionResult JobCategories()
        {
            if(ActiveUser == null) 
            {
                return RedirectToAction("Login", "Home");
            }
            List<JobCategory> jobCategories = _iContext.job_categories.ToList();
            ViewBag.categories = jobCategories;
            ViewBag.user = ActiveUser;
            return View();
        }
        [HttpGet("AddCategoryToJob/{list_id}")]
        public IActionResult AddCategoryToJob(int list_id) 
        {
            if(ActiveUser == null) 
            {
                return RedirectToAction("Login", "Home");
            }
            Jobs single = _iContext.jobs_list.SingleOrDefault(j => j.list_id == list_id);
            List<JobCategory> jobCategories = _iContext.job_categories.ToList();
            ViewBag.categories = jobCategories;
            ViewBag.job = single;
            ViewBag.user = ActiveUser;
            return View();
        }
        [HttpGet("AddJobToCategory/{category_id}")]
        public IActionResult AddJobToCategory(int category_id) 
        {
            if(ActiveUser == null) 
            {
                return RedirectToAction("Login", "Home");
            }
            JobCategory category = _iContext.job_categories.SingleOrDefault(c => c.category_id == category_id);
            List<Jobs> jobs = _iContext.jobs_list.ToList();
            ViewBag.jobs = jobs;
            ViewBag.category = category;
            ViewBag.user = ActiveUser;
            return View();
        }
        [HttpPost("AddJobToCategory/{category_id}/Job/{list_id}")]
        public IActionResult JobToCategory(int list_id, int category_id)
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Login", "Home");
            }
            JobsHasCategories newCategory = new JobsHasCategories
            {
                list_id = list_id,
                category_id = category_id
            };
            return Redirect("/AddJobToCategory/" + category_id);
        }
    }
}