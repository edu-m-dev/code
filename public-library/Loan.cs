using System;

namespace Library
{
    public class Loan
    {
        public User Borrower { get; private set; }
        public DateTime StartDate { get; private set; }
        public Book Book { get; private set; }
        public DateTime EndDate { get; private set; }

        public Loan(User u, Book b, DateTime startDate, DateTime endDate)
        {
            Borrower = u ?? throw new ArgumentNullException(nameof(u));
            Book = b ?? throw new ArgumentNullException(nameof(b));
            if (endDate < startDate)
            {
                throw new ArgumentOutOfRangeException(nameof(endDate));
            }
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}