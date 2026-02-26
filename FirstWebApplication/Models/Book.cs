using System.ComponentModel.DataAnnotations;

namespace FirstWebApplication.Models
{
    public class Book
    {
        // add an attribute to request data field
        [Required(ErrorMessage = "{0} must be supplied")] // {0} represents the field name
        [Display(Name = "Book ID")]
        public int? BookId { get; set; }

        [StringLength(40, MinimumLength = 2, ErrorMessage = "{0} must be at least {2} chars length")] // {2} represents min length
        public string? Author { get; set; }

        public override string ToString()
        {
            return $"Book object: {BookId} {Author}";
        }

        [Range(0, 100, ErrorMessage = "{0} must be between {1} and {2}")]
        [Display(Name ="Number in stock")]
        public int? numberInStock { get; set; }

        [Validators.BookProductionValidator]
        public DateTime productionDateTime { get; set; }
    }
}
