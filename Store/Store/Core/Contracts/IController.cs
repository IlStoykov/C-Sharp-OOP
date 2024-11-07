namespace Store.Core.Contracts
{
    public interface IController
    {
        string CreateStore(string storeType, string storeName);
        string CreateProduct(string productType, string origin, string titleCount, double price, int productNumber);
    }
}
