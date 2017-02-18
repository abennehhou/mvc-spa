using System;
using System.ComponentModel.DataAnnotations;

namespace MvcSpa.Data
{
    public class Product
    {
        //TODO Split this objet in two: one for database and one for model/command.

        public Guid Id { get; set; }

        [Required(ErrorMessage = "Product Name must be filled in.")]
        [Display(Name = "Product Name")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Product Name must be greater than {2} characters and less than {1} characters.")]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime IntroductionDate { get; set; }

        [Required(ErrorMessage = "URL must be filled in.")]
        [Display(Name = "Product URL")]
        [Url]
        [StringLength(2000, MinimumLength = 5, ErrorMessage = "URL must be greater than {2} characters and less than {1} characters.")]
        public string Url { get; set; }

        [Range(1, 9999, ErrorMessage = "{0} must be between {1} and {2}")]
        public decimal Price { get; set; }
    }
}
