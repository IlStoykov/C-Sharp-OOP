using Store.Core.Contracts;
using Store.GeneralWarehouse;
using Store.Models;
using Store.Utilities.Messages;

namespace Store.Core
{
    public class Controller : IController
    {        
        private GWarehouse<object> generalWarehouse;
        private string[] possibleStoreTypes = new string[] { "BookStore", "OfficeStore" };
        public Controller() {
            generalWarehouse = new GWarehouse<object>();            
        }
        public string CreateStore(string storeType, string storeName)
        {
            Store<object> newStore = null;
            if (!possibleStoreTypes.Contains(storeType)) {
                return string.Format(OutputMessages.InvalidStoreType);
            }
            if (storeType == "BookStore") {
                newStore = new BookStore<Book>(storeName); 
            }
            else if (storeType == "OfficeStore") {
                newStore = new OfficeStore<OfficeSupplies>(storeName);
            }
            generalWarehouse.Add(newStore);
            return String.Format(OutputMessages.StoreAdded, storeName);
            
        }
    }
}
