using DBFirstLibrary;
using library_webapi_ef_tutorial.DTOs;
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

        private CancellationTokenSource? _cts;  // https://www.youtube.com/watch?v=hPkAzBJgHXQ

        public BookController(ILibraryRepository libRepo)
        {
            _libRepo = libRepo;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BookDto>), 200)]
        public async Task<IActionResult> GetBooks()
        // public ActionResult<IQueryable<BookDto>> GetBooks()
        {
            // return Ok(_libRepo.GetAllBooks().Select(b => new BookDto(b).ToListAsync()));  // Initializing a DTO instance in this constructor hides from EF Core, which data should be pulled from a DB. USing a parameterless constructor with properties initializers directly in Services / Controllers is a better option as EF then only pulls data for requested props

            return Ok(await _libRepo.GetAllBooks().EntityToModel().ToListAsync()); // Extension method EntityToModel() is declared in BookDto and allows keeping the clean code principles of Separation of Concerns and Single Repsonsibility here
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BookDto), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetBookByID(int id)
        // public ActionResult<BookDto> GetBookByID(int id)
        {
            var book = _libRepo.GetAllBooks(id);

            if (book is null) return NotFound($"Eintrag mit ID {id} wurde nicht gefunden."); 
            return Ok(new BookDto(book));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ActionResult), 201)]
        public async Task<IActionResult> CreateBook([FromBody] Book bookData)
        {
            _cts = new CancellationTokenSource();  // https://www.youtube.com/watch?v=hPkAzBJgHXQ
            // send a cancel after 4000 ms or call cts.Cancel();
             _cts.CancelAfter(4000);
            CancellationToken ct = _cts.Token;

            await _libRepo.CreateBookAsync(bookData);
            await _libRepo.SaveAsync(ct);

            //_libRepo.CreateBook(bookData);
            return CreatedAtAction(nameof(GetBookByID), new { id = bookData.BookId });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ActionResult), 200)]
        public ActionResult UpdateBook(int id, [FromBody] BookDto bookData)
        {
            _libRepo.UpdateBook(id, bookData.ModelToEntity(id));
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
