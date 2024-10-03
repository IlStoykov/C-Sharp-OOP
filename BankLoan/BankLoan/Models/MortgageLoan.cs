namespace BankLoan.Models
{
    public class MortgageLoan : Loan
    {
        private const int FixedInterestRate = 1;
        private const double FixedAmount = 10000.0;
        public MortgageLoan() : base(FixedInterestRate, FixedAmount)
        {
        }
    }
}
