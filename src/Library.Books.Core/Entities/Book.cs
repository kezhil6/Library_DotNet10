using System.ComponentModel.DataAnnotations;

namespace Library.Books.Core.Entities
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string Title { get; set; }
        [Required]
        public required string Author { get; set; }
        [Required]
        public required string Genre { get; set; }
        [Required]
        public int PublishedYear { get; set; }
    }
}
