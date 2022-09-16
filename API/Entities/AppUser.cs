using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Extensions;

namespace API.Entities
{
    public class AppUser
    {

     public int Id { get; set; }
     public String UserName { get; set; }
     
     public byte[] passwordHash { get; set; }
     public byte[] passwordSalt { get; set; }
     public DateTime DateOfBirth { get; set; }
     public string knownAs { get; set; }
     public DateTime Created { get; set; } = DateTime.Now;
     public DateTime LastActive { get; set; } = DateTime.Now;
     public string Gender { get; set; }
     public string Introduction { get; set; }
     public string LookingFor { get; set; }
     public string Interests    { get; set; }
     public string City { get; set; }
     public string Country { get; set; }
     //one user can have multiple photos 1 -many r/p user table with photo table
     public ICollection<Photo> Photos { get; set; }
     public int GetAge(){
        return DateOfBirth.CalculateAge();
     }
    
    }
}