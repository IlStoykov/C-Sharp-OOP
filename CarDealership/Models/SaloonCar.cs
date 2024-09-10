

namespace CarDealership.Models
{
    public class SaloonCar : Vehicle
    {
        private const double IncreaseValue = 1.10;
        public SaloonCar(string model, double price) : base(model, price * IncreaseValue)
        {
        }
    }
}
