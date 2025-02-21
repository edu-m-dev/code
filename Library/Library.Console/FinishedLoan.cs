using System;

namespace Library
{
    public class FinishedLoan
    {
        public Loan Loan { get; }
        public DateTime ReturnedOn { get; }

        public FinishedLoan(Loan loan, DateTime returnedOn)
        {
            Loan = loan;
            ReturnedOn = returnedOn;
        }
    }
}