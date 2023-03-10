using CoreAndFood.Data;
using CoreAndFood.Data.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoreAndFood.Controllers
{
	public class LoginController : Controller
	{
		[AllowAnonymous]
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		Context db = new Context();

		[AllowAnonymous]
		[HttpPost]
		public async Task<IActionResult> Index(Admin admin)
		{
			var dataValue = db.Admins.FirstOrDefault(x => x.UserName == admin.UserName && x.Password == admin.Password);

			if (dataValue != null)
			{
				var claims = new List<Claim>
				{
					new Claim(ClaimTypes.Name, admin.UserName)
				};

				var userIdentity = new ClaimsIdentity(claims, "Login");
				ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
				await HttpContext.SignInAsync(principal);
				return RedirectToAction("Index", "Category");
			}

			return View();
		}
		[HttpGet]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Index", "Login");
		}
		
	}
}


