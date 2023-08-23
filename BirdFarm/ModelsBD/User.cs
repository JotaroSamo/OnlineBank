using System.ComponentModel.DataAnnotations;
using System.Data;
using BirdFarm.ModelsBD.Role;
namespace BirdFarm.ModelsBD
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Phone { get; set; }
        public string? Guarantors  { get; set; }
        public double? Salary { get; set; }
        public int? WorkEx { get; set; }
        public string? Document { get; set; }
        public Role.Role Role { get; set; }

    }
}
