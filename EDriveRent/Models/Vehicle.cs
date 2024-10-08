using EDriveRent.Models.Contracts;
using EDriveRent.Utilities.Messages;
using System;


namespace EDriveRent.Models
{
    public abstract class Vehicle : IVehicle
    {
        private string brand;
        private string model;
        private double maxMilage;
        private string licensePlateNumber;
        private int batteryLevel;
        private bool isDamaged;

        public Vehicle(string brand, string model, double maxMileage, string licensePlateNumber) {
            Brand = brand;
            Model = model;
            this.maxMilage = maxMilage;
            LicensePlateNumber = licensePlateNumber;
            batteryLevel = 100;
            this.isDamaged = false;
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
        public int BatteryLevel
        {
            get => batteryLevel;
            private set
            {
                value = batteryLevel;
            }
        }
        

        public bool IsDamaged { get; private set; }

        public void ChangeStatus()
        {
           IsDamaged = !IsDamaged;
        }
        public void Drive(double mileage)
        {
            int batteryReduction = (int)Math.Round((mileage / MaxMileage) * 100);
            if (GetType().Name == "CargoVan") {
                batteryReduction -= 5;
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
        public override string ToString() => $"{Brand} {Model} License plate: {LicensePlateNumber} Battery" +
            $": {BatteryLevel}% Status: {(IsDamaged ? "damaged":"OK")}";
    }
}
