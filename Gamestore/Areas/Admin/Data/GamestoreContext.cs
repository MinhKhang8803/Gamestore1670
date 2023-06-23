using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Gamestore.Areas.Admin.Models;
using Gamestore.Models;

namespace Gamestore.Data
{
    public class GamestoreContext : DbContext
    {
        public GamestoreContext (DbContextOptions<GamestoreContext> options)
            : base(options)
        {
        }

        public DbSet<Gamestore.Areas.Admin.Models.Users> Users { get; set; } = default!;

        public DbSet<Gamestore.Areas.Admin.Models.Game> Game { get; set; } = default!;

        public DbSet<Gamestore.Areas.Admin.Models.Category> Category { get; set; } = default!;
        public DbSet<Gamestore.Models.CartDetail> CartDetail { get; set; } = default!;

        public DbSet<Gamestore.Models.Cart> Cart { get; set; } = default!;
    }
}
