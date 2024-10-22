using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.GeneralWarehouse.Contracts
{
    public interface IGeneralWarehouse<T>
    {
        public IReadOnlyCollection<T> Warehouse();
        void Add(T item);
        public List<T> ProduceDelivery(string storeType, int number);



    }
}
