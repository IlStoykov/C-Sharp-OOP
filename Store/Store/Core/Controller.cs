using Store.Core.Contracts;
using Store.GeneralWarehouse;


namespace Store.Core
{
    public class Controller : IController
    {
        private GWarehouse<object> generalWarehouse;
        public Controller() {
            generalWarehouse = new GWarehouse<object>();            
        }
        public string CreateStore(string storeType)
        {
            return storeType;
        }
    }
}
