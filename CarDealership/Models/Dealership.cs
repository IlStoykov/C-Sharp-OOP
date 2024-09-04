using CarDealership.Models.Contracts;
using CarDealership.Repositories;
using CarDealership.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Models
{
    public class Dealership : IDealership
    {
        public Dealership() {
            Vehicles = new VehicleRepository();
            Customers = new CustomerRepository();
        }
        public IRepository<IVehicle> Vehicles {get; private set; }

        public IRepository<ICustomer> Customers { get; private set; }
    }
}
