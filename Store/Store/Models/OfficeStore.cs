using Store.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Store.Models
{
    public class OfficeStore<T> : Store<OfficeSupplies>
    {      
        public override int WhereHouseMinLimit => 2;
        public override int WhereHouseMaxLimit => 8;


        private string storeName;
        private Dictionary<string, double> profitTable =
            new Dictionary<string, double>() { { "Pen", 1.05 }, { "Pencil", 2.45 } };
        public OfficeStore(string name): base(name) { 
        
        }
        public override string StoreName { 
            get => storeName;
            set { 
                if (string.IsNullOrWhiteSpace(value)) {
                    throw new ArgumentNullException(ExceptionMessages.StoreName);
                }
            }  
         }

        public override string Order(string item)
        {
            var itemFound = WareHouse.FirstOrDefault(x => x.GetType().Name == item);
            if (itemFound != null)
            {
                double itemProfit = profitTable[item];
                double itemTotalPrice = itemFound.Price + itemProfit;

                Profit += itemProfit;
                Turnover += itemTotalPrice;
                WareHouse.Remove(itemFound);
                CheckWareHouseCapacity();
                return $"A {itemFound} was sold on a price of {itemTotalPrice};";

            }
            else {
                return $"{item} is out of stock";
            }
        }
    }
}
