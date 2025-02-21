using System;

namespace Library
{
    public class LoanFactory : ILoanFactory
    {
        private readonly ILoanDurationPolicy _loanDurationPolicy;
        private readonly IDateTimeProvider _dateTimeProvider;

        public LoanFactory(ILoanDurationPolicy loanDurationPolicy, IDateTimeProvider dateTimeProvider)
        {
            _loanDurationPolicy = loanDurationPolicy ?? throw new System.ArgumentNullException(nameof(loanDurationPolicy));
            _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        }

        public Loan Create(User user, Book book)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (book is null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            var startDate = _dateTimeProvider.Today();
            var endDate = _loanDurationPolicy.CalculateEndDate(startDate, book.Type);
            return new Loan(user, book, startDate, endDate);
        }
    }
}