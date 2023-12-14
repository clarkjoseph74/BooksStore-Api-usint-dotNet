using BooksStore.Data;
using BooksStore.Data.Models;

namespace BooksStore.Repos
{
    public class BookRepo : IBookRepo
    {
        private readonly ApplicationDbContext _db;
        public BookRepo(ApplicationDbContext db)
        {
            _db = db;
        }

        public Book AddBook(BookDTO model)
        {
            Book book = new Book() { 
            Title = model.Title,
            AuthorId = model.AuthorId,
            Descreption = model.Description,
            Author = null
            };
            _db.Books.Add(book);
            _db.SaveChanges();
            return book;
        }

        public BookWithAuthorDTO DeleteBook(int id)
        {
            var book = GetBook(id);
            _db.Remove(book);
            _db.SaveChanges();
            return book;
        }

        public BookWithAuthorDTO GetBook(int id)
        {
            return _db.Books.Where(b => b.Id == id).Select(b => new BookWithAuthorDTO { Id = b.Id, AuthorName = b.Author.Name, Description = b.Descreption, Title = b.Title }).Single();
        }

        public List<BookWithAuthorDTO> GetBooks()
        {

            var books = _db.Books.Select(b => new BookWithAuthorDTO { 
                Id = b.Id,
                AuthorName = b.Author.Name,
                Title = b.Title,
                Description = b.Descreption });
           return books.ToList();
        }

        public Book UpdateBook(Book model)
        {
            _db.Update(model);
            _db.SaveChanges();
            return model;
        }




        public List<BookWithAuthorDTO> GetBooksWithAuthor()
        {
            var bookWithAuthor = _db.Books.Join(_db.Authors, b => b.AuthorId, a => a.Id,
                 (b, a) => new 
                 BookWithAuthorDTO { AuthorName = a.Name ,
                     Description = b.Descreption ,
                     Id = b.Id ,
                     Title = b.Title });
            return bookWithAuthor.ToList();
        }




        public List<BooksByAuhorDTO> GetBooksByAuthor()
        {
            var booksByAuthor = _db.Authors.GroupJoin(_db.Books, a => a.Id, b => b.AuthorId, (a, b) =>
            new BooksByAuhorDTO
            {
                Books = b.Select(x => 
                    new BookDTO{ Title = x.Title, 
                        Description = x.Descreption ,
                        AuthorId = a.Id}).ToList(),
                Name = a.Name,
                booksCount = b.AsQueryable().Count()
            });
            return booksByAuthor.ToList();
        }

        
    }
}
