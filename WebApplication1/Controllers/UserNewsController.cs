using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class UserNewsController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();
        List<GNewsViewModel> list = new List<GNewsViewModel>();

   
        public ActionResult Index(string searchText)
        {
            var some = _context.News.ToList();
            if (searchText == null)
            {
                foreach (var i in some)
                {
                    list.Add(new GNewsViewModel { Id = i.Id, Title = i.Title, Country = i.Country, Description = i.Description, Image = i.Image, Time = i.Time, Date = i.Date });

                }
            }
            else
            {
                foreach (var i in some)
                {
                    if (i.Title.Contains(searchText))
                    {
                        list.Add(new GNewsViewModel { Id = i.Id, Title = i.Title, Country = i.Country, Description = i.Description, Image = i.Image, Time = i.Time, Date = i.Date });
                    }

                }
            }
           
            return View(list);
        }
        public ActionResult Details(int id)
        {
            var temp = _context.News.FirstOrDefault(t => t.Id == id);

            if (temp == null)
                return View("NotFound");
            else
                return View(temp);
        }

      



    }
}