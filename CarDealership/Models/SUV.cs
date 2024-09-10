

namespace CarDealership.Models
{
    public class SUV : Vehicle
    {
        private const double IncreaseValue = 1.20;
        public SUV(string model, double price) : base(model, price * IncreaseValue)
        {
        }
    }
}
