using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    public class Book
    {
        public enum BookType
        {
            Comic,
            Novel,
            Poems,
            NonFiction,
        }

        public string Title { get; set; }

        public string Author { get; set; }

        public BookType Type { get; set; }
    }
}