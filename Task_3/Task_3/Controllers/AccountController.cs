using Microsoft.AspNetCore.Mvc;
using Task_3.Data;
using Task_3.Data.Models;
using Task_3.Data.ViewModels;

namespace Task_3.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
                if (user != null)
                {
                    // Login successful, redirect to user management page
                    return RedirectToAction("Index", "UserManagement");
                }
                else
                {
                    // Login failed, display error message
                    ModelState.AddModelError("", "Invalid email or password");
                }
            }
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = model.Password,
                    RegistrationTime = DateTime.Now
                };
                _context.Users.Add(user);
                _context.SaveChanges();
                // Registration successful, redirect to login page
                return RedirectToAction("Login");
            }
            return View(model);
        }
    }
}
