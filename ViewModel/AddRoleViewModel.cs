using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Projet.ViewModel
{
    public class AddRoleViewModel
    {
        [Required]
        [Display(Name = "Role")]
        public string RoleName { get; set; }
    }
}
