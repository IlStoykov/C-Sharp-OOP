using NuGet.Frameworks;
using NUnit.Framework;


namespace VehicleGarage.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GarageCheckCapacity() {
            Garage garage = new Garage(3);
            Vehicle car = new("Toyota", "Corolla", "CA6306AM");
            Vehicle van = new ("Mercedes-Benz", "Vito", "H7806AH");
            Vehicle truck = new ("Scania", "Citywide", "P7006XX");
            Vehicle scooter = new ("Yamaha", "Aerox", "PB6006PA");

            garage.AddVehicle(car);
            garage.AddVehicle(van);
            garage.AddVehicle(truck);

            var resutl = garage.AddVehicle(scooter);
            Assert.IsFalse(resutl);
        }
        [Test]
        public void CheckAddVehicleIfAlreadyExist() {
            Garage garage = new Garage(3);
            Vehicle car = new("Toyota", "Corolla", "CA6306AM");
            garage.AddVehicle(car);

            var result = garage.AddVehicle(car);
            
            Assert.IsFalse(result);
        }
        [Test]
        public void CheckAddVehicleWorkProperly()
        {
            Garage garage = new Garage(3);
            Vehicle car = new("Toyota", "Corolla", "CA6306AM");

            var result = garage.AddVehicle(car);

            Assert.IsTrue(result);
        }
        [Test]
        public void Check_DriveVehicleWorkProperly() {
            int expectedResult = 56;
            Garage garage = new Garage(3);
            Vehicle car = new("Toyota", "Corolla", "CA6306AM");
            garage.AddVehicle(car);

            garage.DriveVehicle("CA6306AM", 44, false);

            Assert.AreEqual(expectedResult, car.BatteryLevel);
        }
        [Test]
        public void Test_DriveVehiclAccdentOccured() {
            Garage garage = new Garage(3);
            Vehicle car = new("Toyota", "Corolla", "CA6306AM");
            garage.AddVehicle(car);

            garage.DriveVehicle("CA6306AM", 44, true);

            bool result = car.IsDamaged;
            Assert.IsTrue(result);
        }
    }
}
