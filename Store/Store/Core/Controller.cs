using Store.Core.Contracts;
using Store.GeneralWarehouse;
using Store.Models;
using Store.Models.Contracts;
using Store.Repositories;
using Store.Utilities.Messages;



namespace Store.Core
{
    public class Controller : IController
    {        
        private GWarehouse<object> generalWarehouse;
        private StoreRepository<IStore> storeRepository;
        private int counter = 0;
        private string[] possibleStoreTypes = new string[] { "BookStore", "OfficeStore" };
        private string[] officeProducts = new string[] { "Pen", "Pencil" };
        private string[] books = new string[] { "NovelBook", "CoockingBook" };
        public Controller() {
            generalWarehouse = new GWarehouse<object>();
            storeRepository = new StoreRepository<IStore>();            
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
            storeRepository.Add(newStore);
            return String.Format(OutputMessages.StoreAdded, storeName);
            
        }
        public string CreateProduct(string productType, string origin, string titleCount, double price, int productNumber)
        {
            object newProduct = null;
            if (officeProducts.Contains(productType)){
                counter++;
                if (productType == "Pen") {                    
                    newProduct = new Pen(origin, titleCount, price, counter);
                }
                else{
                    newProduct = new Pencil(origin, titleCount, price, counter);
                }
            }
            else {
                counter++;
                if (productType == "NovelBook")
                {
                    newProduct = new NovelBook(origin, titleCount, price, counter);
                }
                else {
                    newProduct = new CoockingBook(origin, titleCount, price, counter);
                }
            }
            generalWarehouse.Add(newProduct);
            return String.Format(OutputMessages.ItemAdde, newProduct.GetType().Name, price, counter) + Environment.NewLine;
        }
    }
}
