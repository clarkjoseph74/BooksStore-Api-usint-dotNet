using BooksStore.Data;
using BooksStore.Data.Models;
using BooksStore.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BooksStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        public BooksController(IBookRepo repo) { 
        _repo = repo;
        }
        private readonly IBookRepo _repo;


        [HttpGet]
        public IActionResult GetBooks() {
            return Ok(_repo.GetBooks());
        }


        [HttpGet("{id}")]
        public IActionResult GetBook(int id)
        {
            return Ok(_repo.GetBook(id));
        }

        [HttpPost]
        public  IActionResult AddBook([FromBody] BookDTO model)
        {
            return Ok(_repo.AddBook(model));
        }

        [HttpPut]
        public IActionResult UpdateBook([FromBody] Book model)
        {
            return Ok(_repo.UpdateBook(model));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            return Ok(_repo.DeleteBook(id));
        }

        [HttpGet("getBookWithAuthor")]
        public IActionResult GetBookWithAuthor()
        {
            return Ok(_repo.GetBooksWithAuthor());
        }

        [HttpGet("getBookByAuthor")]
        public IActionResult GetBookByAuthor()
        {
            return Ok(_repo.GetBooksByAuthor());
        }
    }
}
