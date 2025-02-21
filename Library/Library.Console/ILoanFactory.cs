namespace Library
{
    public interface ILoanFactory
    {
        Loan Create(User user, Book book);
    }
}