
namespace RobotService.Models.Contracts
{
    public interface IStore
    {
        const int WhereHouseMaxLimit = 10;
        const int WhereHouseMinLimit = 3;       

        string StoreType { get; }
         List<object> WareHouse { get; }  
        double Turnover { get; }
        double Profit { get; }   

        string Order(string kind);
    }
}
