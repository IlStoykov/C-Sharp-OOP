using EDriveRent.Models.Contracts;
using EDriveRent.Utilities.Messages;
using System;


namespace EDriveRent.Models
{
    public abstract class Vehicle : IVehicle
    {
        private string brand;
        private string model;
        private string licensePlateNumber;

        public Vehicle(string brand, string model, string licensePlateNumber, double maxMileage) { 
            Brand = brand;
            Model = model;
            LicensePlateNumber = licensePlateNumber;
            MaxMileage = maxMileage;
            IsDamaged = false;
            BatteryLevel = 100;
        }    
        

        public string Brand {
            get => brand;
            private set {
                if (string.IsNullOrWhiteSpace(value)) {
                    throw new ArgumentNullException(ExceptionMessages.BrandNull);
                }
                brand = value;
            }
        }
        public string Model {
            get => model;
            private set {
                if (string.IsNullOrWhiteSpace(value)) {
                    throw new ArgumentException(ExceptionMessages.ModelNull);
                }
                model = value;
            }
        }

        public double MaxMileage { get; private set; }

        public string LicensePlateNumber { 
            get => licensePlateNumber;
            private set{
                if (string.IsNullOrWhiteSpace(value)) {
                    throw new ArgumentException(ExceptionMessages.LicenceNumberRequired);
                }
                licensePlateNumber = value;
            }
        }
        public int BatteryLevel { get; private set; }

        public bool IsDamaged { get; private set; }

        public void ChangeStatus()
        {
           IsDamaged = !IsDamaged;
        }
        public void Drive(double mileage)
        {
            int batteryReduction = (int)Math.Round((mileage / MaxMileage) * 100);
            if (GetType().Name == "CargoVan") {
                batteryReduction += 5;
            }

            BatteryLevel -= batteryReduction;

            if (BatteryLevel < 0) {
                BatteryLevel = 0;
            }
        }
        public void Recharge()
        {
            BatteryLevel = 100;
        }
        public override string ToString() => $"{Brand} {Model} License plate: {LicensePlateNumber} Battery: {BatteryLevel}% Status: {(IsDamaged ? "damaged":"OK")}";
    }
}
