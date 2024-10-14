namespace Store.Models.Contracts
{
    public interface IBook
    {
        string Author { get; }
        string Title { get; }
        double Price { get; }
        string Genre { get; }
    }
}
