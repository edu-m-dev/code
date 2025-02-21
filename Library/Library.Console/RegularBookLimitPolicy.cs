using System.Collections.Generic;
using System.Linq;

namespace Library
{
    public class RegularBookLimitPolicy : IBookLimitPolicy
    {
        private const int MaxTitlesPerUser = 7;

        public bool LimitReached(IReadOnlyList<Book> currentBooks, Book newBook)
        {
            return currentBooks.Count() >= MaxTitlesPerUser;
        }
    }
}