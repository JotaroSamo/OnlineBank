using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BirdFarm.ModelsBD
{
    public class CreditContext : DbContext
    {

        public CreditContext(DbContextOptions<CreditContext> options) : base(options)
        {
 
            Database.EnsureCreated();
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Credit> Credits { get; set; }
        public DbSet<Cart> Carts { get; set; }
    }
}
