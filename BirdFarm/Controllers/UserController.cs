﻿using BirdFarm.Interfaces;
using BirdFarm.ModelsBD;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BirdFarm.Controllers
{
    public class UserController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IUserService _userService;
        public UserController(IAdminService adminService, IUserService userService)
        {
            _adminService = adminService;
            _userService = userService;
        }
        private int GetCurrentUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
        public IActionResult UserTools()
        {
            HttpContext.Session.SetInt32("UserId", GetCurrentUserId());
            return View();
        }
        public async Task<IActionResult> ListBank()
        {

            return View(await _adminService.GetAllCreditsAsync());
        }
        public async Task<IActionResult> AddCart(int id, double sum)
        {
            int userId = GetCurrentUserId();
            HttpContext.Session.SetInt32("UserId", userId);
            var credit = await _adminService.GetCreditByIdAsync(id);
            DateTime a = DateTime.Now;
            Credit ncredit = new Credit()
            {
                Status = credit.Status,
                Name = credit.Name,
                Month = credit.Month,
                Procent = credit.Procent,
                Sum = sum,
                idd = credit.Id,
                date = a
            };
            await _adminService.CreateCreditAsync(ncredit);
            credit = await _adminService.GetCreditByIddAsync(id, a);
            Cart cart = new Cart()
            {
             UserId = userId,
             CreditId = credit.Id,
             date = a
             
                };
            await _adminService.CreateCartAsync(cart);
            return RedirectToAction("ListUserCart");
        }

        public async Task<IActionResult> ListUserCart()
        {
            int userId = GetCurrentUserId();
            HttpContext.Session.SetInt32("UserId", userId);
            var cart = await _adminService.GetCartsByIdAsync(userId);
            return View(cart);
        }
    }
}
