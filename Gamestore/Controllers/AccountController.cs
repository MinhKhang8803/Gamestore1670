using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gamestore.Areas.Admin.Models;
using Gamestore.Data;
using System.Security.Claims;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Gamestore.Controllers
{

    public class AccountController : Controller
    {
        private readonly GamestoreContext _context;

        public AccountController(GamestoreContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.User = GetCurrentUserInformation();
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("UserId,UserName,UserEmail,UserPassword,UserPhone,UserAddress")] Users user)
        {
            if (ModelState.IsValid)
            {
                user.UserRole = "User"; // Set the user role to "User" by default
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login", "Account");
            }
            return View(user);
        }



        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
     
        public async Task<IActionResult> Login(Users _user)
        {
            var _users = _context.Users.Where(model => model.UserEmail == _user.UserEmail && model.UserPassword == _user.UserPassword).FirstOrDefault();

            if (_users == null)
            {
                ViewBag.LoginStatus = 0;
            }
            else
            {
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, _users.UserEmail),
            new Claim("FullName", _users.UserName),
            new Claim(ClaimTypes.Role, _users.UserRole),
        };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties();

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                // Redirect to different pages depending on the user's role
                if (_users.UserRole == "Admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
                else if (_users.UserRole == "User")
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (_users.UserRole == "Employee")
                {
                    return RedirectToAction("Index", "Employee");
                }
            }

            return View();
        }

        public Users GetCurrentUserInformation()
        {
            var email = User.Identity.Name;
            var user = _context.Users.FirstOrDefault(u => u.UserEmail == email);
            return user;
        }


        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Account");
        }
    }
}