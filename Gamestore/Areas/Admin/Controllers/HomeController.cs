using Microsoft.AspNetCore.Mvc;
using Gamestore.Areas.Admin.Models;
using Gamestore.Data;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly GamestoreContext _context;

        public HomeController(GamestoreContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var gamestoreContext = _context.Game.Include(g => g.Category);
            return View(await gamestoreContext.ToListAsync());
        }
    }
}
