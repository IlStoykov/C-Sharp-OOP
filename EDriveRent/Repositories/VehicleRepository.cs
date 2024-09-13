using EDriveRent.Models.Contracts;
using EDriveRent.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Repositories
{
    public class VehicleRepository : IRepository<IVehicle>
    {
        private List<IVehicle> vehicles;
        public VehicleRepository() { 
            vehicles = new List<IVehicle>();
        }
        public void AddModel(IVehicle model)// check if should duplicate vehicle
        {
            IVehicle vehiclFound = vehicles.FirstOrDefault(x => x.LicensePlateNumber == model.LicensePlateNumber);
            if (vehiclFound == null){
                vehicles.Add(model);
            }
        }
        public IVehicle FindById(string identifier)
        {
            return vehicles.FirstOrDefault(x => x.LicensePlateNumber == identifier);
        }
        public IReadOnlyCollection<IVehicle> GetAll()
        {
            return vehicles.AsReadOnly();
        }
        public bool RemoveById(string identifier)
        {
            return vehicles.Remove(vehicles.Find(x => x.LicensePlateNumber == identifier));
        }
    }
}
