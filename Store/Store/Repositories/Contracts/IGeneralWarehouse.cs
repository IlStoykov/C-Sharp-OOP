namespace Store.GeneralWarehouse.Contracts
{
    public interface IGeneralWarehouse<T>
    {
        public IReadOnlyCollection<T> Warehouse();
        void Add(T item);
        void Remove(T item);
        public int Count();
        public List<T> ProduceDelivery(string storeType, int number);
    }
}
