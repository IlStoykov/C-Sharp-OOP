using Store.Repositories.Contracts;
using Store.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RobotService.Models.Contracts;

namespace Store.Repositories
{
    public class StoreRepository<TStore, T> : IStoreRepository<TStore, T> where TStore : IStore<T> where T : class
    {
        public void Add(TStore store)
        {
            throw new NotImplementedException();
        }

        public void FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public System.Collections.ObjectModel.ReadOnlyCollection<TStore> Stores()
        {
            throw new NotImplementedException();
        }
    }
}
