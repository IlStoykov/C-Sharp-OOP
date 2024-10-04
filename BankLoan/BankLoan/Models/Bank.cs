using BankLoan.Models.Contracts;
using BankLoan.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BankLoan.Models
{
    public abstract class Bank : IBank
    {
        private string name;
        private List<ILoan> loans;
        private List<IClient> clients;
        public Bank(string name, int capacity) { 
            Name = name;
            Capacity = capacity;
            loans = new List<ILoan>();
            clients = new List<IClient>();
        }
        public string Name {
            get => name;
            set {
                if (string.IsNullOrWhiteSpace(value)) {
                    throw new ArgumentNullException(ExceptionMessages.BankNameNullOrWhiteSpace);
                }
                name = value;
            }
        }
        public int Capacity { get; private set; }

        public IReadOnlyCollection<ILoan> Loans => loans.AsReadOnly();

        public IReadOnlyCollection<IClient> Clients => clients.AsReadOnly();

        public void AddClient(IClient Client)
        {
            if (clients.Count == Capacity) {
                throw new ArgumentException(ExceptionMessages.NotEnoughCapacity);
            }
            clients.Add(Client);
        }
        public void AddLoan(ILoan loan)
        {
            loans.Add(loan);
        }
        public string GetStatistics()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine($"Name: {Name}, Type: {GetType().Name}");
            result.AppendLine($"Clients: {(clients.Any() ? string.Join(", ", clients.Select(x => x.Name)) : "none")}");
            result.AppendLine($"Loans: {loans.Count}, Sum of Rates: {SumRates()}");
            return result.ToString().Trim();
        }
        public void RemoveClient(IClient Client)
        {
            IClient clientFound = clients.FirstOrDefault(x => x.Id == Client.Id);
            if (clientFound != null){
                clients.Remove(clientFound);
            }
        }
        public double SumRates() => loans.Sum(x => x.InterestRate);           
        
    }
}
