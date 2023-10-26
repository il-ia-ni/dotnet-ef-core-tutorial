using DBFirstLibrary;

namespace library_webapi_ef_tutorial.DTOs
{
    // https://stackoverflow.com/questions/65163728/how-to-json-serialize-without-cyclic-error
    // Separating MVC Models from DB Models (Entities) is a more clean alternative to setting attributes like for entity classes [JsonIgnore]
    public class AuthorDto
    {
        public AuthorDto()
        {

        }

        public AuthorDto(Author entity, ICollection<BookDto>? books = null)  // Initializing a DTO instance in this constructor hides from EF Core, which data should be pulled from a DB. USing a parameterless constructor with properties initializers directly in Services / Controllers is a better option as EF then only pulls data for requested props
        {
            this.AuthorID = entity.AuthorId;
            this.AuthorName = entity.Name;
            this.BirthDate = entity.BirthDate;
            this.DeathDate = entity.DeathDate;
            this.Books = books is null ? null : books;  // is there a better way to leave out this prop from BookDto prospective?
        }

        public long AuthorID { get; set; }

        public string AuthorName { get; set; }

        public DateOnly? BirthDate {  get; set; }

        public DateOnly? DeathDate { get; set; }

        public virtual ICollection<BookDto>? Books { get; set; }  // TODO: How to configure author collection of books from the author prospective while avoiding in-memory loops between BookDto and AuthorDto instances?

        // TODO: Add ToEntity Method
    }
}
