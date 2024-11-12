using Store.Models.Contracts;
using Store.Utilities.Messages;
using System;
using System.Text;


namespace Store.Models
{
    public class BookStore<T> : Store<Book>
    {
        private string storeName;
        private Dictionary<string, double> profitTable = new Dictionary<string, double> { { "NovelBook", 4.55 }, { "CoockingBook", 6.85 } };

        public BookStore(string name): base(name) { 
            StoreName = name;
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
        public override int WareHouseMaxLimit => 8;
        public override int WareHouseMinLimit => 3;

        public override string Order(string item)
        {
            IProduct itemFound = WareHouse.FirstOrDefault(x => x.GetType().Name == item);
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
        public override string GetInventory()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine($"Store name {StoreName} invetori contain {WareHouse.Count()}:");
            foreach (var item in WareHouse) {
                result.AppendLine($"Author {item.Origin}, title {item.TitleIspackage}");
            }
            return result.ToString().TrimEnd();
        }
    }
}
