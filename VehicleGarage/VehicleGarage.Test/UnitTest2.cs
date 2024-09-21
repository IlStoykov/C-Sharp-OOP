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
        [Test]
        public void Test_DriveVehicleIfAlreadydamaged() {
            int expectResult = 60;
            Garage garage = new Garage(3);
            Vehicle car = new("Toyota", "Corolla", "CA6306AM");
            garage.AddVehicle(car);

            garage.DriveVehicle("CA6306AM", 40, true);
            garage.DriveVehicle("CA6306AM", 40, true);

            Assert.AreEqual(expectResult, car.BatteryLevel);

        }
        [Test]
        public void Test_DriveVehicle_SetBatteryLevelCorrectly() {
            int expectResult = 100;
            Garage garage = new Garage(3);
            Vehicle car = new("Toyota", "Corolla", "CA6306AM");
            garage.AddVehicle(car);

            garage.DriveVehicle("CA6306AM", 150, false);

            Assert.AreEqual(expectResult, car.BatteryLevel);
        }
        [Test]
        public void Test_DriveVehicle_ChargedBatteryCorrectly() {
            int expectResult = 30;
            Garage garage = new Garage(3);
            Vehicle car = new("Toyota", "Corolla", "CA6306AM");
            garage.AddVehicle(car);

            garage.DriveVehicle("CA6306AM", 70, false);
            garage.DriveVehicle("CA6306AM", 50, false);

            Assert.AreEqual(expectResult, car.BatteryLevel);
        }
        [Test]
        public void Test_ChargeVehicle() {
            int expectResult = 2;
            Garage garage = new Garage(3);
            Vehicle car = new("Toyota", "Corolla", "CA6306AM");
            Vehicle bus = new("Mercedes", "Vito", "CA8308AM");
            Vehicle truck = new("FIAT", "TIR", "CA1111AM");

            garage.AddVehicle(car);
            garage.AddVehicle(bus);
            garage.AddVehicle(truck);

            garage.DriveVehicle("CA6306AM", 70, false);
            garage.DriveVehicle("CA8308AM", 70, false);
            garage.DriveVehicle("CA1111AM", 10, false);

            int result = garage.ChargeVehicles(40);

            Assert.AreEqual(expectResult, result);
        }
        [Test]
        public void Test_GarageRepairVehicle() {
            int expectResult = 2;
            Garage garage = new Garage(3);
            Vehicle car = new("Toyota", "Corolla", "CA6306AM");
            Vehicle bus = new("Mercedes", "Vito", "CA8308AM");
            Vehicle truck = new("FIAT", "TIR", "CA1111AM");
            garage.AddVehicle(car);
            garage.AddVehicle(bus);
            garage.AddVehicle(truck);

            garage.DriveVehicle("CA6306AM", 46, false);
            garage.DriveVehicle("CA8308AM", 46, true);
            garage.DriveVehicle("CA1111AM", 46, true);

            string expectedResult = "Vehicles repaired: 2";
            string result = garage.RepairVehicles();

            Assert.AreEqual(expectedResult, result);
        }
    }
}
