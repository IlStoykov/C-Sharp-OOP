namespace Store.Models.Contracts
{
    public interface IBook
    {
        string Author { get; }
        string Title { get; }
        double Price { get; }
        int ProductNumber { get; }
    }
}
