using System.ComponentModel.DataAnnotations;

namespace BooksStore.Data.Models
{
    //Princple
    public class Author
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }

    }
}
