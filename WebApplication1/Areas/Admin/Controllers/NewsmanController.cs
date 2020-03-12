using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Entity;
using WebApplication1.Models;

namespace WebApplication1.Areas.Admin.Controllers
{
    public class NewsmanController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();
        List<GNewsViewModel> list = new List<GNewsViewModel>();
       
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var some = _context.News.ToList();
            foreach (var i in some)
            {
                list.Add(new GNewsViewModel {Id=i.Id, Title=i.Title ,Country=i.Country ,Description=i.Description ,Image=i.Image ,Time=i.Time,Date=i.Date });

            }
            return View(list);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(NewsViewModel model, HttpPostedFileBase someFile)
        {
            if (ModelState.IsValid)
            {
                string link = string.Empty;
            string filename = Guid.NewGuid().ToString() + ".jpg";
            string image = Server.MapPath(Constants.ProductImagePath) +
                filename;
            using (Bitmap bmp = new Bitmap(someFile.InputStream))
            {
                var saveImage = ImageWorker.CreateImage(bmp, 450, 450);
                if (saveImage != null)
                {
                    saveImage.Save(image, ImageFormat.Jpeg);
                    link = Url.Content(Constants.ProductImagePath) +
                        filename;
                    string path = Url.Content(Constants.ProductImagePath);

                    var pdImage = new News
                    {
                        Image = path
                    };
                }
            }                     
                _context.News.Add(new News
                {
                    Country=model.Country,
                    Date=model.Date,
                    Description=model.Description,
                    Time=model.Time,
                    Title=model.Title,
                    Image = filename
                });
                _context.SaveChanges();
                return RedirectToAction("Index", "Newsman");
            }
            else
            {
                return View(model);
            }
        }
        public ActionResult Delete(int id)
        {

            var temp = _context.News.FirstOrDefault(t => t.Id == id);

            var path = Server.MapPath(Constants.ProductImagePath) + temp.Image;
             
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            _context.News.Remove(temp);
            _context.SaveChanges();
            return RedirectToAction("Index","Newsman");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var temp = _context.News.FirstOrDefault(t => t.Id == id);

            NewsViewModel model = new NewsViewModel()
            {
                Country = temp.Country,
                 Description = temp.Description,
                 Image = temp.Image,
                 Time = temp.Time,
                 Title = temp.Title,
                 Date = temp.Date,
                 Id = temp.Id
            };

            return View(model);
        }



        [HttpPost]
        public ActionResult Edit( NewsViewModel movie)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(movie).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index","Newsman");
            }
            return View(movie);
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