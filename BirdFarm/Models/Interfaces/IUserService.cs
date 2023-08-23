
using BirdFarm.ModelsBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BirdFarm.Interfaces
{
    public interface IUserService
    {
        Task CreateAsync(User userCreateDto);
        Task<List<Cart>> GetCart(int id);
        //Task DeleteCart(int id, int CountEggs, int EggsId);
        //Task SaveCart(int UserId, int EggId, int count);
        Task<User> GetAsync(User userAuthDto);

        Task<User> GetCheckAsync(User checkUser);

        Task<bool> CheckNull(User model);

        //Task<List<Egg>> GetEggsAsync();
      

    }
}
