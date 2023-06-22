using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Gamestore.Models;
using Gamestore.Data;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.Controllers;

public class HomeController : Controller
{
    private readonly GamestoreContext _context;

    public HomeController(GamestoreContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var _Newgame = _context.Game.Include(p => p.Category).OrderByDescending(p => p.CreatedDate).Take(10);
        var _Hotgame = _context.Game.Include(p => p.Category).OrderByDescending(p => p.ViewCount).Take(10);
        var model = new HomeViewModel { NewGame = _Newgame.ToList(), HotGame = _Hotgame.ToList() };
        return View(model);
    }

    [Route("/GameProduct")]
    public IActionResult GameProduct()
    {
        var _Game = _context.Game.Include(p => p.Category);
        return View(_Game.ToList());
    }

    [Route("/GameProductDetails")]
    public async Task<IActionResult> GameProductDetails(int? id)
    {
        if (id == null || _context.Game == null)
        {
            return NotFound();
        }
        var Game = await _context.Game.Include(p => p.Category).FirstOrDefaultAsync(m => m.GameID == id);
        if (Game == null)
        {
            return NotFound();
        }

  

        return View(Game);

    }

}

