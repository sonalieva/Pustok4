using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok4.DAL;
using Pustok4.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok4.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeViewModels homeVM = new HomeViewModels
            {
                Sliders = _context.Sliders.OrderBy(x => x.Order).ToList(),
                DiscountedBooks = _context.Books
                               .Include(x => x.BookImages).Include(x => x.Author)
                               .Where(x => x.DiscountPercent > 0).Take(20).ToList(),
                FeaturedBooks = _context.Books
                               .Include(x => x.BookImages).Include(x => x.Author)
                               .Where(x => x.IsFeatured).Take(20).ToList(),
                NewBooks = _context.Books
                               .Include(x => x.BookImages).Include(x => x.Author)
                               .Where(x => x.IsNew).Take(20).ToList()
            };
            return View(homeVM);
        }
    }
}
