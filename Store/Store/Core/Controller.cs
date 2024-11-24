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
        private GWarehouse<IProduct> generalWarehouse;
        private StoreRepository<IStore> storeRepository;
        private int counter = 0;
        private string[] possibleStoreTypes = new string[] { "BookStore", "OfficeStore" };
        private string[] officeProducts = new string[] { "Pen", "Pencil" };
        private string[] books = new string[] { "NovelBook", "CoockingBook" };
        private List<string> storeFirstDeliveryList = new List<string>();
        IStore storeFound = null;
        List<IProduct> deliveryObjects = new List<IProduct>();

        public Controller() {
            generalWarehouse = new GWarehouse<IProduct>();
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
            IProduct newProduct = null;
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
        
        public string Delivery(string storeName) {
            int deliveryItem = 0;
            storeFound = storeRepository.FindByName(storeName);
            if (storeFound == null) {
                return String.Format(OutputMessages.NoSuchStore, storeName);
            }
            else if (!storeFirstDeliveryList.Contains(storeName))
            {
                deliveryItem = storeFound.WareHouseMaxLimit;
                storeFirstDeliveryList.Add(storeName);
            }

            else {
                deliveryItem = storeFound.WareHouseMaxLimit - storeFound.CheckWareHouseCapacity();
            }
            
            string storeType = storeFound.GetType().IsGenericType ? storeFound.GetType().GetGenericTypeDefinition().Name.Split('`')[0] : storeFound.GetType().Name;
            deliveryObjects = generalWarehouse.ProduceDelivery(storeType, deliveryItem);
            storeFound.AcceptDelivery(deliveryObjects.Cast<object>().ToList());
            
            return String.Format(OutputMessages.StoreDelivery, storeType, storeFound.StoreName, deliveryItem);
        }
        
        public string TempDelivery(string storeName, int itemNum) 
        {                  
            storeFound = storeRepository.FindByName(storeName);
            string storeType = storeFound.GetType().IsGenericType ? storeFound.GetType().GetGenericTypeDefinition().Name.Split('`')[0] : storeFound.GetType().Name;
            deliveryObjects = generalWarehouse.ProduceDelivery(storeType, itemNum);
            storeFound.AcceptDelivery(deliveryObjects.Cast<object>().ToList());
            return String.Format(OutputMessages.StoreDelivery, storeType, storeFound.StoreName, itemNum);
        }
        
        public string GetInventory(string storeName)
        {
            storeFound = storeRepository.FindByName(storeName);
            if (storeFound == null)
            {
                return String.Format(OutputMessages.NoSuchStore, storeName);
            }
            return storeFound.GetInventory();
        }
        
        public string OrderOfficeSupply(string storeName, string item, string color) {
            IStore storeFound = storeRepository.FindByName(storeName); 
            if (storeFound == null) {
                return String.Format(OutputMessages.NoSuchStore, storeName);
            }
            return storeFound.Order(item, color);         
        }
        
        public string OrderBook(string storeName, string author, string title) {
            IStore storeFound = storeRepository.FindByName(storeName);
            if (storeFound == null){
                return String.Format(OutputMessages.NoSuchStore, storeName);
            }
            return storeFound.Order(author, title);
        }
        
        public string Report(string storeName) {
            IStore storeFound = storeRepository.FindByName(storeName);
            if (storeFound == null)
            {
                return String.Format(OutputMessages.NoSuchStore, storeName);
            }
            return storeFound.Report();
        }
    }
}
