using EDriveRent.Core.Contracts;
using EDriveRent.Models;
using EDriveRent.Models.Contracts;
using EDriveRent.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace EDriveRent.Core
{
    public class Controller : IController
    {
        private List<IUser> users;
        private List<IVehicle> vehicles;
        private List<IRoute> routes;
        private string[] vehicleTypeList = new string[] { "PassengerCar", "CargoVan" };

        public Controller() {         
            users = new List<IUser>();
            vehicles = new List<IVehicle>();
            routes = new List<IRoute>();
        }
        public string AllowRoute(string startPoint, string endPoint, double length)
        {
            if (routes.Any(x => x.StartPoint == startPoint && x.EndPoint == endPoint && x.Length == length)) {
                return string.Format(OutputMessages.RouteExisting, startPoint, endPoint, length);
            }
            if (routes.Any(x => x.StartPoint == startPoint && x.EndPoint == endPoint && x.Length < length)) {
                return string.Format(OutputMessages.RouteIsTooLong, startPoint, endPoint);
            }
            int routeNum = routes.Count + 1;
            IRoute newRoute = new Route(startPoint, endPoint, length, routeNum);
            routes.Add(newRoute);
            var mathingRoute = routes.FirstOrDefault(x => x.StartPoint == newRoute.StartPoint && x.EndPoint == newRoute.EndPoint && x.Length > newRoute.Length);
            if (mathingRoute != null) { 
                mathingRoute.LockRoute();
            }
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
            var userFound = users.FirstOrDefault(x => x.DrivingLicenseNumber == drivingLicenseNumber);
            if (userFound != null){
                return string.Format(OutputMessages.UserWithSameLicenseAlreadyAdded, drivingLicenseNumber);
            }
            users.Add(new User(firstName, lastName, drivingLicenseNumber));
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
            IVehicle newVehicle = null;
             
            if (!vehicleTypeList.Contains(vehicleType)) {
                return string.Format(OutputMessages.VehicleTypeNotAccessible, vehicleType);
            }
            if (vehicles.Any(x => x.LicensePlateNumber == licensePlateNumber)) {
                return string.Format(OutputMessages.LicensePlateExists, licensePlateNumber);
            }
            if (vehicleType == "PassengerCar") {
                newVehicle = new PassengerCar(brand, model, licensePlateNumber);
            }
            if (vehicleType == "CargoVan"){
                newVehicle = new CargoVan(brand, model, licensePlateNumber);
            }
            vehicles.Add(newVehicle);
            return string.Format(OutputMessages.VehicleAddedSuccessfully, brand, model, licensePlateNumber);
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
