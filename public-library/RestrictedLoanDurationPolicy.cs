using System;

namespace Library
{
    public class RestrictedLoanDurationPolicy : ILoanDurationPolicy
    {
        private const int LoanDurationInDays = 21;
        private const int ComicLoanDurationInDays = 14;

        public DateTime CalculateEndDate(DateTime startDate, Book.BookType type)
        {
            if (type == Book.BookType.Comic)
            {
                return startDate.AddDays(ComicLoanDurationInDays);
            }
            else
            {
                return startDate.AddDays(LoanDurationInDays);
            }
        }
    }
}