using Store.GeneralWarehouse.Contracts;
using Store.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Store.GeneralWarehouse
{
    public class GWarehouse<T> : IGeneralWarehouse<T>
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

        public List<T> ProduceDelivery(string storeType, string storeName, int number)
        {            
            
            if (!listStoreType.Contains(storeType)){
                throw new ArgumentException(ExceptionMessages.StoreType);
            }

            itemsFordeliver = items.Where(x => x.GetType().Name == storeType).ToList();
            result = itemsFordeliver.Take(number).ToList();
            items.RemoveAll(x=>result.Contains(x));
            return result;
        }

        public IReadOnlyCollection<T> Warehouse()
        {
            return items.AsReadOnly();
        }
    }
}
