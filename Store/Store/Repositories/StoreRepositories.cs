using Store.Repositories.Contracts;
using Store.Models.Contracts;
using System; 
using Store.Utilities.Messages;
using System.Collections.ObjectModel;


namespace Store.Repositories
{
    public class StoreRepository<TStore>  where TStore : IStore
    {
        private List<TStore> stores;
        public StoreRepository() { 
            stores = new List<TStore>();
        }
        public void Add(TStore store)
        {
            var storeFound = stores.FirstOrDefault(x => x.StoreName == store.StoreName);
            if (storeFound == null)
            {
                stores.Add(store);
            }          
            
        }
        public IStore FindByName(string name)
        {
            return stores.FirstOrDefault(x => x.StoreName == name);
        }

        public ReadOnlyCollection<TStore> Models => stores.AsReadOnly();
        
    }
}
