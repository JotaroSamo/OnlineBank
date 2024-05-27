using BirdFarm.ModelsBD;

namespace OnlineBank.Models
{
    public class CreditUser
    {
        public User user { get; set; } 
        public List<Credit> credit { get; set; }
    }
}
