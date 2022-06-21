using Pustok4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok4.ViewModels
{
    public class HomeViewModels
    {
        public List<Slider> Sliders { get; set; }
        public List<Book> DiscountedBooks { get; set; }
        public List<Book> FeaturedBooks { get; set; }
        public List<Book> NewBooks { get; set; }
    }
}
