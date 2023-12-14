using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksStore.Data.Models
{
    //Child
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        public string? Descreption { get; set; }       
        public int? AuthorId { get; set; }

        public virtual Author? Author { get; set; }
    }
}
