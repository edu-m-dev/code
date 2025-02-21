namespace Library
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var dateTimeProvider = new RegularDateTimeProvider();
            var newLibrary = new Library(
                new LoanFactory(new RestrictedLoanDurationPolicy(), dateTimeProvider),
                new RestrictedBookLimitPolicy(),
                dateTimeProvider);
        }

        // =========================================================
        // C# test project : give yourself at most 90 minutes.
        // =========================================================

        //  Doing all questions is optional
        //  Code quality and explanations are the most important
        //

        // 1. Complete functions Library.AddLoan and Library.FinishLoan

        // 2. Complete function LateLoans knowing that Loans older than 3 weeks are late

        // 3. Users can borrow simultaneously at most 7 books
        // write a function PeopleBorrowingTooMuch to check the offending users

        // 4. Users that have late loans or at least 7 current loans can not borrow new books
        // write a function CanBorrow to check that

        // 5. Almost-late loans are loans that will become late in the next 48 hours.
        // Create a function returning almost-late loans

        // 6. We want to know which users have almost-late loans with the number of concerned books for each user

        // 7. We want to archive finished loans. Implement the feature

        // 8. Given a date, return the number of books that were on loan this day

        // 9. What are our options when calling Library.AddLoan with an user that cannot borrow new books ?

        // 10. New rule: users can borrow only 3 books with BookType equals to Comic, and the borrowing duration for comics is only two weeks
        // Write a new version of functions PeopleBorrowingTooMuch and CanBorrow
    }
}