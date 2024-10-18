using RobotService.Models.Contracts;
using Store.Models.Contracts;
using Store.Utilities.Messages;


namespace Store.Models
{
    public class Store<T> : IStore<T> where T : class, IOfficeSupplies
    {
        private int orderCount = 0;
        private string storeType;
        private string[] storeTypes = new string[] { "BookStore", "OfficeStore" };
        private List<T> storeWarehouse;
        private Dictionary<string, double> profitTable = 
            new Dictionary<string, double>() { { "Pen", 1.05 }, { "Pencil", 2.45}, { "NovelBook", 4.55}, { "CoockingBook", 6.85} };

        public Store(string storeType)
        {
            this.storeType = storeType;
            storeWarehouse = new List<T>();
        }
        public string StoreType { 
            get => storeType;
            set {
                if (!storeTypes.Contains(value)) {
                    throw new ArgumentException(ExceptionMessages.StoreType);
                }
                storeType = value;
            }         
        }
        public List<T> WareHouse => storeWarehouse;

        public double Turnover { get; private set; }

        public double Profit { get; private set; }

        public string Order(string kind)
        {
            IOfficeSupplies suppliceFound = storeWarehouse.FirstOrDefault(x => x.GetType().Name == kind);
            CheckNullAndThrow(suppliceFound, kind);

            //switch (kind)
            //{
            //    case "Pen":                    
            //        if (suppliceFound == null)
            //        {
            //            throw new ArgumentException(string.Format(ExceptionMessages.OutOfStock, kind));
            //        }
            //        break;
            //    case "Pencil":                    
            //        if (suppliceFound == null)
            //        {
            //            throw new ArgumentException(string.Format(ExceptionMessages.OutOfStock, kind));
            //        }
            //        break;
            //    case "NovelBook":                   
            //        if (suppliceFound == null)
            //        {
            //            throw new ArgumentException(string.Format(ExceptionMessages.OutOfStock, kind));
            //        }
            //        break;
            //    case "CoockingBook":                    
            //        if (suppliceFound == null)
            //        {
            //            throw new ArgumentException(string.Format(ExceptionMessages.OutOfStock, kind));
            //        }
            //        break;
            //}
            double profitAdding = profitTable[kind];
            double totalIncome = suppliceFound.Price + profitAdding;
            
            Profit += profitAdding;
            Turnover += totalIncome;
            WareHouse.Remove((T)suppliceFound);
            orderCount ++;
            return $"A {suppliceFound.GetType().Name} was sold on a price of {totalIncome}";
        }
        public void StockLoading() {

            return;
        }
        private void CheckNullAndThrow(IOfficeSupplies suppliceFound, string kind) {
            if (suppliceFound == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.OutOfStock, kind));
            }
        }
    }
    
}
