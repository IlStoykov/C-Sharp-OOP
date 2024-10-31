using Store.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repositories.Contracts
{
    public interface IStoreRepository<TStore, T> where TStore : IStore<T> where T:class
    {
        ReadOnlyCollection<TStore> Stores();
        void Add(TStore store);
        void FindByName(string name);
    }
}
