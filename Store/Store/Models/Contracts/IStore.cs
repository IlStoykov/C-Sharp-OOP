namespace Store.Models.Contracts
{
    public interface IStore<T> where T : class
    {
        const int WhereHouseMaxLimit = 10;
        const int WhereHouseMinLimit = 3;       

        string StoreType { get; }
        string StoreName { get; }
        List<T>WareHouse { get; } 
        double Turnover { get; }
        double Profit { get; }   

        string Order(string kind);
    }
}
