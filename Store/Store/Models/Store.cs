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
        public virtual int WhereHouseMaxLimit => 0;
        public virtual int WhereHouseMinLimit => 0;

        private List<T> storeWarehouse;
        

        public Store(string name)
        {                        
            storeWarehouse = new List<T>();
        }
        public abstract string StoreName { get; set; }
        public List<T> WareHouse => storeWarehouse;

        public double Turnover { get; private set; }

        public double Profit { get; private set; }

        public abstract string Order(string item);
       
        public void CheckWareHouseCapacity()
        {
            if (WareHouse.Count == WhereHouseMinLimit){
                int numOfGoodsForDelvery = WhereHouseMaxLimit - WareHouse.Count;
                throw new ArgumentException(String.Format(ExceptionMessages.OutOfStock, StoreName, GetType().Name, numOfGoodsForDelvery));            
            }
        }

    }

}
