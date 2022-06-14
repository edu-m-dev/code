using System.Collections.Generic;

namespace Library
{
    public interface IBookLimitPolicy
    {
        bool LimitReached(IReadOnlyList<Book> currentBooks, Book newBook);
    }
}