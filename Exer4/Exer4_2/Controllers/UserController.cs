using Exer4_2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Exer4_2.Controllers
{
    public class UserController : Controller
    {
        private readonly Ex4DbContext _context;

        public UserController(Ex4DbContext context)
        {
            _context = context;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login([Bind("Email","Username","Password")] User user)
        {
            if(ModelState.IsValid)
            {
                var existing = _context.Users.FirstOrDefault(u => u.Username == user.Username);
                if(existing != null)
                {
                    if(existing.Password == user.Password)
                    {
                        return RedirectToAction("Main", "Home");
                    }
                    ModelState.AddModelError("Incorrect password", "Incorrect Password");
                }
                ModelState.AddModelError("Incorrect username", "Incorrect Username");
            }
            return View("Login");
        }
    }
}
