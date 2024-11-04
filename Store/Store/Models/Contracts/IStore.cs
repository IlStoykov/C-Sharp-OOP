
namespace Store.Models.Contracts
{
    public interface IStore<T> where T : class
    {
                       
        string StoreName { get; set; }
        List<T>WareHouse { get; } 
        double Turnover { get; }
        double Profit { get; }   

        string Order(string item);
        void CheckWareHouseCapacity();
        
        }
}
