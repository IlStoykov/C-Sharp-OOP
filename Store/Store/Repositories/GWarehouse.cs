using Store.GeneralWarehouse.Contracts;
using Store.Models.Contracts;
using Store.Utilities.Messages;

namespace Store.GeneralWarehouse
{
    public class GWarehouse<T> : IGeneralWarehouse<T> where T : IProduct
    {
        private List<T> items;
        private List<T> itemsFordeliver;
        private List<T> result;
        private string[] listStoreType = new string[]{"BookStore", "OfficeStore" };
        
        public GWarehouse() { 
            items = new List<T>();
            itemsFordeliver = new List<T>();
            result = new List<T>();
        }
        public void Add(T item)
        {
            items.Add(item);
        }
        public int Count() => items.Count;
        public void Remove(T item) => items.Remove(item);
        

        public List<T> ProduceDelivery(string storeType, int number)
        {            
            
            if (!listStoreType.Contains(storeType)){
                throw new ArgumentException(ExceptionMessages.StoreType);
            }
            if (storeType == "OfficeStore") {
                itemsFordeliver = items.Where(x => x.GetType().Name == "Pen" || x.GetType().Name == "Pencil").Take(number).ToList();
            }
            else if (storeType == "BookStore") {
                itemsFordeliver = items.Where(x => x.GetType().Name == "NovelBook" || x.GetType().Name == "CoockingBook").Take(number).ToList();
            }                       
            items.RemoveAll(x=> itemsFordeliver.Contains(x));
            return itemsFordeliver;
        }
        public IReadOnlyCollection<T> Warehouse()
        {
            return items.AsReadOnly();
        }
    }
}
