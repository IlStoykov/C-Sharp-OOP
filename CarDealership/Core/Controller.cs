using CarDealership.Core.Contracts;
using CarDealership.Models;
using CarDealership.Models.Contracts;
using CarDealership.Utilities.Messages;


namespace CarDealership.Core
{
    public class Controller : IController
    {
        string[] customerType = new[] { "IndividualClient", "LegalEntityCustomer" };
        string[] vehicleType = new[] { "SaloonCar", "SUV", "Truck" };
        private Dealership dealership;
        public Controller() {
            dealership = new Dealership();
        } 
        public string AddCustomer(string customerTypeName, string customerName)
        {
            ICustomer newCustomer = null;
            if (!customerType.Contains(customerTypeName)) {
                return string.Format(OutputMessages.InvalidType, customerTypeName);
            }
            if (dealership.Customers.Exists(customerName)) {
                return string.Format(OutputMessages.CustomerAlreadyAdded, customerName);
            }
            if (customerTypeName == "IndividualClient")
            {
                newCustomer = new IndividualClient(customerName);
            }
            if (customerTypeName == "LegalEntityCustomer")
            {
                newCustomer = new IndividualClient(customerName);
            }
            dealership.Customers.Add(newCustomer);
            return string.Format(OutputMessages.CustomerAddedSuccessfully, customerName);                  
        }

        public string AddVehicle(string vehicleTypeName, string model, double price)
        {
            IVehicle newVehicle = null;
            if (!vehicleType.Contains(vehicleTypeName)) {
                return string.Format(OutputMessages.InvalidType, vehicleTypeName);
            }
            if (dealership.Vehicles.Exists(model)) {
                return string.Format(OutputMessages.VehicleAlreadyAdded, model);
            }
            if (vehicleTypeName == "SaloonCar") {
                newVehicle = new SaloonCar(model, price);
            }
            if (vehicleTypeName == "SUV")
            {
                newVehicle = new SUV(model, price);
            }
            if (vehicleTypeName == "Truck")
            {
                newVehicle = new Truck(model, price);
            }
            dealership.Vehicles.Add(newVehicle);
            return string.Format(OutputMessages.VehicleAddedSuccessfully, vehicleTypeName, model, newVehicle.Price.ToString("F2"));
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
