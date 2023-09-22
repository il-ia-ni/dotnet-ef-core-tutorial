using DBFirstLibrary;
using library_webapi_ef_tutorial.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace library_webapi_ef_tutorial.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private ILibraryRepository _libRepo;

        public BookController(ILibraryRepository libRepo)
        {
            _libRepo = libRepo;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Book>), 200)]
        public ActionResult<IQueryable<Book>> GetBooks()
        {
            return Ok(_libRepo.ReadBooks());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Book), 200)]
        public ActionResult<IQueryable<Book>> GetBookByID(int id)
        {
            return Ok(_libRepo.ReadBooks(id));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ActionResult), 201)]
        public ActionResult CreateBook([FromBody] Book bookData)
        {
            _libRepo.CreateBook(bookData);
            return CreatedAtAction(nameof(GetBookByID), new { id = bookData.BookId });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ActionResult), 200)]
        public ActionResult UpdateBook(int id, [FromBody] Book bookData)
        {
            _libRepo.UpdateBook(id, bookData);
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ActionResult), 200)]
        public ActionResult DeleteBook(int id)
        {
            _libRepo.DeleteBook(id);
            return Ok();
        }
    }
}
