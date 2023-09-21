using System;
using System.Collections.Generic;

namespace DBFirstLibrary
{
    public partial class Book
    {
        public long BookId { get; set; }
        public string Title { get; set; } = null!;
        public DateOnly? PublishedDate { get; set; }
        public long AuthorId { get; set; }

        public virtual Author Author { get; set; } = null!;
    }
}
