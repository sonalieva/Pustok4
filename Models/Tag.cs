using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok4.Models
{
    public class Tag
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(35)]

        public string Name { get; set; }
        public List<BookTag> BookTags { get; set; }
    }
}
