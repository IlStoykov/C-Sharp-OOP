namespace Store.Core.Contracts
{
    public interface IController
    {
        string CreateStore(string storeType, string storeName);
        string CreateProduct(string productType, string origin, string titleCount, double price, int productNumber);
        string Delivery(string storeName);
        string GetInventory(string storename);
        string OrderOfficeSupply(string storeName, string item, string color);
        string OrderBook(string storeName, string author, string title);
    }
}
