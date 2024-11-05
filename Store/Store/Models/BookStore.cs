using Store.Models.Contracts;
using Store.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Models
{
    public class BookStore<T> : Store<Book>
    {
        public override int WhereHouseMinLimit => 2;
        public override int WhereHouseMaxLimit => 8;

        private string storeName;
        private Dictionary<string, double> profitTable = new Dictionary<string, double> { { "NovelBook", 4.55 }, { "CoockingBook", 6.85 } };

        public BookStore(string name): base(name) { 
            
        }
        public override string StoreName { 
            get => storeName;
            set {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(ExceptionMessages.StoreName);
                }
                storeName = value;
            }
        }

        public override string Order(string item)
        {
            IBook itemFound = WareHouse.FirstOrDefault(x => x.GetType().Name == item);
            if (itemFound != null)
            {
                double itemProfit = profitTable[item];
                double itemTotalPrice = itemFound.Price + itemProfit;

                Profit += itemProfit;
                Turnover += itemTotalPrice;
                CheckWareHouseCapacity();
                return $"A {itemFound} was sold on a price of {itemTotalPrice}.";
            }
            else {
                return $"{item} is out of stock";
            }
        }
    }
}
