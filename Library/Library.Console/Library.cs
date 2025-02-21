using System;
using System.Collections.Generic;
using System.Linq;

namespace Library
{
    public class Library
    {
        private List<Book> books = new List<Book>();

        private List<User> users = new List<User>();

        private List<Loan> currentLoans = new List<Loan>();
        private List<FinishedLoan> finishedLoans = new List<FinishedLoan>();

        private readonly ILoanFactory _loanFactory;
        private readonly IBookLimitPolicy _bookLimitPolicy;
        private readonly IDateTimeProvider _dateTimeProvider;

        public Library(ILoanFactory loanFactory,
            IBookLimitPolicy bookLimitPolicy,
            IDateTimeProvider dateTimeProvider)
        {
            _loanFactory = loanFactory ?? throw new ArgumentNullException(nameof(loanFactory));
            _bookLimitPolicy = bookLimitPolicy ?? throw new ArgumentNullException(nameof(bookLimitPolicy));
            _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        }

        public Loan AddLoan(User user, Book book)
        {
            if (IsAlreadyOnLoan(book))
            {
                throw new Exception($"{book.Title} is already on loan");
            }

            var userLoans = GetUserLoans(user);

            var lateLoansCount = userLoans
                .Count(x => LoanIsLate(x));
            if (lateLoansCount > 0)
            {
                throw new Exception($"User cannot borrow book, {lateLoansCount} loans are overdue");
            }

            var limitReached = _bookLimitPolicy.LimitReached(
                userLoans.Select(x => x.Book).ToList(),
                book);
            if (limitReached)
            {
                throw new Exception($"User cannot borrow book, limit reached");
            }

            var loan = _loanFactory.Create(user, book);
            currentLoans.Add(loan);
            return loan;
        }

        public void FinishLoan(Loan l)
        {
            finishedLoans.Add(new FinishedLoan(l, _dateTimeProvider.Today()));
            currentLoans.Remove(l);
        }

        public List<Loan> LateLoans()
        {
            return
                currentLoans
                .Where(x => LoanIsLate(x))
                .ToList();
        }

        private bool LoanIsLate(Loan x)
        {
            return x.EndDate < _dateTimeProvider.Today();
        }

        /// <summary>
        /// As the system does not allow to borrow more than the limit, this function returns users who cannot borrow any more titles. This is dependent on the book type.
        /// </summary>
        /// <returns></returns>
        public List<User> PeopleBorrowingTooMuch(Book.BookType type)
        {
            return
            currentLoans
                .GroupBy(x => new { x.Borrower.FirstName, x.Borrower.LastName })
                .Where(x => _bookLimitPolicy.LimitReached(
                    x.Select(y => y.Book).ToList(),
                    new Book() { Type = type }))
                .Select(x => x.First().Borrower)
                .ToList();
        }

        private bool IsAlreadyOnLoan(Book book)
        {
            return
            currentLoans
                .Where(x => x.Book.Title == book.Title)
                .Any();
        }

        public IReadOnlyList<Loan> GetUserLoans(User user)
        {
            return
            currentLoans
                .Where(x => x.Borrower.FirstName == user.FirstName
                    && x.Borrower.LastName == user.LastName)
                .ToList();
        }

        public IReadOnlyList<Loan> GetLoansDueInDays(int days)
        {
            var today = _dateTimeProvider.Today();
            return
                currentLoans
                .Where(x => x.EndDate <= today.AddDays(days))
                .ToList();
        }

        public IReadOnlyList<UserLoanCount> GetUserLoansDueIn(int days)
        {
            var today = _dateTimeProvider.Today();
            return
                currentLoans
                .Where(x => x.EndDate <= today.AddDays(days))
                .GroupBy(x => new { x.Borrower.FirstName, x.Borrower.LastName })
                .Select(x => new UserLoanCount
                {
                    User = x.First().Borrower,
                    LoanCount = x.Count(),
                })
                .ToList();
        }

        public int GetNumberOfBooksOnLoanOnThisDate(DateTime date)
        {
            var _date = date.Date;
            return
                finishedLoans
                    .Where(x => x.Loan.StartDate >= _date
                        && x.ReturnedOn <= _date)
                    .Select(x => x.Loan)
                .Union(
                    currentLoans
                        .Where(x => x.StartDate >= _date)
                    )
                .Select(x => x.Book)
                .GroupBy(x => x.Title) // distinct
                .Count();
        }
    }
}