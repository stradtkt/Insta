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
            return View();
        }
    }
}