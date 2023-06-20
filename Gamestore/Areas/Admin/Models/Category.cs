using System.ComponentModel.DataAnnotations;

namespace Gamestore.Areas.Admin.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        public string? CategoryName { get; set; }

        public ICollection<Game>? Games { get; set; }
    }
}

