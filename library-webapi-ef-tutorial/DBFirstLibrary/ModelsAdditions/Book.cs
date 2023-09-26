using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBFirstLibrary
{
    public partial class Book
    {
        public byte[] RowVersion { get; set; }  // the property is used for optimistic concurrency

        public override string ToString()
        {
            return $"{Title} ({PublishedDate})";
        }
    }
}
