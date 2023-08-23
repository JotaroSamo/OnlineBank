using BirdFarm.ModelsBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace BirdFarm.Interfaces
{
    public interface IAdminService
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);
        Task<List<Credit>> GetAllCreditsAsync();
        Task<Credit> GetCreditByIdAsync(int id);
        Task<Credit> GetCreditByIddAsync(int id, DateTime date);
        Task CreateCreditAsync(Credit credit);
        Task<List<Credit>> GetCartsByIdAsync(int id);
        Task<List<Credit>> GetCreditsByIdAsync(int id);
        Task UpdateCreditAsync(Credit credit);
        Task UpdateCreditSpecialAsync(Credit credit);
        Task DeleteCreditAsync(int id);
        Task<List<Cart>> GetAllCartsAsync();
       
        Task<Cart> GetCartByIdAsync(int id);
        Task CreateCartAsync(Cart cart);
        Task UpdateCartAsync(Cart cart);
        Task DeleteCartAsync(int id);
    }
}
