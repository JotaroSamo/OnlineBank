
using BirdFarm.Interfaces;
using BirdFarm.ModelsBD;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using System.Security.Claims;


namespace Notebook.Controllers
{
    public class AdminController : Controller
    {
      
        private readonly IAdminService _adminService;
        private readonly IUserService _userService;

        public AdminController(IAdminService adminService, IUserService userService)
        {
            _adminService = adminService;
            _userService = userService;
        }
        public IActionResult AddCredit()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SaveAddCredit(Credit credit)
        {
            await _adminService.CreateCreditAsync(credit);
            return RedirectToAction("CreditsList");
        }
        public async Task<IActionResult> CreditsList()
        {
            return View(await _adminService.GetAllCreditsAsync()); 
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCredit(int id,bool t)
        {
            await _adminService.DeleteCreditAsync(id);
            if (t==true)
            {
                return RedirectToAction("ListUserCart","User");
            }
            else
            {
                return RedirectToAction("CreditsList");
            }
         
        }
        [HttpPost]
        public async Task<IActionResult> EditCredit(int id)
        {

            return View(await _adminService.GetCreditByIdAsync(id));
        }
       
        [HttpPost]
        public async Task<IActionResult> UpdateJsonCredit(Credit credit, int Id)
        {
            //Credit creditObject = JsonConvert.DeserializeObject<Credit>(credit);
            credit.Id=Id;
            await _adminService.UpdateCreditSpecialAsync(credit);
            return RedirectToAction("GetAllUser");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCredit(Credit credit)
        {
            await _adminService.UpdateCreditAsync(credit);
            return RedirectToAction("CreditsList");
        }
        [HttpPost]
        public async Task<IActionResult> StatusUser(int id)
        {
            return View(await _adminService.GetCartsByIdAsync(id));
        }
        public async Task<IActionResult> EditUser(int id)
        {

            var user = await _adminService.GetUserByIdAsync(id);
            if (user == null)
            {

                return NotFound();
            }

            return View(user);
        }
       


        [HttpPost]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            if (id != user.Id)
            {

                return NotFound();
            }

            try
            {
                await _adminService.UpdateUserAsync(user);

                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("GetAllUser");
                }
                else
                {
                    return RedirectToAction("UserTools", "User");
                }

            }
            catch (Exception)
            {

                ModelState.AddModelError("", "The user has been updated by another user. Please refresh and try again.");
                return View("EditUser", user);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _adminService.DeleteUserAsync(id);

            return RedirectToAction("GetAllUser");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            try
            {
                var users = await _adminService.GetAllUsersAsync();

                return View("ViewAllUser", users);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IActionResult ViewAllUser(List<User> user)
        {

            return View(user);
        }

        public IActionResult Tools()
        {
            return View();
        }


    }
}
