using Store.Models.Contracts;
using Store.Utilities.Messages;
using Store.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Store.GeneralWarehouse;

namespace Store.Models
{
    public abstract class Store<T> : IStore<T> where T : class, IOfficeSupplies
    {        
        private int orderCount = 0;
        private string storeType;
        private string storeName;        
        private List<T> storeWarehouse;
        private Dictionary<string, double> profitTable = 
            new Dictionary<string, double>() { { "Pen", 1.05 }, { "Pencil", 2.45}, { "NovelBook", 4.55}, { "CoockingBook", 6.85} };

        public Store(string name)
        {            
            this.storeName = name;
            storeWarehouse = new List<T>();
        }        
        public string StoreName{
            get => storeName;
            private set {
                if (string.IsNullOrWhiteSpace(value)) {
                    throw new ArgumentException(ExceptionMessages.StoreName);
                }
                storeName = value;
            }
        }
        public List<T> WareHouse => storeWarehouse;

        public double Turnover { get; private set; }

        public double Profit { get; private set; }

        public string Order(string kind)
        {
            IOfficeSupplies suppliceFound = storeWarehouse.FirstOrDefault(x => x.GetType().Name == kind);
            CheckNullAndThrow(suppliceFound, kind);
            
            double profitAdding = profitTable[kind];
            double totalIncome = suppliceFound.Price + profitAdding;
            
            Profit += profitAdding;
            Turnover += totalIncome;
            WareHouse.Remove((T)suppliceFound);
            orderCount ++;
            return $"A {suppliceFound.GetType().Name} was sold on a price of {totalIncome}";
        }        
        
        
        private void CheckNullAndThrow(IOfficeSupplies suppliceFound, string kind) {
            if (suppliceFound == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.OutOfStock, kind));
            }
        }
    }
    
}
