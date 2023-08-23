
using BirdFarm.Interfaces;
using BirdFarm.ModelsBD;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BirdFarm.Services
{
    public class AdminService : IAdminService
    {
        private readonly CreditContext _creditsContext;
        public AdminService(CreditContext CreditContext) 
        { 
          _creditsContext = CreditContext;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _creditsContext.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _creditsContext.Users.FindAsync(id);
        }

 

        public async Task<bool> UpdateUserAsync(User user)
        {
            var existingUser = await _creditsContext.Users.FindAsync(user.Id);
            if (existingUser == null)
                return false;

            _creditsContext.Entry(existingUser).CurrentValues.SetValues(user);
            await _creditsContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _creditsContext.Users.FindAsync(id);
            if (user == null)
                return false;

            _creditsContext.Users.Remove(user);
            await _creditsContext.SaveChangesAsync();
            return true;
        }
        // Метод для получения всех кредитов из базы данных
        public async Task<List<Credit>> GetAllCreditsAsync()
        {
            return await _creditsContext.Credits.Where(c=>c.idd==0).ToListAsync();
        }

        // Метод для получения кредита по идентификатору из базы данных
        public async Task<Credit> GetCreditByIdAsync(int id)
        {
            return await _creditsContext.Credits.FindAsync(id);
        }

        // Метод для создания нового кредита в базе данных
        public async Task CreateCreditAsync(Credit credit)
        {
            _creditsContext.Credits.Add(credit);
            await _creditsContext.SaveChangesAsync();
        }

        // Метод для обновления данных кредита в базе данных
        public async Task UpdateCreditAsync(Credit credit)
        {
            _creditsContext.Credits.Update(credit);
            await _creditsContext.SaveChangesAsync();
        }

        // Метод для удаления кредита из базы данных
        public async Task DeleteCreditAsync(int id)
        {
            Credit credit = await _creditsContext.Credits.FindAsync(id);
            if (credit != null)
            {
                _creditsContext.Credits.Remove(credit);
                await _creditsContext.SaveChangesAsync();
            }
        }
        public async Task<List<Cart>> GetAllCartsAsync()
        {
            return await _creditsContext.Carts.ToListAsync();
        }

        public async Task<Cart> GetCartByIdAsync(int id)
        {
            return await _creditsContext.Carts.FindAsync(id);
        }

        public async Task CreateCartAsync(Cart cart)
        {
            _creditsContext.Carts.Add(cart);
            await _creditsContext.SaveChangesAsync();
        }

        public async Task UpdateCartAsync(Cart cart)
        {
            _creditsContext.Carts.Update(cart);
            await _creditsContext.SaveChangesAsync();
        }

        public async Task DeleteCartAsync(int id)
        {
            Cart cart = await _creditsContext.Carts.FindAsync(id);
            if (cart != null)
            {
                _creditsContext.Carts.Remove(cart);
                await _creditsContext.SaveChangesAsync();
            }
        }

        public async Task<List<Credit>> GetCartsByIdAsync(int id)
        {
           List<Cart> carts = await _creditsContext.Carts.Where(c=>c.UserId==id).ToListAsync();
            List<Credit> cr = await _creditsContext.Credits.ToListAsync();
            List<Credit> credits = new List<Credit>();
           
                foreach (var crs in cr)
                {
                     foreach (var c in carts)
                     {
                    if (crs.idd == c.UserId && crs.date == c.date)
                    {
                        credits.Add(crs);
                    }
                     }
                }
            

            return credits;
        }

        public async Task<List<Credit>> GetCreditsByIdAsync(int id)
        {
           return await _creditsContext.Credits.Where(c=>c.Id == id).ToListAsync();
        }

        public async Task<Credit> GetCreditByIddAsync(int id, DateTime date)
        {
            return await _creditsContext.Credits.FirstOrDefaultAsync(c => c.idd == id&&c.date ==date);
        }

        public async Task UpdateCreditSpecialAsync(Credit credit)
        {
            var credits = await _creditsContext.Credits.FirstOrDefaultAsync(c => c.Id == credit.Id);
            credits.Status=credit.Status;
            _creditsContext.Credits.Update(credits);
            await _creditsContext.SaveChangesAsync();
        }
    }

}
