using Microsoft.AspNetCore.Mvc;
using Task_3.Data;

namespace Task_3.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserManagementController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Users.ToList());
        }

        public IActionResult Block(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                user.IsActive = false;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Unblock(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                user.IsActive = true;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult BlockMultiple(List<int> ids)
        {
            foreach (var id in ids)
            {
                var user = _context.Users.Find(id);
                if (user != null)
                {
                    user.IsActive = false;
                }
            }
            _context.SaveChanges();
            return Json(new { success = true });
        }

        public IActionResult UnblockMultiple(List<int> ids)
        {
            foreach (var id in ids)
            {
                var user = _context.Users.Find(id);
                if (user != null)
                {
                    user.IsActive = true;
                }
            }
            _context.SaveChanges();
            return Json(new { success = true });
        }

        public IActionResult DeleteMultiple(List<int> ids)
        {
            foreach (var id in ids)
            {
                var user = _context.Users.Find(id);
                if (user != null)
                {
                    _context.Users.Remove(user);
                }
            }
            _context.SaveChanges();
            return Json(new { success = true });
        }
    }
}
