using NuGet.Frameworks;
using NUnit.Framework;
using System;
using System.Reflection;

namespace VehicleGarage.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Constructor_ShouldInitializeCapacityAndVehicleCorrectly() {
            int capacity = 10;
            Garage garage = new Garage(capacity);

            Assert.AreEqual(capacity, garage.Capacity);
            Assert.IsNotNull(garage.Capacity);
            Assert.AreEqual(0, garage.Vehicles.Count);
        }
        [Test]
        public void AddVehicle_ShouldAddObjectWhenGarageIsNotFull() {
            Garage garage = new Garage(2);
            Vehicle newVehicle = new Vehicle("Tesla", "Model S", "ABC123");
            garage.Vehicles.Add(newVehicle);

            Assert.AreEqual(1, garage.Vehicles.Count);
            Assert.AreEqual(newVehicle, garage.Vehicles[0]);
        }
        [Test]
        public void AddVehicle_ShouldReturnFalseWhenGarageIsFull() {
            Garage garage = new Garage(1);
            Vehicle vehicle1 = new Vehicle("Tesla", "Model S", "ABC123");
            Vehicle vehicle2 = new Vehicle("Toyota", "Corolla", "ABC321");
            garage.AddVehicle(vehicle1);

            bool result = garage.AddVehicle(vehicle2);

            Assert.IsFalse(result);
        }
        [Test]
        public void Addvehicle_ShouldReturnFalseWhenVehicleWithSameLicensePlateExist() {
            Garage garage = new Garage(2);
            Vehicle vehicle1 = new Vehicle("Tesla", "Model S", "ABC123");
            Vehicle vehicle2 = new Vehicle("Toyota", "Corolla", "ABC123");
            garage.AddVehicle(vehicle1);

            bool result = garage.AddVehicle(vehicle2);

            Assert.IsFalse(result);
        }
        [Test]
        public void AddVehicle_ShoudReturnTrueWhenLacensePlateIsUniq() {
            Garage garage = new Garage(2);
            Vehicle vehicle1 = new Vehicle("Tesla", "Model S", "ABC123");
            Vehicle vehicle2 = new Vehicle("Toyota", "Corolla", "ABC123FGH");
            garage.AddVehicle(vehicle1);

            bool result = garage.AddVehicle(vehicle2);
            string uniqPlateNumber = "ABC123FGH";

            Assert.IsTrue(result);
            Assert.AreEqual(2, garage.Vehicles.Count);
            Assert.AreEqual(uniqPlateNumber, garage.Vehicles[1].LicensePlateNumber);
        }
        [Test]
        public void ChargeVehicle_ShouldCountChargeBatteryCorrectlly() {
            Garage garage = new Garage(2);
            Vehicle vehicle1 = new Vehicle("Tesla", "Model S", "ABC123");
            Vehicle vehicle2 = new Vehicle("Toyota", "Corolla", "ABC123FGH");

            int testBatteryLevel = 120;
            garage.AddVehicle(vehicle1);
            garage.AddVehicle(vehicle2);
            int expectResult = 2;
            int result = garage.ChargeVehicles(testBatteryLevel);

            Assert.AreEqual(expectResult, result);
        }
        [Test]
        public void ChargedVehicle_SetBatteryLevelCorrectly() {
            int expectedBatteryLevel = 100;

            Garage garage = new Garage(1);
            Vehicle vehicle1 = new Vehicle("Tesla", "Model S", "ABC123");
                        
            int result = vehicle1.BatteryLevel;
            
            Assert.AreEqual(expectedBatteryLevel, result);
        }
        [Test]
        public void DriveVehicle_ShouldNotChangeBatteryLevelIfIsTooHigh() {
            int expectedBatteryLevel = 100;
            Garage garage = new Garage(2);
            Vehicle testVehicle = new Vehicle("Tesla", "Model S", "ABC123");
            garage.AddVehicle(testVehicle);
            garage.DriveVehicle("ABC123", 150, false);

            Assert.AreEqual(expectedBatteryLevel, testVehicle.BatteryLevel);
        }
        [Test]
        public void DriveVehicle_ShouldNotChangeBatteryLevelIfBatteryLavelIsTooLow() {
            int expectedBatteryLevel = 25;
            Garage garage = new Garage(2);
            Vehicle testVehicle = new Vehicle("Tesla", "Model S", "ABC123");

            testVehicle.BatteryLevel = expectedBatteryLevel;
            garage.AddVehicle(testVehicle);
            garage.DriveVehicle("ABC123", 150, false);

            Assert.AreEqual(expectedBatteryLevel, testVehicle.BatteryLevel);
        }
        [Test]
        public void DriveVehicle_ShouldChangeBatteryLevelCorrectly() {
            int expectedBatteryLevel = 50;
            Garage garage = new Garage(2);
            Vehicle testVehicle = new Vehicle("Tesla", "Model S", "ABC123");
            
            garage.AddVehicle(testVehicle);
            garage.DriveVehicle("ABC123", 50, false);

            Assert.AreEqual(expectedBatteryLevel, testVehicle.BatteryLevel);

        }
        [Test] 
        public void DriveVehicle_ShouldChangeIsdamagedlCorrectly() {
            Garage garage = new Garage(2);
            Vehicle testVehicle = new Vehicle("Tesla", "Model S", "ABC123");
            garage.AddVehicle(testVehicle);
            garage.DriveVehicle("ABC123", 50, true);

            Assert.IsTrue(testVehicle.IsDamaged);
        }
    }
}
