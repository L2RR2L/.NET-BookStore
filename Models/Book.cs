using System.ComponentModel.DataAnnotations;

namespace Projet.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        [Required, MaxLength(20)]
        public string ISBN { get; set; }

        [Required,DataType(DataType.Date),Display(Name = "Publish Date")]
        public DateTime PublishDate { get; set; }

        [Required,DataType(DataType.Currency)]
        public int Price { get; set; }

        [Required]
        public string Author { get; set; }

        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }
    }
}
