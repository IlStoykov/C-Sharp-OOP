using BankLoan.Models.Contracts;
using BankLoan.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Repositories
{
    public class LoanRepository : IRepository<ILoan>
    {
        private List<ILoan> loans;
        ILoan modelFound = null;
        LoanRepository() {
            loans = new List<ILoan>();
        }
        public IReadOnlyCollection<ILoan> Models => loans.AsReadOnly();

        public void AddModel(ILoan model)
        {
            modelFound = loans.FirstOrDefault(x => x.Amount == model.Amount && x.InterestRate == model.InterestRate);
            if (modelFound == null) {
                loans.Add(model);
            }
        }
        public ILoan FirstModel(string name)
        {
            return loans.First(x => x.GetType().Name == name);
        }

        public bool RemoveModel(ILoan model)
        {
            modelFound = loans.FirstOrDefault(x => x.Amount == model.Amount && x.InterestRate == model.InterestRate);
            if (modelFound != null)
            {
                loans.Remove(model);
                return true;
            }
            return false;
        }
    }
}
