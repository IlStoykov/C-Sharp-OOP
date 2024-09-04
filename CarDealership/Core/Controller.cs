using CarDealership.Core.Contracts;
using CarDealership.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Core
{
    public class Controller : IController
    {
        string[] customerType = new[] { "IndividualClient", "LegalEntityCustomer" };
        public string AddCustomer(string customerTypeName, string customerName)
        {
            if (!customerType.Contains(customerTypeName)) {
                return string.Format(OutputMessages.InvalidType, customerTypeName);
            }
        }

        public string AddVehicle(string vehicleTypeName, string model, double price)
        {
            throw new NotImplementedException();
        }

        public string CustomerReport()
        {
            throw new NotImplementedException();
        }

        public string PurchaseVehicle(string vehicleTypeName, string customerName, double budget)
        {
            throw new NotImplementedException();
        }

        public string SalesReport(string vehicleTypeName)
        {
            throw new NotImplementedException();
        }
    }
}
