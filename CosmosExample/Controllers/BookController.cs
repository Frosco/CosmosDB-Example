using System.Collections.Generic;
using System.Threading.Tasks;
using CosmosExample.Models;
using CosmosExample.Services;
using Microsoft.AspNetCore.Mvc;

namespace CosmosExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        // GET: api/Book
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> Get()
        {
            var books = await _bookRepository.GetAllBooks();
            return Ok(books);
        }

        // GET: api/Book/5
        [HttpGet("{isbn}", Name = "Get")]
        public async Task<ActionResult<Book>> Get(string isbn)
        {
            return Ok(await _bookRepository.GetBook(isbn)); ;
        }

        // POST: api/Book
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Book book)
        {
            await _bookRepository.CreateBook(book);
            return NoContent();
        }

        // PUT: api/Book/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string isbn, [FromBody] Book book)
        {
            await _bookRepository.UpdateBook(isbn, book);
            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string isbn)
        {
            await _bookRepository.Delete(isbn);
            return NoContent();
        }
    }
}
