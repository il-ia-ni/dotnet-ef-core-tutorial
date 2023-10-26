using DBFirstLibrary;

namespace library_webapi_ef_tutorial.DTOs
{
    public static class BookDtoExtensions  // the word "extensions" has to be present in the name of the class with extension methods
    {
        public static IQueryable<BookDto> EntityToModel(this IQueryable<Book> source)  // Extension method that maps a collection of Book entites to a collection of BookDtos. https://www.youtube.com/watch?v=xzlNv737xBM
        {
            return source.Select(b => new BookDto()
            {
                BookID = b.BookId,
                Title = b.Title,
                PublishedDate = b.PublishedDate,
                AuthorData = new AuthorDto(b.Author, null)
            });
        }

        public static Book ModelToEntity(this BookDto source, long sourceId)  
        {
            return new Book()
            {
                BookId = sourceId,
                Title = source.Title,
                PublishedDate = source.PublishedDate,
                AuthorId = source.AuthorData.AuthorID
            };
        }
    }

    public class BookDto
    {
        public BookDto()
        {

        }

        public BookDto(Book entity)  // Initializing a DTO instance in this constructor hides from EF Core, which data should be pulled from a DB. USing a parameterless constructor with properties initializers directly in Services / Controllers is a better option as EF then only pulls data for requested props
        {
            this.BookID = entity.BookId;
            this.Title = entity.Title;
            this.PublishedDate = entity.PublishedDate;
            this.AuthorData = new AuthorDto(entity.Author);  // Here we don't need author's collection of books and make its "flattened" version for MVC. https://www.youtube.com/watch?v=6145Q1juVHI
        }

        public long? BookID { get; set; }

        public string Title { get; set; }

        public DateOnly? PublishedDate { get; set; }

        public AuthorDto AuthorData { get; set; }

        // TODO: Add ToEntity Method
    }
}
