namespace BankLoan.Models
{
    public class StudentLoan : Loan
    {
        private const int FixedInterestRate = 1;
        private const double FixedAmount = 10000.0;
        public StudentLoan() : base(FixedInterestRate, FixedAmount)
        {
        }
    }
}
