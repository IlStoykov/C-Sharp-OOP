namespace BankLoan.Models
{
    public class MortgageLoan : Loan
    {
        private const int FixedInterestRate = 3;
        private const double FixedAmount = 50000.0;
        public MortgageLoan() : base(FixedInterestRate, FixedAmount)
        {
        }
    }
}
