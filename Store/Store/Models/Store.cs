using Store.Models.Contracts;
using Store.Utilities.Messages;
using Store.Repositories;
using System.Text;


namespace Store.Models
{
    public abstract class Store<T> : IStore<T> where T : class, IProduct
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
       
        public int CheckWareHouseCapacity()
        {
            int numOfGoodsForDelvery = 0;
            if (WareHouse.Count == WareHouseMinLimit)
            {
                numOfGoodsForDelvery = WareHouseMaxLimit - WareHouse.Count;
                throw new ArgumentException(String.Format(ExceptionMessages.OutOfStock, StoreName, GetType().Name, numOfGoodsForDelvery));            
            }
            return numOfGoodsForDelvery;
        }
        public void AcceptDelivery(List<Object> deliveryItems) {
            foreach (var item in deliveryItems) {
                if (item is T validItem && WareHouse.Count < WareHouseMaxLimit) { 
                    WareHouse.Add(validItem);
                }
            }
        }
        public abstract string GetInventory();      
    }

}
