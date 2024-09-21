namespace EDriveRent.Models
{
    public class PassengerCar : Vehicle
    {
        private const double ConstMileage = 450; 

        public PassengerCar(string brand, string model, string licensePlateNumber)
            : base(brand, model, ConstMileage, licensePlateNumber) 
        {
        }
    }
}
