using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogT.ViewModels;
using DogT.Data;
using DogT.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace DogT.Controllers
{
    public class AccountController : Controller
    {
        private readonly DogTContext _context;

        public AccountController(DogTContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.Positions = new Position();
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                User user = await _context
                    .Users.FirstOrDefaultAsync(u => u.Email == viewModel.Email);

                DogHandler dogHandler = new DogHandler
                {
                    Name = viewModel.Name,
                    Surname = viewModel.Surname,
                    Position = viewModel.Position
                };

                if (user == null)
                {
                    user = new User { 
                        Email = viewModel.Email, 
                        Password = viewModel.Password, 
                        DogHandler = dogHandler};
                    
                    Role userRole = await _context.Roles.FirstOrDefaultAsync(r => r.Title == "Кінолог");
                    if (userRole != null)
                    {
                        user.Role = userRole;
                    }

                    _context.Users.Add(user);
                    _context.DogHandlers.Add(dogHandler);

                    await _context.SaveChangesAsync();

                    await Authenticate(user);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Некоректний логін чи(та) пароль.");
                }

            }
            return View(viewModel);
        }

        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Title)
            };

            ClaimsIdentity id = new ClaimsIdentity(
                claims, "ApplicationCookie", 
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, 
                new ClaimsPrincipal(id));
        }
    }
}
