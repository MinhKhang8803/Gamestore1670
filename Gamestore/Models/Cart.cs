using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gamestore.Areas.Admin.Models;

namespace Gamestore.Models
{
    public class Cart
	{
        [Key]
        public int CartID { get; set; }

        // Foreign key
        public int UserID { get; set; }

        // Navigation properties
        [ForeignKey("UserID")]
        public Users? Users { get; set; }
        public ICollection<CartDetail>? CartDetails { get; set; } = new List<CartDetail>();
    }
}

