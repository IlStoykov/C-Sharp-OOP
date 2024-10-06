using BankLoan.Core.Contracts;
using BankLoan.Models;
using BankLoan.Models.Contracts;
using BankLoan.Repositories;
using BankLoan.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BankLoan.Core
{
    public class Controller : IController
    {
        private LoanRepository loans;
        private BankRepository banks;
        private string[] bankTypes = new string[] { "BranchBank", "CentralBank" };
        //private string[] loanTypes = new string[] { "StudentLoan", "MortgageLoan" };
        private string[] clientsTypes = new string[] { "Student", "Adult" };
        public Controller() { 
            loans = new LoanRepository();
            banks = new BankRepository();
        }

        public string AddBank(string bankTypeName, string name)
        {
            IBank newBank = null;
            if (!bankTypes.Contains(bankTypeName)) {
                throw new ArgumentException("Invalid bank type.");
            }
            if (bankTypeName == "BranchBank"){
                newBank = new BranchBank(name);
            }
            if (bankTypeName == "CentralBank") { 
                newBank = new CentralBank(name);
            }
            banks.AddModel(newBank);
            return string.Format(OutputMessages.BankSuccessfullyAdded, newBank.GetType().Name);
        }

        public string AddClient(string bankName, string clientTypeName, string clientName, string id, double income)
        {
            IBank bankFound = null;
            IClient newClient = null;
            if (!clientsTypes.Contains(clientTypeName)) {
                throw new ArgumentException("Invalid client type.");
            }
            bankFound = banks.FirstModel(bankName);
            if ((clientTypeName == "Student" && !(bankFound is BranchBank)) || (clientTypeName == "Adult" && !(bankFound is CentralBank))) {
                return string.Format(OutputMessages.UnsuitableBank);
            }
            switch (clientTypeName) {
                case "Student":
                    newClient = new Student(clientName, id, income);
                    break;

                case "Adult":
                    newClient = new Adult(clientName, id, income);
                    break;
            }
            bankFound.AddClient(newClient);
            return string.Format(OutputMessages.ClientAddedSuccessfully, clientTypeName, bankFound.Name);
        }

        public string AddLoan(string loanTypeName)
        {
            ILoan newLoan = null;
            switch (loanTypeName)
            {
                case "StudentLoan":
                    newLoan = new StudentLoan();
                    break;
                case "MortgageLoan":
                    newLoan = new MortgageLoan();
                    break;
                default: throw new ArgumentException("Invalid loan type.");
            }   
            loans.AddModel(newLoan);
            return string.Format(OutputMessages.LoanSuccessfullyAdded, newLoan.GetType().Name);
        }

        public string FinalCalculation(string bankName)
        {
            
            IBank bankFound = banks.FirstModel(bankName);
            
            double totalIncomeFromClients = bankFound.Clients.Sum(client => client.Income);          
            double totalLoanAmount = bankFound.Loans.Sum(loan => loan.Amount);            
            double totalFunds = totalIncomeFromClients + totalLoanAmount;

            return $"The funds of bank {bankName} are {totalFunds:F2}.";
        }

        public string ReturnLoan(string bankName, string loanTypeName)
        {
            ILoan returnLoan = loans.FirstModel(loanTypeName);
            IBank bankFound = banks.FirstModel(bankName);

            if (returnLoan == null){
                return $"Loan of type {loanTypeName} is missing.";
            }
            loans.RemoveModel(returnLoan);
            bankFound.AddLoan(returnLoan);
            return $"{loanTypeName} successfully added to {bankName}";
        }

        public string Statistics()
        {
            throw new NotImplementedException();
        }
    }
}
