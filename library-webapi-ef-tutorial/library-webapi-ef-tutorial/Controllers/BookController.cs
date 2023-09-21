using DBFirstLibrary;
using library_webapi_ef_tutorial.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<IQueryable<Book>> GetBooks()
        {
            return Ok(_libRepo.ReadBooks());
        }
    }
}
