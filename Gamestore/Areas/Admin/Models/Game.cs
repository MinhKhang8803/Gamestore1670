using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gamestore.Models;

namespace Gamestore.Areas.Admin.Models
{
    public class Game
    {
        [Key]
        public int GameID { get; set; }
        public string? GameName { get; set; }
        public string? ShortDescription { get; set; }
        public string? GameDescription { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public string? ImageURL { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public int ViewCount { get; set; }

        // Foreign keys
        public int? CategoryID { get; set; }

        // Navigation properties
        [ForeignKey("CategoryID")]
        public Category? Category { get; set; }
        public ICollection<CartDetail>? CartDetails { get; set; }
        public ICollection<OrderDetails>? OrderDetails { get; set; }
    }
}

