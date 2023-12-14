namespace BooksStore.Data.Models
{
    public class BooksByAuhorDTO
    {
        public string Name { get; set; }
        public List<BookDTO> Books { get; set;}

        public int booksCount { get; set; }
    }
}
