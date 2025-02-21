using System;

namespace Library
{
    public interface ILoanDurationPolicy
    {
        DateTime CalculateEndDate(DateTime startDate, Book.BookType type);
    }
}