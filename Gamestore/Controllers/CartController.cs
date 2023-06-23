using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Gamestore.Data;
using Gamestore.Areas.Admin.Models;
using Gamestore.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.Controllers
{
    public class CartController : Controller
    {
        private readonly GamestoreContext _context;

        public CartController(GamestoreContext context)
        {
            _context = context;
        }

        // Method to add a product to the cart.
        [Route("/AddToCart")]
        [HttpPost]
        public async Task<IActionResult> AddToCart(int gameId, int quantity)
        {

            if (User.Identity.IsAuthenticated)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserEmail == User.Identity.Name);

                if (user != null)
                {
                    var cart = await _context.Cart.Include(c => c.CartDetails)
                        .FirstOrDefaultAsync(c => c.UserID == user.UserId);

                    if (cart == null)
                    {
                        cart = new Cart
                        {
                            UserID = user.UserId,
                            CartDetails = new List<CartDetail>()
                        };
                        _context.Cart.Add(cart);
                    }

                    var cartDetail = cart.CartDetails.FirstOrDefault(cd => cd.GameID == gameId);

                    if (cartDetail != null)
                    {
                        cartDetail.Quantity += quantity;
                    }
                    else
                    {
                        cartDetail = new CartDetail
                        {
                            CartID = cart.CartID,
                            GameID = gameId,
                            Quantity = quantity
                        };
                        cart.CartDetails.Add(cartDetail);
                    }
                    await _context.SaveChangesAsync();

                    return RedirectToAction("CartDetails", "Cart");
                }
            }

            return RedirectToAction("Login", "Account");
        }

        [Route("/CartDetails")]
        public IActionResult CartDetails()
        {
            var user = _context.Users.FirstOrDefault(u => u.UserEmail == User.Identity.Name);

            if (user != null)
            {
                var cart = _context.Cart.Include(c => c.CartDetails).ThenInclude(cd => cd.Game)
                    .FirstOrDefault(c => c.UserID == user.UserId);

                if (cart != null)
                {
                    ViewBag.CartQuantity = cart.CartDetails.Sum(cd => cd.Quantity);
                    return View(cart);
                }
            }

         
            ViewBag.CartQuantity = 0;
            return PartialView("_EmptyCart");
        } 

        [Route("/UpdateQuantity")]
        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int gameId, int quantity)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserEmail == User.Identity.Name);
            if (user != null)
            {
                var cart = await _context.Cart.Include(c => c.CartDetails)
                    .FirstOrDefaultAsync(c => c.UserID == user.UserId);
                if (cart != null)
                {
                    var cartDetail = cart.CartDetails.FirstOrDefault(cd => cd.GameID == gameId);
                    if (cartDetail != null)
                    {
                        cartDetail.Quantity = quantity;
                        await _context.SaveChangesAsync();
                    }
                }
            }
            return RedirectToAction("CartDetails", "Cart");
        }

        [Route("/RemoveFromCart")]
        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int gameId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserEmail == User.Identity.Name);
            if (user != null)
            {
                var cart = await _context.Cart.Include(c => c.CartDetails)
                    .FirstOrDefaultAsync(c => c.UserID == user.UserId);
                if (cart != null)
                {
                    var cartDetail = cart.CartDetails.FirstOrDefault(cd => cd.GameID == gameId);
                    if (cartDetail != null)
                    {
                        cart.CartDetails.Remove(cartDetail);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            return RedirectToAction("CartDetails", "Cart");
        }



    }
}
