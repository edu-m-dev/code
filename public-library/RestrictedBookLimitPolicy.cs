using System.Collections.Generic;
using System.Linq;

namespace Library
{
    public class RestrictedBookLimitPolicy : IBookLimitPolicy
    {
        public const int MaxTitlesPerUser = 7;
        public const int MaxComicTitlesPerUser = 3;

        public bool LimitReached(IReadOnlyList<Book> currentBooks, Book newBook)
        {
            if (newBook.Type == Book.BookType.Comic)
            {
                return currentBooks
                    .Where(x => x.Type == Book.BookType.Comic)
                    .Count() < MaxComicTitlesPerUser;
            }
            else
            {
                return currentBooks.Count() < MaxTitlesPerUser;
            }
        }
    }
}