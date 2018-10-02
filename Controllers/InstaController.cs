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
    public class InstaController : Controller
    {
        private InstaContext _iContext;
        public InstaController(InstaContext context)
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
        [HttpGet("Dashboard")]
        public IActionResult Dashboard()
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Login", "Home");
            }
            List<Photo> photos = _iContext.photos
                .Include(u => u.User)
                .Include(c => c.Comments)
                .Include(l => l.Likes)
                .ToList();
            ViewBag.user = ActiveUser;
            ViewBag.photos = photos;
            return View();
        }
        [HttpGet("AddNewPhoto")]
        public IActionResult AddNewPhoto()
        {
            if(ActiveUser == null) 
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.user = ActiveUser;
            return View();
        }
        [HttpPost("ProcessNewPhoto")]
        public IActionResult ProcessNewPhoto(AddPhoto add)
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if(ModelState.IsValid)
            {
                Photo photo = new Photo
                {
                    image = add.image,
                    img_alt = add.img_alt,
                    desc = add.desc
                };
                _iContext.photos.Add(photo);
                _iContext.SaveChanges();
                return RedirectToAction("Dashboard", "Insta");
            }
            return View("AddNewPhoto");
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}