using System;
using System.Collections.Generic;

namespace DBFirstLibrary
{
    public partial class Author
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }

        public long AuthorId { get; set; }
        public string Name { get; set; } = null!;
        public DateOnly? BirthDate { get; set; }
        public DateOnly? DeathDate { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
