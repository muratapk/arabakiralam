using arabakiralam.Models;
using arabakiralam.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace arabakiralam.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public LoginController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View(); 
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel loginViewModel)
        {

            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Kullanıcı bulunamadı");
                return View(loginViewModel);
            }

            if (ModelState.IsValid)
            {
                var result =await _signInManager.PasswordSignInAsync(
                    user.UserName,
                    loginViewModel.Password,
                    false,
                    lockoutOnFailure:true
                    
                    
                    );
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Main");
                }
                return View();


            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult>Logout()
        {
             await _signInManager.SignOutAsync();



            return RedirectToAction("Index","Home");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {

            if (registerViewModel.Password == null)
            {
                return NotFound();
            }
            if(registerViewModel.Password==registerViewModel.ConfirmedPassword)
            {
                var user = new AppUser
                {
                    AdSoyad = registerViewModel.AdSoyad,
                    Country = registerViewModel.Country,
                    Email= registerViewModel.Email,
                    UserName= registerViewModel.UserName,
                    EmailConfirmed=false,
                };
                var result=await _userManager.CreateAsync(user,registerViewModel.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Main");
                }
                
                //Main isimli Control nesnesine yönlen
            }

            return View();
        }
    }
}
