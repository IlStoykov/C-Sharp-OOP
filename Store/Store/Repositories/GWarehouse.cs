using Store.Repositories.Contracts;
using Store.Models.Contracts;
using System; 
using Store.Utilities.Messages;
using System.Collections.ObjectModel;

namespace Store.Repositories
{
    public class StoreRepository<TStore, T> : IStoreRepository<TStore, T> where TStore : IStore<T> where T : class
    {
        private List<TStore> stores;
        public StoreRepository() { 
            stores = new List<TStore>();
        }
        public string Add(TStore store)
        {
            var storeFound = stores.FirstOrDefault(x => x.StoreName == store.StoreName);
            if (storeFound != null)
            {
                return String.Format(OutputMessages.StoreExist, storeFound.StoreName);
            }
            stores.Add(store);
            return String.Format(OutputMessages.StoreAdded, storeFound.StoreName);
        }

        public bool FindByName(string name)
        {
            return stores.Any(x => x.StoreName == name);
        }

        public ReadOnlyCollection<TStore> Models => stores.AsReadOnly();        
    }
}
