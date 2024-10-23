using Store.Core.Contracts;
using Store.GeneralWarehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core
{
    public class Controller : IController
    {
        private GWarehouse<T> generalWarehouse;
        public Controller() {
            generalWarehouse = new GWarehouse<T>();            
        }
        public string CreateStore(string storeType)
        {
            return;
        }
    }
}
