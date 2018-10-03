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
                .ThenInclude(ul => ul.User)
                .ToList();
            List<User> users = _iContext.users
                .Include(l => l.Likes)
                .Include(c => c.Comments)
                .Include(p => p.Photos)
                .ToList();
            List<Like> likes = _iContext.likes
                .Include(p => p.Photo)
                .Include(u => u.User)
                .ToList();
            ViewBag.likes = likes;
            ViewBag.users = users;
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
                    user_id = ActiveUser.user_id,
                    image = add.image,
                    img_alt = add.img_alt,
                    description = add.description
                };
                _iContext.photos.Add(photo);
                _iContext.SaveChanges();
                return RedirectToAction("Dashboard", "Insta");
            }
            return View("AddNewPhoto");
        }
        [HttpGet("DeletePhoto/{photo_id}")]
        public IActionResult DeletePhoto(int photo_id)
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Login", "Home");
            }
            Photo photo = _iContext.photos.Where(p => p.photo_id == photo_id).SingleOrDefault();
            List<Like> likes = _iContext.likes.Include(p => p.Photo).Where(p => p.photo_id == photo_id).ToList();
            List<Comment> comments = _iContext.comments.Include(p => p.Photo).Where(p => p.photo_id == photo_id).ToList();
            _iContext.likes.RemoveRange(likes);
            _iContext.comments.RemoveRange(comments);
            _iContext.photos.Remove(photo);
            _iContext.SaveChanges();
            ViewBag.user = ActiveUser;
            return RedirectToAction("Dashboard", "Insta");
        }
        [HttpGet("EditPhoto/{photo_id}")]
        public IActionResult EditPhoto(int photo_id)
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.success = TempData["success"];
            ViewBag.user = ActiveUser;
            return View();
        }
        [Route("{photo_id}/ProcessEditPhoto")]
        public IActionResult ProcessEditPhoto(int photo_id, string image, string img_alt, string description)
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Login", "Home");
            }
            Photo photo = _iContext.photos.Where(p => p.photo_id == photo_id).SingleOrDefault();
            photo.image = image;
            photo.img_alt = img_alt;
            photo.description = description;
            _iContext.SaveChanges();
            TempData["success"] = "Photo successfully edited";
            return Redirect("/EditPhoto/"+ photo_id);
        }
        [HttpGet("Comments/{photo_id}")]
        public IActionResult Comments(int photo_id)
        {
            return View();
        }
        [HttpGet("DeleteComment/{comment_id}")]
        public IActionResult DeleteComment(int comment_id)
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Login", "Home");
            }
            Comment comment = _iContext.comments.Where(c => c.comment_id == comment_id).SingleOrDefault();
            _iContext.comments.Remove(comment);
            ViewBag.user = ActiveUser;
            return RedirectToAction("Dashboard", "Insta");
        }
        [HttpPost("PostComment")]
        public IActionResult PostComment(CommentOnPhoto photo)
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if(ModelState.IsValid)
            {
                Comment comment = new Comment
                {
                    photo_id = photo.photo_id,
                    comment_id = photo.comment_id,
                    comment = photo.comment
                };
                _iContext.comments.Add(comment);
                _iContext.SaveChanges();
                return Redirect("/Comments/" + photo.photo_id);
            }
            return View("Comments/" + photo.photo_id);
        }
        [Route("LikeAPhoto/{photo_id}")]
        public IActionResult LikeAPhoto(int photo_id)
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if(ModelState.IsValid)
            {
                Like newLike = new Like
                {
                    photo_id = photo_id,
                    user_id = ActiveUser.user_id
                };
                _iContext.likes.Add(newLike);
                _iContext.SaveChanges();
                return RedirectToAction("Dashboard", "Insta");
            }
            return View("Dashboard", "Insta");
        }

        [Route("UnlikeAPhoto/{photo_id}")]
        public IActionResult UnlikeAPhoto(int photo_id, int user_id, int like_id)
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Login", "Home");
            }
            Photo photo = _iContext.photos.Where(p => p.photo_id == photo_id).SingleOrDefault();
            Like like = _iContext.likes.Include(p => p.Photo).Where(p => p.photo_id == photo_id).Include(u => u.User).Where(u => u.user_id == user_id).SingleOrDefault(u => u.like_id == like_id);
            _iContext.likes.RemoveRange(like);
            return RedirectToAction("Dashboard", "Home");
        }
        [HttpGet("ViewAllPhotos/{user_id}")]
        public IActionResult ViewAllPhotos(int user_id)
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Login", "Home");
            }
            List<Photo> photos = _iContext.photos
                .Include(u => u.User)
                .ThenInclude(u => u.Likes)
                .Include(c => c.Comments)
                .Where(u => u.user_id == user_id)
                .ToList();
            ViewBag.photos = photos;
            ViewBag.user = ActiveUser;
            return View();
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}