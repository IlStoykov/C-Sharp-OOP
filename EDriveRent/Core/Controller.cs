using EDriveRent.Core.Contracts;
using EDriveRent.Models;
using EDriveRent.Models.Contracts;
using EDriveRent.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;


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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
