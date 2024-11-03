namespace Store.GeneralWarehouse.Contracts
{
    public interface IGeneralWarehouse<T>
    {
        public IReadOnlyCollection<T> Warehouse();
        void Add(T item);
        public List<T> ProduceDelivery(string storeType, string storeName, int number);

    }
}
