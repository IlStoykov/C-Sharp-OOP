using Store.Models.Contracts;
using Store.Utilities.Messages;
using Store.Repositories;
using System.Text;
using Store.GeneralWarehouse;


namespace Store.Models
{
    public abstract class Store<T> : IStore<T> where T : class, IProduct
    {
        
        protected GWarehouse<T> storeWarehouse;        

        public Store(string name)
        {                        
            storeWarehouse = new GWarehouse<T>();
        }
        public abstract int WareHouseMaxLimit { get; }
        public abstract int WareHouseMinLimit { get; }
        public abstract string StoreName { get; set; }
        public IReadOnlyCollection<T> WareHouse => storeWarehouse.Warehouse();

        public double Turnover { get; protected set; }

        public double Profit { get; protected set; }        
       
        public int CheckWareHouseCapacity()
        {
            int numOfGoodsForDelvery = 0;
            if (WareHouse.Count <= WareHouseMinLimit)
            {
                numOfGoodsForDelvery = WareHouseMaxLimit - WareHouse.Count;
                throw new ArgumentException(String.Format(ExceptionMessages.OutOfStock, StoreName, GetType().Name, numOfGoodsForDelvery));            
            }
            return numOfGoodsForDelvery;
        }
        public void AcceptDelivery(List<Object> deliveryItems) {
            foreach (var item in deliveryItems) {
                if (item is T validItem && WareHouse.Count < WareHouseMaxLimit) { 
                    storeWarehouse.Add(validItem);
                }
            }
        }
        public abstract string Order(string toke1, string token2);
        public abstract string GetInventory();
        public string Report() {
            StringBuilder result = new StringBuilder();
            result.AppendLine($"Store name {StoreName}, store type {(GetType().Name).Split('`')[0]}");
            result.AppendLine($"Turnover {Turnover:F2}, profit {Profit:F2}");
            foreach (var item in WareHouse)
            {
                result.AppendLine(item.ToString());
            }

            return result.ToString();
        
        }

    }

}
