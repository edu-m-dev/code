using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Tests
{
    [TestClass]
    public class LibraryTests
    {
        [TestMethod]
        public void CannotBorrowMoreBooksThanAllowed()
        {
            var users = GenerateUsers();
            var books = GenerateBooks();

            var dateTimeProvider = new RegularDateTimeProvider();
            var loanDurationPolicy = new RegularLoanDurationPolicy();
            var bookLimitPolicy = new RegularBookLimitPolicy();
            var library = CreateLibrary(dateTimeProvider, loanDurationPolicy, bookLimitPolicy);

            var user = users[0];
            var booksToBorrow = books.Take(7);
            foreach (var book in booksToBorrow)
            {
                library.AddLoan(user, book);
            }
            Action act = () => library.AddLoan(user, books[7]);
            act.Should().Throw<Exception>();
        }

        [TestMethod]
        public void FinishingALoanAllowsForMoreBooksToBeBorrowed()
        {
            var users = GenerateUsers();
            var books = GenerateBooks();

            var dateTimeProvider = new RegularDateTimeProvider();
            var loanDurationPolicy = new RegularLoanDurationPolicy();
            var bookLimitPolicy = new RegularBookLimitPolicy();
            var library = CreateLibrary(dateTimeProvider, loanDurationPolicy, bookLimitPolicy);

            var user = users[0];
            var booksToBorrow = books.Take(7);
            Loan lastLoan = null;
            foreach (var book in booksToBorrow)
            {
                lastLoan = library.AddLoan(user, book);
            }

            library.FinishLoan(lastLoan);

            library.AddLoan(user, books[7]);
        }

        [TestMethod]
        public void UserLoansAreAsExpected()
        {
            var users = GenerateUsers();
            var books = GenerateBooks();

            var dateTimeProvider = new RegularDateTimeProvider();
            var loanDurationPolicy = new RegularLoanDurationPolicy();
            var bookLimitPolicy = new RegularBookLimitPolicy();
            var library = CreateLibrary(dateTimeProvider, loanDurationPolicy, bookLimitPolicy);

            var user = users[0];
            var booksToBorrow = books.Take(7);
            Loan lastLoan = null;
            foreach (var book in booksToBorrow)
            {
                lastLoan = library.AddLoan(user, book);
            }

            var loans = library.GetUserLoans(user);
            loans.Select(x => x.Book).Should().BeEquivalentTo(booksToBorrow);
        }

        [TestMethod]
        public void UserHasReachedLoanLimit()
        {
            var users = GenerateUsers();
            var books = GenerateBooks();

            var dateTimeProvider = new RegularDateTimeProvider();
            var loanDurationPolicy = new RegularLoanDurationPolicy();
            var bookLimitPolicy = new RegularBookLimitPolicy();
            var library = CreateLibrary(dateTimeProvider, loanDurationPolicy, bookLimitPolicy);

            var user = users[0];
            var booksToBorrow = books.Take(7);
            foreach (var book in booksToBorrow)
            {
                library.AddLoan(user, book);
            }

            var usersAtLimit = library.PeopleBorrowingTooMuch(Book.BookType.Novel);
            usersAtLimit.Should().BeEquivalentTo(new List<User> { user });
        }

        [TestMethod]
        public void NewUserLoansAreDueInLoanDurationDays()
        {
            var users = GenerateUsers();
            var books = GenerateBooks();

            var dateTimeProvider = new RegularDateTimeProvider();
            var loanDurationPolicy = new RegularLoanDurationPolicy();
            var bookLimitPolicy = new RegularBookLimitPolicy();
            var library = CreateLibrary(dateTimeProvider, loanDurationPolicy, bookLimitPolicy);

            var user = users[0];
            var booksToBorrow = books.Take(7);
            var loans = new List<Loan>();
            foreach (var book in booksToBorrow)
            {
                loans.Add(
                    library.AddLoan(user, book));
            }

            var dueLoans = library.GetLoansDueInDays(21);
            dueLoans.Should().BeEquivalentTo(loans);

            dueLoans = library.GetLoansDueInDays(1);
            dueLoans.Should().BeEmpty();
        }

        [TestMethod]
        public void UserLoanIsLate()
        {
            var users = GenerateUsers();
            var books = GenerateBooks();

            var dateTimeProvider = new FixedDateTimeProvider(DateTime.Today.AddYears(1));
            var loanDurationPolicy = new RegularLoanDurationPolicy();
            var bookLimitPolicy = new RegularBookLimitPolicy();
            var library = CreateLibrary(dateTimeProvider, loanDurationPolicy, bookLimitPolicy);

            var user = users[0];
            var book = books[0];
            var loan = library.AddLoan(user, book);

            var lateLoans = library.LateLoans();
            lateLoans.Should().BeEquivalentTo(new List<Loan> { loan });
        }

        [TestMethod]
        public void BooksOnLoanOnADateIncludesReturned()
        {
            var users = GenerateUsers();
            var books = GenerateBooks();

            var dateTimeProvider = new RegularDateTimeProvider();
            var loanDurationPolicy = new RegularLoanDurationPolicy();
            var bookLimitPolicy = new RegularBookLimitPolicy();
            var library = CreateLibrary(dateTimeProvider, loanDurationPolicy, bookLimitPolicy);

            var user = users[0];
            var booksToBorrow = books.Take(7);
            Loan lastLoan = null;
            foreach (var book in booksToBorrow)
            {
                lastLoan = library.AddLoan(user, book);
            }

            library.FinishLoan(lastLoan);
            lastLoan = library.AddLoan(user, books[7]);

            var bookNumber = library.GetNumberOfBooksOnLoanOnThisDate(dateTimeProvider.Now());
            bookNumber.Should().Be(8);

            library.FinishLoan(lastLoan);
            library.AddLoan(user, books[7]); // same book
            bookNumber = library.GetNumberOfBooksOnLoanOnThisDate(dateTimeProvider.Now());
            bookNumber.Should().Be(8);
        }

        private static IEnumerable<Loan> Generate1DayLoans(IReadOnlyList<Book> books, IReadOnlyList<User> users)
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);
            return
                from u in users
                from b in books
                select new Loan(u, b, today, tomorrow);
        }

        private IReadOnlyList<Book> GenerateBooks()
        {
            var titles = Enumerable.Range(0, 10).Select(x => $"Title{x}");
            var authors = Enumerable.Range(0, 10).Select(x => $"Author{x}");
            var bookTypes = Enum.GetValues(typeof(Book.BookType)).Cast<Book.BookType>();

            return
                (from t in titles
                 from a in authors
                 from bt in bookTypes
                 select new Book
                 {
                     Title = $"{t}_{a}_{bt}",
                     Author = a,
                     Type = bt,
                 }
                 ).ToList();
        }

        private IReadOnlyList<User> GenerateUsers()
        {
            return
                Enumerable.Range(0, 10).Select(x => new User
                {
                    FirstName = $"FirstName{x}",
                    LastName = $"LastName{x}",
                }).ToList();
        }

        private Library CreateLibrary(
            IDateTimeProvider dateTimeProvider,
            ILoanDurationPolicy loanDurationPolicy,
            IBookLimitPolicy bookLimitPolicy)
        {
            return new Library(
                new LoanFactory(loanDurationPolicy, new RegularDateTimeProvider()),
                bookLimitPolicy,
                dateTimeProvider);
        }
    }
}