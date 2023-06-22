using System.ComponentModel.DataAnnotations;

namespace Gamestore.Areas.Admin.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPassword { get; set; }
         
        public string? UserPhone { get; set; }

        public string? UserAddress { get; set; }
        public string? UserRole { get; set; }

    }
}

