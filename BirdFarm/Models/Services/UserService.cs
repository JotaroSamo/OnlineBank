

using BirdFarm.ModelsBD;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BirdFarm.Interfaces
{
    public class UserService : IUserService
    {
        private readonly CreditContext _creditContext;

		public UserService(CreditContext creditContext)
        {
         _creditContext = creditContext;
		}

        public async Task<bool> CheckNull(User model)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            {
                return false;
            }
            return true;
        }

        public async Task CreateAsync(User userCreateDto)
        {

            _creditContext.Users.Add(userCreateDto);
            await _creditContext.SaveChangesAsync();
            
        }

        //public async Task DeleteCart(int id, int CountEggs, int EggsId)
        //{
        //    var cart = await _farmContext.Carts.FirstOrDefaultAsync(c=>c.Id==id);
        //    _farmContext.Carts.Remove(cart);
        //    var perm = await _farmContext.Eggs.FirstOrDefaultAsync(c => c.EggID == EggsId);
        //    perm.CountEggs = perm.CountEggs+CountEggs;
        //    _farmContext.Eggs.UpdateRange(perm);
        //    await _farmContext.SaveChangesAsync();
        //}

        public async Task<User> GetAsync(User userAuthDto)
        {
            var user = await _creditContext.Users.FirstOrDefaultAsync(u =>
                u.Email == userAuthDto.Email && u.Password == userAuthDto.Password);
            if (user == null)
            {
                return null;
            }
            return user;
        }

        public async Task<List<Cart>> GetCart(int id)
        {
            return await _creditContext.Carts.Where(c => c.UserId == id).ToListAsync();
        }

        public async Task<User> GetCheckAsync(User checkUser)
        {
            var user = await _creditContext.Users.FirstOrDefaultAsync(u =>
                 u.Email == checkUser.Email);
            if (user == null)
            {
                return null;
            }
			return user;
		}

        //public async Task<List<Egg>> GetEggsAsync()
        //{
        //   return await _farmContext.Eggs.ToListAsync();
        //}

        //public async Task SaveCart(int UserId, int EggId, int count)
        //{
        //    Cart cart = new Cart();
        //    cart.UserId = UserId;
        //    var perm = await _farmContext.Eggs.FirstOrDefaultAsync(c=>c.EggID == EggId);
        //    cart.Price = count*perm.Price;
        //    cart.EggsId = EggId;
        //    cart.CountEggs= count;
        //    perm.CountEggs = perm.CountEggs - count;
        //    _farmContext.Eggs.UpdateRange(perm);
        //    _farmContext.Carts.Add(cart);
        //    await _farmContext.SaveChangesAsync();
        //}
    }
}
