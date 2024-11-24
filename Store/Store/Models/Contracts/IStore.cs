
namespace Store.Models.Contracts
{
    public interface IStore
    {
        int WareHouseMaxLimit { get; }
        int WareHouseMinLimit { get; }
        string StoreName { get; set; }        
        double Turnover { get; }
        double Profit { get; } 
        string Order(string toke1, string token2);
        int CheckWareHouseCapacity();
        void AcceptDelivery(List<object> deliveryItems);
        string GetInventory();       
        string Report();
    }
    public interface IStore<T> : IStore where T : class {
        IReadOnlyCollection<T> WareHouse { get; }
    }
}
