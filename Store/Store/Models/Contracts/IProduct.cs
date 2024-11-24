namespace Store.Models.Contracts
{
    public interface IProduct
    {
        string Origin { get; }
        string TitleIspackage { get; }
        double Price { get; }
        int ProductNumber { get; }

    }
}
