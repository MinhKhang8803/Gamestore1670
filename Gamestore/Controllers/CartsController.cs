using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Gamestore.Data;
using Gamestore.Areas.Admin.Models;
using Gamestore.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Gamestore.Models;

namespace ASM_2_1670.Controllers
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
            // Kiểm tra xem người dùng đã đăng nhập chưa
            if (User.Identity.IsAuthenticated)
            {
                // Lấy thông tin người dùng
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserEmail == User.Identity.Name);

                if (user != null)
                {
                    // Kiểm tra xem giỏ hàng đã tồn tại chưa
                    var cart = await _context.Cart.Include(c => c.CartDetails)
                        .FirstOrDefaultAsync(c => c.UserID == user.UserId);

                    // Nếu giỏ hàng chưa tồn tại, tạo mới
                    if (cart == null)
                    {
                        cart = new Cart
                        {
                            UserID = user.UserId,
                            CartDetails = new List<CartDetail>()
                        };
                        _context.Cart.Add(cart);
                    }

                    // Kiểm tra xem sản phẩm đã tồn tại trong giỏ hàng chưa
                    var cartDetail = cart.CartDetails.FirstOrDefault(cd => cd.GameID == gameId);

                    if (cartDetail != null)
                    {
                        // Nếu sản phẩm đã tồn tại trong giỏ hàng, tăng số lượng
                        cartDetail.Quantity += quantity;
                    }
                    else
                    {
                        // Nếu sản phẩm chưa tồn tại trong giỏ hàng, thêm mới
                        cartDetail = new CartDetail
                        {
                            CartID = cart.CartID,
                            GameID = gameId,
                            Quantity = quantity
                        };
                        cart.CartDetails.Add(cartDetail);
                    }

                    // Lưu các thay đổi vào cơ sở dữ liệu
                    await _context.SaveChangesAsync();

                    return RedirectToAction("CartDetails", "Cart");
                }
            }

            // Nếu người dùng chưa đăng nhập, chuyển hướng đến trang đăng nhập
            return RedirectToAction("Login", "Account");
        }

        [Route("/CartDetails")]
        public IActionResult CartDetails()
        {
            // Lấy thông tin người dùng đã đăng nhập
            var user = _context.Users.FirstOrDefault(u => u.UserEmail == User.Identity.Name);

            if (user != null)
            {
                // Lấy giỏ hàng của người dùng
                var cart = _context.Cart.Include(c => c.CartDetails).ThenInclude(cd => cd.Games)
                    .FirstOrDefault(c => c.UserID == user.UserId);

                if (cart != null)
                {
                    ViewBag.CartQuantity = cart.CartDetails.Sum(cd => cd.Quantity);
                    return View(cart);
                }
            }

            // Nếu không có giỏ hàng hoặc người dùng, bạn có thể xử lý tình huống theo ý muốn, chẳng hạn hiển thị một giao diện giỏ hàng trống hoặc thông báo lỗi.
            ViewBag.CartQuantity = 0;
            return PartialView("_EmptyCart");
        }

        [Route("/Cart/GetCartItems")]
        public IActionResult GetCartItems()
        {
            // Lấy danh sách sản phẩm trong giỏ hàng
            var user = _context.Users.FirstOrDefault(u => u.UserEmail == User.Identity.Name);

            if (user != null)
            {
                var cart = _context.Cart.Include(c => c.CartDetails).ThenInclude(cd => cd.Games)
                    .FirstOrDefault(c => c.UserID == user.UserId);

                if (cart != null)
                {
                    return PartialView("_CartItems", cart.CartDetails);
                }
            }

            // Nếu không có giỏ hàng hoặc người dùng, trả về một phần tử HTML trống
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
