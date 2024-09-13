using EDriveRent.Core.Contracts;
using EDriveRent.Models;
using EDriveRent.Models.Contracts;
using EDriveRent.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Core
{
    public class Controller : IController
    {
        private List<IUser> users;
        private List<IVehicle> vehicles;
        private List<IRoute> routes;

        public Controller() {         
            users = new List<IUser>();
            vehicles = new List<IVehicle>();
            routes = new List<IRoute>();
        }
        public string AllowRoute(string startPoint, string endPoint, double length)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public string UsersReport()
        {
            throw new NotImplementedException();
        }
    }
}
