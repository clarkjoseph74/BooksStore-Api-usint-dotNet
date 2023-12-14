using BooksStore.Data.Models;

namespace BooksStore.Repos
{
    public interface IBookRepo
    {
        public List<BookWithAuthorDTO> GetBooks(); 
        public BookWithAuthorDTO GetBook(int id);
        public Book UpdateBook(Book model);
        public BookWithAuthorDTO DeleteBook(int id);
        public Book AddBook(BookDTO model);
        public List<BookWithAuthorDTO> GetBooksWithAuthor();
        public List<BooksByAuhorDTO> GetBooksByAuthor();
    }
}
