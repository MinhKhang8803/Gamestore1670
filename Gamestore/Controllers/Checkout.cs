using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Gamestore.Data;
using Gamestore.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Gamestore.Models;
using Microsoft.EntityFrameworkCore;
using Gamestore.Areas.Admin.Models;
using Gamestore.Models;

namespace Gamestore.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly GamestoreContext _context;

        public CheckoutController(GamestoreContext context)
        {
            _context = context;
        }

        // GET: Checkout
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserEmail == User.Identity.Name);

            var cart = await _context.Cart
                .Include(c => c.CartDetails)
                .ThenInclude(cd => cd.Game)
                .FirstOrDefaultAsync(m => m.UserID == user.UserId);

            if (cart == null)
            {
                // User does not have a cart, redirect them somewhere appropriate
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            return View(cart);
        }

        // POST: Checkout

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> PlaceOrder([Bind("CartID,UserID")] Cart cart, bool SaveAddress = false)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserEmail == User.Identity.Name);

            if (ModelState.IsValid)
            {
                var cardDetails = await _context.CartDetail.Include(cd => cd.Game)
                    .Where(cd => cd.CartID == cart.CartID)
                    .ToListAsync();

                cart.CartDetails = cardDetails;

                var totalOrderPrice = cart.CartDetails.Sum(cd => cd.Quantity * cd.Game.Price);
                // Create new Order
                var order = new Order
                {
                    DateCreated = DateTime.Now,
                    UserID = user.UserId,
                    TotalPrice = totalOrderPrice,
                    OrderStatus = "Pending",
                    CartID = cart.CartID
                };

                _context.Add(order);
                await _context.SaveChangesAsync();



                foreach (var cartDetail in cart.CartDetails)
                {
                    // Create OrderDetail for each CartDetail
                    var orderDetail = new OrderDetails
                    {
                        OrderID = order.OrderID,
                        GameID = cartDetail.GameID,
                        Quantity = cartDetail.Quantity,
                        Price = cartDetail.Game.Price,
                        Orders = order,
                        Games = cartDetail.Game
                    };

                    _context.Add(orderDetail);

                    // Decrease the product stock
                    var product = await _context.Game.FindAsync(cartDetail.GameID);
                    if (product != null)
                    {
                        product.Stock -= cartDetail.Quantity;
                        _context.Game.Update(product);
                    }

                }

                // Clear Cart
                _context.CartDetail.RemoveRange(cart.CartDetails);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(CheckoutController.Confirmation));
            }

            return View(cart);
        }



        [Route("/Confirmation")]
        public ActionResult Confirmation()
        {
            return View();
        }
    }
}