namespace BooksStore.Data.Models
{
    public class BookWithAuthorDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string AuthorName { get; set; }

    }
}
