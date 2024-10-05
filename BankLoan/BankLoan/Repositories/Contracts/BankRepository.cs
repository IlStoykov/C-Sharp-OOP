using BankLoan.Models.Contracts;
using BankLoan.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BankLoan.Repositories
{
    public class BankRepository : IRepository<IBank>
    {
        List<IBank> banks;
       
        BankRepository() { 
            banks = new List<IBank>();
        }
        public IReadOnlyCollection<IBank> Models => banks.AsReadOnly();

        public void AddModel(IBank model)
        {
            if (!banks.Any(x => x.Name == model.Name)){
                banks.Add(model);
            }
        }
        public IBank FirstModel(string name)
        {
            return banks.FirstOrDefault(x => x.Name == name);
        }

        public bool RemoveModel(IBank model)
        {
            if (banks.Any(x => x.Name == model.Name)) { 
                banks.Remove(model);
                return true;
            }
            return false;
        }
    }
}
