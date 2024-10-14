

namespace Store.Models.Contracts
{
    public interface IOfficeSupplies
    {
        string Color { get; }
        string Manufacturer { get; }
        bool IsPackage { get; }
        double Price { get; }
    }
}
