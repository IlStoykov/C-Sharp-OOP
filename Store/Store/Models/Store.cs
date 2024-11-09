using Store.Models.Contracts;
using Store.Utilities.Messages;
using Store.Repositories;


namespace Store.Models
{
    public abstract class Store<T> : IStore<T> where T : class
    {
        
        private List<T> storeWarehouse;        

        public Store(string name)
        {                        
            storeWarehouse = new List<T>();
        }
        public abstract int WareHouseMaxLimit { get; }
        public abstract int WareHouseMinLimit { get; }
        public abstract string StoreName { get; set; }
        public List<T> WareHouse => storeWarehouse;

        public double Turnover { get; protected set; }

        public double Profit { get; protected set; }

        public abstract string Order(string item);
       
        public void CheckWareHouseCapacity()
        {
            if (WareHouse.Count == WareHouseMinLimit)
            {
                int numOfGoodsForDelvery = WareHouseMaxLimit - WareHouse.Count;
                throw new ArgumentException(String.Format(ExceptionMessages.OutOfStock, StoreName, GetType().Name, numOfGoodsForDelvery));            
            }
        }

    }

}
