using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Projet.Models
{
    public class DefaultUser : IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; }

        [PersonalData]
        public string LastName { get; set; }

        [PersonalData]
        public string City { get; set; } = "";

        [PersonalData]
        [DataType(DataType.Date)]
        public DateTime UserCreationDate { get; set; }  
    }
}
