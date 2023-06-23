using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Gamestore.Areas.Admin.Models;

namespace Gamestore.Models
{
    public class CartDetail
	{
        [Key]
        public int CartDetailID { get; set; }
        public int Quantity { get; set; }

        // Foreign keys
        public int CartID { get; set; }
        public int GameID { get; set; }

        // Navigation properties
        [ForeignKey("CartID")]
        public Cart? Cart { get; set; }

        [ForeignKey("GameID")]
        public Game? Game { get; set; }
    }
}

