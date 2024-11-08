using Store.Models.Contracts;
using System.Collections.ObjectModel;


namespace Store.Repositories.Contracts
{
    public interface IStoreRepository<TStore, T> where TStore : IStore<T> where T:class
    {
        ReadOnlyCollection<TStore> Models { get; }
        void Add(TStore store);
        T FindByName(string name);
    }
}
