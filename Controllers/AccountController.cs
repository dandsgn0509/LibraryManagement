using LibraryManagement.Data;
using LibraryManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace LibraryManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly LibraryManagementContext _context;
        public AccountController(LibraryManagementContext context)
        {
            _context = context;
        }
        public IActionResult Login()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var employee = _context.Employees
                .FirstOrDefault(e => e.Username == model.Username);

            if (employee == null)
            {
                ViewBag.Error = "Tên đăng nhập không tồn tại.";
                return View(model);
            }

            if (employee.Password != model.Password)
            {
                ViewBag.Error = "Mật khẩu không đúng.";
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, employee.Username),
                new Claim(ClaimTypes.Role, employee.Role.ToString()),
                new Claim("EmployeeId", employee.EmployeeId.ToString())
            };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal);

            return RedirectToAction("Index", "Dashboard");
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }
    }
}
