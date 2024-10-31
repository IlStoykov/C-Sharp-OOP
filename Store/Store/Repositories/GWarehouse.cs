using Store.GeneralWarehouse.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Store.GeneralWarehouse
{
    public class GWarehouse<T> : IGeneralWarehouse<T>
    {
        private List<T> items;
        List<T> itemsFordeliver;
        public GWarehouse() { 
            items = new List<T>();
            itemsFordeliver = new List<T>();
        }
        public void Add(T item)
        {
            items.Add(item);
        }

        public List<T> ProduceDelivery(string storeType, int number)
        {
            Type typeFound = Type.GetType(storeType);
            
            if (typeFound == null){
                throw new ArgumentException($"Type '{storeType}' not found.");
            }

            itemsFordeliver = items.Where(x=>x.GetType() == typeFound).ToList();
            return itemsFordeliver.Take(number).ToList();
        }

        public IReadOnlyCollection<T> Warehouse()
        {
            return items.AsReadOnly();
        }
    }
}
