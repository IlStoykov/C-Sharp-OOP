using Store.Models.Contracts;
using Store.Utilities.Messages;
using System.Text;
using System.Threading.Tasks;

namespace Store.Models
{
    public class OfficeStore<T> : Store<OfficeSupplies> {      
       
        private string storeName;
        private Dictionary<string, double> profitTable =
            new Dictionary<string, double>() { { "Pen", 1.05 }, { "Pencil", 2.45 } };
        public OfficeStore(string name): base(name) { 
            StoreName = name;        
        }
        public override string StoreName { 
            get => storeName;
            set { 
                if (string.IsNullOrWhiteSpace(value)) {
                    throw new ArgumentNullException(ExceptionMessages.StoreName);
                }
                storeName = value;
            }  
        }
        public override int WareHouseMaxLimit => 7;
        public override int WareHouseMinLimit => 3;
        public override string Order(string item, string color)
        {
            var itemFound = WareHouse.FirstOrDefault(x => x.GetType().Name == item && x.Color == color);
            if (itemFound != null)
            {
                double itemProfit = profitTable[item];
                double itemTotalPrice = itemFound.Price + itemProfit;

                Profit += itemProfit;
                Turnover += itemTotalPrice;
                storeWarehouse.Remove(itemFound);    
                CheckWareHouseCapacity();
                return $"A {itemFound.GetType().Name} with color {itemFound.Color} was sold on a price of {itemTotalPrice:f2}." + Environment.NewLine;

            }
            else {
                return $"{item} with {color} is out of stock" + Environment.NewLine;
            }
        }
        public override string GetInventory()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine($"Store name {StoreName} invetori contain {WareHouse.Count()}:");
            foreach (var item in WareHouse)
            {                
                result.AppendLine($"Item type {item.GetType().Name}, producer {item.Origin}");
            }
            return result.ToString().TrimEnd();
        }
        
    }
}
