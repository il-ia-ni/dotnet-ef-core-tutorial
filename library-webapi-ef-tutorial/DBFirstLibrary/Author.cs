using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DBFirstLibrary
{
    public partial class Author  // keyword partial lets extending the auto-generated class with additions in other files
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }

        public long AuthorId { get; set; }
        public string Name { get; set; } = null!;
        public DateOnly? BirthDate { get; set; }
        public DateOnly? DeathDate { get; set; }
        // https://stackoverflow.com/questions/65163728/how-to-json-serialize-without-cyclic-error
        // [JsonIgnore]
        public virtual ICollection<Book> Books { get; set; }
    }
}
