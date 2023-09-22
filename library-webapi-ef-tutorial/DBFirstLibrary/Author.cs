using System;
using System.Collections.Generic;

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

        public virtual ICollection<Book> Books { get; set; }
    }
}
