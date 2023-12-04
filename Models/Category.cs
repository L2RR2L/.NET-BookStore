using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Projet.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = "undefined";

        [DisplayName("Display Order")]
        [Range(1,50)]
        public int DisplayOrder { get; set; }

        public DateTime CreationTime { get; set; } = DateTime.Now;

         
    }
}
