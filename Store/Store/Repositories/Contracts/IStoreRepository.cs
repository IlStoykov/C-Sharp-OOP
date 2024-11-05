using Store.Models.Contracts;
using System.Collections.ObjectModel;


namespace Store.Repositories.Contracts
{
    public interface IStoreRepository<TStore, T> where TStore : IStore<T> where T:class
    {
        ReadOnlyCollection<TStore> Models { get; }
        string Add(TStore store);
        bool FindByName(string name);
    }
}
