using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBFirstLibrary
{
    public partial class Book
    {
        [Timestamp]
        public byte[] RowVersion { get; set; }  // the property (concurrency token) is used for optimistic concurrency: https://learn.microsoft.com/en-us/ef/core/saving/concurrency?tabs=data-annotations#optimistic-concurrency

        public override string ToString()
        {
            return $"{Title} ({PublishedDate})";
        }
    }
}
