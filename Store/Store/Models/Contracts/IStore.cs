
namespace Store.Models.Contracts
{
    public interface IStore
    {
        int WareHouseMaxLimit { get; }
        int WareHouseMinLimit { get; }
        string StoreName { get; set; }        
        double Turnover { get; }
        double Profit { get; } 
        string Order(string item);
        int CheckWareHouseCapacity();
        void AcceptDelivery(List<object> deliveryItems);
        string GetInventory();
    }
    public interface IStore<T> : IStore where T : class {
        List<T> WareHouse { get; }
    }
}
