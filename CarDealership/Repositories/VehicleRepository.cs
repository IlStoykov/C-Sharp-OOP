using CarDealership.Models.Contracts;
using CarDealership.Repositories.Contracts;


namespace CarDealership.Repositories
{
    public class VehicleRepository : IRepository<IVehicle>
    {
        private List<IVehicle> vehicles;

        public VehicleRepository() { 
            vehicles = new List<IVehicle>();
        }
        public IReadOnlyCollection<IVehicle> Models => vehicles.AsReadOnly();

        public void Add(IVehicle model)
        {
            if (!vehicles.Contains(model)) { 
                vehicles.Add(model);
            }
        }
        public bool Exists(string text)
        {
            return vehicles.Any(x => x.Model == text);
        }

        public IVehicle Get(string text)
        {
            return vehicles.FirstOrDefault(x => x.Model == text);
        }

        public bool Remove(string text)
        {
            IVehicle vehicle = vehicles.FirstOrDefault(y => y.Model == text);
            if (vehicle != null) {
                return false;
            }
            vehicles.Remove(vehicle);
            return true;    
        }
    }
}
