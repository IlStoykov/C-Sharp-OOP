using EDriveRent.Core.Contracts;
using EDriveRent.Models;
using EDriveRent.Models.Contracts;
using EDriveRent.Repositories;
using EDriveRent.Repositories.Contracts;
using EDriveRent.Utilities.Messages;
using System;
using System.Linq;
using System.Text;


namespace EDriveRent.Core
{
    public class Controller : IController
    {
        private IRepository<IUser> users;
        private IRepository<IVehicle> vehicles;
        private IRepository<IRoute> routes;
        private string[] vehicleTypeList = new string[] { "PassengerCar", "CargoVan" };

        public Controller() {         
            users = new UserRepository();
            vehicles = new VehicleRepository();
            routes = new RouteRepository();
        }
        public string AllowRoute(string startPoint, string endPoint, double length)
        {
            int routeNum = routes.GetAll().Count() + 1;
            IRoute routeFound = routes.GetAll().FirstOrDefault(x => x.StartPoint == startPoint && x.EndPoint == endPoint);

            if (routeFound != null) {

                if (routeFound.Length == length)
                {
                    return string.Format(OutputMessages.RouteExisting, startPoint, endPoint, length);
                }
                else if (routeFound.Length < length)
                {
                    return string.Format(OutputMessages.RouteIsTooLong, startPoint, endPoint);
                }
                else if (routeFound.Length > length) { 
                    routeFound.LockRoute();                   
                }
            }
            IRoute newRoute = new Route(startPoint, endPoint, length, routeNum);
            routes.AddModel(newRoute);
            return string.Format(OutputMessages.NewRouteAdded, startPoint, endPoint, length);
        }

        public string MakeTrip(string drivingLicenseNumber, string licensePlateNumber, string routeId, bool isAccidentHappened)
        {
            var userFound = users.Find(x => x.DrivingLicenseNumber == drivingLicenseNumber);
            var vehicleFound = vehicles.Find(x => x.LicensePlateNumber == licensePlateNumber);
            var routeFound = routes.Find(x => x.RouteId == int.Parse(routeId));

            if (userFound.IsBlocked) {
                return string.Format(OutputMessages.UserBlocked, drivingLicenseNumber);
            }
            if (vehicleFound.IsDamaged) {
                return string.Format(OutputMessages.VehicleDamaged, licensePlateNumber);
            }
            vehicleFound.Drive(routeFound.Length);
            if (isAccidentHappened)
            {
                vehicleFound.ChangeStatus();
                userFound.DecreaseRating();
            }
            else {
                userFound.IncreaseRating();
            }
            
            return $"{vehicleFound.Brand} {vehicleFound.Model} License plate: {vehicleFound.LicensePlateNumber} " +
                $"Battery: {vehicleFound.BatteryLevel}% Status: {(vehicleFound.IsDamaged ? "damaged" : "OK")}";
        }

        public string RegisterUser(string firstName, string lastName, string drivingLicenseNumber)
        {
            var userFound = users.FindById(drivingLicenseNumber);
            if (userFound != null){
                return string.Format(OutputMessages.UserWithSameLicenseAlreadyAdded, drivingLicenseNumber);
            }
            users.AddModel(new User(firstName, lastName, drivingLicenseNumber));
            return string.Format(OutputMessages.UserSuccessfullyAdded, firstName, lastName, drivingLicenseNumber);
        }

        public string RepairVehicles(int count)
        {
            var selectedVehicles = vehicles.Where(x => x.IsDamaged == false).OrderBy(x => x.Brand).ThenBy(x => x.Model).Take(count).ToList();
            foreach (var vehicle in selectedVehicles) { 
                vehicle.ChangeStatus();
                vehicle.Recharge();
            }
            return $"{selectedVehicles.Count} vehicles are successfully repaired!";          

        }

        public string UploadVehicle(string vehicleType, string brand, string model, string licensePlateNumber)
        {
            IVehicle vehicleFound = vehicles.FindById(licensePlateNumber);
            IVehicle newVehicle = null;

            if (!vehicleTypeList.Contains(vehicleType))
            {
                return string.Format(OutputMessages.VehicleTypeNotAccessible, vehicleType);
            }
            else {
                if (vehicleFound != null)
                {
                    return string.Format(OutputMessages.LicensePlateExists, licensePlateNumber);
                }
                if (vehicleType == "PassengerCar")
                {
                    newVehicle = new PassengerCar(brand, model, licensePlateNumber);
                }
                if (vehicleType == "CargoVan")
                {
                    newVehicle = new CargoVan(brand, model, licensePlateNumber);
                }
                vehicles.AddModel(newVehicle);
                return string.Format(OutputMessages.VehicleAddedSuccessfully, brand, model, licensePlateNumber);
            }            
        }

        public string UsersReport()
        {
            var arrangedUsers = users.OrderByDescending(x => x.Rating).ThenBy(x =>x.LastName).ThenBy(x =>x.FirstName);
            StringBuilder result = new StringBuilder();
            result.AppendLine("*** E-Drive-Rent ***");
            foreach (var user in arrangedUsers) {
                result.AppendLine(user.ToString());
            }
            return result.ToString().Trim();
        }
    }
}
