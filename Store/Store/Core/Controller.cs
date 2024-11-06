using Store.Core.Contracts;
using Store.GeneralWarehouse;
using Store.Models;
using Store.Models.Contracts;
using Store.Utilities.Messages;


namespace Store.Core
{
    public class Controller : IController
    {        
        private GWarehouse<IStore> generalWarehouse;
        private string[] possibleStoreTypes = new string[] { "BookStore", "OfficeStore" };
        public Controller() {
            generalWarehouse = new GWarehouse<IStore>();            
        }
        public string CreateStore(string storeType, string storeName)
        {
            IStore newStore = null;
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
