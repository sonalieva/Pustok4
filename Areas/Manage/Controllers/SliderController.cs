using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Pustok4.DAL;
using Pustok4.Helpers;
using Pustok4.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok4.Areas.Manage.Controllers
{
    [Area("manage")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        

        public SliderController(AppDbContext context,IWebHostEnvironment env)
        {
           _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            var model = _context.Sliders.ToList();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Slider slider)
        {
            if (!ModelState.IsValid)
                return View();
            if (slider.ImageFile != null)
            {
                if (slider.ImageFile.ContentType != "image/png" && slider.ImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "File format must be image/png or image/jpeg");
                }

                if (slider.ImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "File size must be less than 2MB");
                }
            }
            else
            {
                ModelState.AddModelError("ImageFile", "ImageFile is required!");
            }
            slider.Image = FileManager.Save(_env.WebRootPath, "uploads/sliders", slider.ImageFile);
            _context.Sliders.Add(slider);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            Slider slider = _context.Sliders.FirstOrDefault(x => x.Id == id);
            if (slider == null)
                return NotFound();
            FileManager.Delete(_env.WebRootPath, "uploads/sliders", slider.Image);
            _context.Sliders.Remove(slider);
            _context.SaveChanges();

            return Ok();
        }
        public IActionResult Edit(int id)
        {
            Slider slider = _context.Sliders.FirstOrDefault(x => x.Id == id);
            if (slider == null)
                return RedirectToAction("error", "dashboard");
            return View(slider);
            
        }
        [HttpPost]
        public IActionResult Edit(Slider slider)
        {
            Slider existslider = _context.Sliders.FirstOrDefault(x => x.Id == slider.Id);
            if (slider == null)
                return RedirectToAction("errror", "dashboard");
            if (slider.ImageFile != null)
            {
                if (slider.ImageFile.ContentType != "image/png" && slider.ImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "File format must be image/png or image/jpeg");
                }

                if (slider.ImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "File size must be less than 2MB");
                }
                if (!ModelState.IsValid)
                    return View();
                string newFileName= FileManager.Save(_env.WebRootPath, "uploads/sliders", slider.ImageFile);
                FileManager.Delete(_env.WebRootPath, "uploads/sliders", existslider.Image);
                slider.Image = newFileName;
            }

            existslider.Title = slider.Title;
            existslider.SubTitle = slider.SubTitle;
            existslider.Desc = slider.Desc;
            existslider.BtnText = slider.BtnText;
            existslider.BtnUrl = slider.BtnUrl;
            existslider.Order = slider.Order;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
