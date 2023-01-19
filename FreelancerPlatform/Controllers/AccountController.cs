using FreelancerPlatform.Data;
using FreelancerPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FreelancerPlatform.Controllers
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
        public IActionResult Login(User user)
        {
            var userInDb = _context.Users.SingleOrDefault(u => u.Email == user.Email && u.Password == user.Password);

            if (userInDb == null)
            {
                ModelState.AddModelError("Email", "Invalid Email");
                ModelState.AddModelError("Password", "Invalid Password");
                return View();
            }

            HttpContext.Session.SetString("UserName", userInDb.Name);
            HttpContext.Session.SetString("UserEmail", userInDb.Email);
            HttpContext.Session.SetString("UserType", userInDb.UserType);
            
            if (userInDb.UserType == "admin")
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
    public class AdminOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = filterContext.HttpContext.Session;
            if (session.GetString("UserEmail") == null ||
                session.GetString("UserType") != "admin")
            {
                filterContext.Result = new RedirectResult("/Home/Index");
            }
        }
    }

}
