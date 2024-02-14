using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Projet.ViewModel
{
    public class EditRoleViewModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name ="Role")]
        public string RoleName { get; set; }


        public List<string> Users { get; set; } = new();
    }
}