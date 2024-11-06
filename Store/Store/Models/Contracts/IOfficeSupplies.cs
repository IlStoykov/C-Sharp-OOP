

namespace Store.Models.Contracts
{
    public interface IOfficeSupplies
    {
        string Color { get; }
        string Manufacturer { get; }        
        double Price { get; }
        int ProductNumber { get; }
    }
}
