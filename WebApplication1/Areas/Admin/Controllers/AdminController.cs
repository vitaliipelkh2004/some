using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Areas.Admin.Models;
using WebApplication1.Models;

namespace WebApplication1.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();
        List<UserViewModel> list = new List<UserViewModel>();
       
        
        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var listU = _context.Users.ToList();
            foreach (var i in listU)
            {
                list.Add(new UserViewModel { Id = i.Id, Name = i.UserName, Email = i.Email,Password=i.PasswordHash });
            }
            return View(list);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var some = _context.Users.ToList();
                _context.Users.Add(new ApplicationUser
                {
                  Email=model.Email,
                  UserName=model.Name,
                  PasswordHash=model.Password                   
                });
                _context.SaveChanges();
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return View(model);
            }
        }
    }
}