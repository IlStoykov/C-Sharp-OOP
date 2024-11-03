using Store.Models.Contracts;
using Store.Utilities.Messages;
using Store.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Store.GeneralWarehouse;

namespace Store.Models
{
    public abstract class Store<T> : IStore<T> where T : class
    {
        const int WhereHouseMaxLimit = 10;
        const int WhereHouseMinLimit = 3;
        private string storeName;        
        private List<T> storeWarehouse;
        private Dictionary<string, double> profitTable = 
            new Dictionary<string, double>() { { "Pen", 1.05 }, { "Pencil", 2.45}, { "NovelBook", 4.55}, { "CoockingBook", 6.85} };

        public Store(string name)
        {            
            StoreName = name;
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
            double totalIncome = 0.0;
            var suppliceFound = storeWarehouse.FirstOrDefault(x => x.GetType().Name == kind);            
            if (suppliceFound == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.OutOfStock, kind));
            }
            double profitAdding = profitTable[kind];
            
            if (suppliceFound is IOfficeSupplies supplies) {
                totalIncome = supplies.Price + profitAdding;
            }
            else if(suppliceFound is IBook book) {
                totalIncome = book.Price + profitAdding;
            }
            
            Profit += profitAdding;
            Turnover += totalIncome;
            WareHouse.Remove((T)suppliceFound);
            CheckWareHouseCapacity();
            return $"A {suppliceFound.GetType().Name} was sold on a price of {totalIncome}";
        }
        private void CheckWareHouseCapacity()
        {
            if (WareHouse.Count == WhereHouseMinLimit){
                int numOfGoodsForDelvery = WhereHouseMaxLimit - WareHouse.Count;
                throw new ArgumentException(String.Format(ExceptionMessages.OutOfStock, storeName, GetType().Name, numOfGoodsForDelvery));            
            }
        }

    }

}
