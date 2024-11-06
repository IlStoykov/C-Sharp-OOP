
namespace Store.Models.Contracts
{
    public interface IStore
    {                       
        string StoreName { get; set; }        
        double Turnover { get; }
        double Profit { get; } 
        string Order(string item);
        void CheckWareHouseCapacity();        
        }
    public interface IStore<T> : IStore where T : class {
        List<T> WareHouse { get; }
    }
}
