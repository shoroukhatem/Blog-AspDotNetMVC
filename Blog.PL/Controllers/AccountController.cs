using Blog.DAL.Entities;
using Blog.PL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.PL.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> userManager;
		private readonly SignInManager<ApplicationUser> signInManager;
		private readonly ILogger<AccountController> logger;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<AccountController> logger)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
			this.logger = logger;
		}
		public IActionResult register()
		{
			return View(new RegisterViewModel());
		}
		[HttpPost]
		public async Task<IActionResult> register(RegisterViewModel input)
		{
			if(ModelState.IsValid)
			{
				var user = new ApplicationUser
				{
					UserName = input.Name,
					PhoneNumber = input.PhoneNumber
				};
				var result = await userManager.CreateAsync(user,input.Password);
				if (result.Succeeded)
				{
					return RedirectToAction("Login");
				}
				foreach (var item in result.Errors)
				{
					logger.LogError(item.Description);
					ModelState.AddModelError("", item.Description);
				}
			}
			return View(input);
		}
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel input)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(input.Name);
                if (user is null)
                {
                    ModelState.AddModelError("", "User Name Does Not Exist");
                    return View(input);
                }
                if (user != null && await userManager.CheckPasswordAsync(user, input.Password));
                {
                    var result = await signInManager.PasswordSignInAsync(user, input.Password,input.RememberMe, false);
                    if (result.Succeeded)
                    {
						//var token = await userManager.GenerateUserTokenAsync(user,"CustomTokenProvider", "TokenPurpose");

                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View(input);
        }
        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

    }
}
