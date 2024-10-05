using BankLoan.Models.Contracts;
using BankLoan.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BankLoan.Repositories
{
    public class LoanRepository : IRepository<ILoan>
    {
        private List<ILoan> loans;
        ILoan modelFound = null;
        public LoanRepository() {
            loans = new List<ILoan>();
        }
        public IReadOnlyCollection<ILoan> Models => loans.AsReadOnly();

        public void AddModel(ILoan model)
        {
            modelFound = loans.FirstOrDefault(x => x.Equals(model));
            if (modelFound == null) {
                loans.Add(model);
            }
        }
        public ILoan FirstModel(string name)
        {
            return loans.FirstOrDefault(x => x.GetType().Name == name);
        }

        public bool RemoveModel(ILoan model)
        {
            modelFound = loans.FirstOrDefault(x => x.Equals(model));
            if (modelFound != null)
            {
                loans.Remove(model);
                return true;
            }
            return false;
        }
    }
}
