using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Models
{
    public class SaloonCar : Vehicle
    {
        private const double IncreaseValue = 1.1;
        public SaloonCar(string model, double price) : base(model, price * IncreaseValue)
        {
        }
    }
}
