using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BirdFarm.Interfaces;
using BirdFarm.ModelsBD;


namespace BirdFarm.Controllers
{
    public class AuthController : Controller
    {


        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
          _userService = userService;
           
        }

        public IActionResult AuthPage()
        {
            return View();
        }
        public IActionResult RegPage()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegPage(User model)
        {
            try
            {
                if (await _userService.CheckNull(model) == false)
                {
                    return View(model);
                }
              
                if (await _userService.GetCheckAsync(model) == null)
                {
                    await _userService.CreateAsync(model);
                   
                    return RedirectToAction("Index", "Home");
                }

              

                return View(model);
            }
            catch (Exception )
            {
              
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> AuthPage(User userAuthDto)
        {
            try
            {
                var user = await _userService.GetAsync(userAuthDto);
                if (user == null)
                {
                  
                    return RedirectToAction("RegPage");
                }

                var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

                if (user.Role.ToString() == "Admin")
                {
                  
                    var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity));
                    return RedirectToAction("Tools", "Admin");
                }

             
                var userClaimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(userClaimsIdentity));
                return RedirectToAction("UserTools", "User");
            }
            catch (Exception )
            {
               
                throw;
            }
        }
    }
}
