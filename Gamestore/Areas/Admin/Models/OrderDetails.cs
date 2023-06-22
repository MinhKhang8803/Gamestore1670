using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamestore.Areas.Admin.Models
{
    public class OrderDetails
    {
        [Key]
        public int OrderDetailID { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        // Foreign keys
        public int OrderID { get; set; }
        public int GameID { get; set; }

        // Navigation properties
        [ForeignKey("OrderID")]
        public Order? Orders { get; set; }

        [ForeignKey("GameID")]
        public Game? Games { get; set; }
    }
}

