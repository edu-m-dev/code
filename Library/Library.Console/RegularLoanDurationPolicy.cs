using System;

namespace Library
{
    public class RegularLoanDurationPolicy : ILoanDurationPolicy
    {
        private const int LoanDurationInDays = 21;

        public DateTime CalculateEndDate(DateTime startDate, Book.BookType type)
        {
            return startDate.AddDays(LoanDurationInDays);
        }
    }
}