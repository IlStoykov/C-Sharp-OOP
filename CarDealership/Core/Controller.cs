using CarDealership.Core.Contracts;
using CarDealership.Models;
using CarDealership.Models.Contracts;
using CarDealership.Repositories.Contracts;
using CarDealership.Utilities.Messages;
using System.Text;


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
        public string PurchaseVehicle(string vehicleTypeName, string customerName, double budget)
        {
            if (!dealership.Customers.Exists(customerName)){
                return string.Format(OutputMessages.CustomerNotFound, customerName);
            }
            if (!dealership.Vehicles.Models.Any(x=>x.GetType().Name == vehicleTypeName)) {
                return string.Format(OutputMessages.VehicleTypeNotFound, vehicleTypeName);
            }
            //string customerType = dealership.Customers.Models.First(x=>x.Name == customerName).GetType().Name;
            ICustomer customerFound = dealership.Customers.Get(customerName);
            if (customerFound is LegalEntityCustomer && vehicleTypeName == "SaloonCar" || customerFound is IndividualClient && vehicleTypeName == "Truck") {
                return string.Format(OutputMessages.CustomerNotEligibleToPurchaseVehicle, customerName, vehicleTypeName);
            }
            IVehicle vehicleFound = dealership.Vehicles.Models.Where(v=>v.GetType().Name == vehicleTypeName && v.Price <= budget)
                .OrderByDescending(v=>v.Price).FirstOrDefault();
            if (vehicleFound != null){
                customerFound.BuyVehicle(vehicleFound.Model);
                vehicleFound.SellVehicle(vehicleFound.Model);
                return string.Format(OutputMessages.VehiclePurchasedSuccessfully, customerName, vehicleFound.Model);
            }
            return string.Format(OutputMessages.BudgetIsNotEnough, customerName, vehicleTypeName);
        }

        public string CustomerReport()
        {
            var customerOrderd = dealership.Customers.Models.OrderBy(n => n.Name);
            StringBuilder report = new StringBuilder();
            report.AppendLine("Customer Report:");

            foreach (var customer in customerOrderd){
                report.AppendLine(customer.ToString());
                report.AppendLine("-Models:");

                var models = customer.Purchases.OrderBy(p => p).ToList();
                if (customer.Purchases.Count == 0) {
                    report.AppendLine("--none");
                    continue;
                }
                else {
                    foreach (var model in models) { 
                        report.AppendLine($"--{model}");
                    }  
                }
            }
            return report.ToString().Trim();
        }        
        
        public string SalesReport(string vehicleTypeName)
        {
            var vehiclesOrdered = dealership.Vehicles.Models
                .Where(x => x.GetType().Name == vehicleTypeName)
                .OrderBy(x => x.Model);
            StringBuilder report = new StringBuilder();
            foreach (var vehicle in vehiclesOrdered)
            {
                report.AppendLine($"--{vehicle}");
            }
            var totalSales = vehiclesOrdered.Sum(v => v.SalesCount);
            report.AppendLine($"-Total Purchases: {totalSales}");

            return report.ToString().Trim();
        }
    }
}
