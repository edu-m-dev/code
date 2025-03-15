namespace Library
{
    public class Book
    {
        public enum BookType
        {
            Comic,
            Novel,
            Poems,
            NonFiction,
        }

        public required string Title { get; init; }

        public required string Author { get; init; }

        public BookType Type { get; init; }
    }
}
