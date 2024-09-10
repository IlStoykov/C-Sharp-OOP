using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NUnit.Framework;

namespace AutoTrade.Tests
{
    [TestFixture]
    public class DealerShopTests
    {

        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void ConstructorShouldworkCorrectly()
        {
            int capacity = 10;
            DealerShop dealerShop = new DealerShop(capacity);
            Assert.AreEqual(capacity, dealerShop.Capacity);
            Assert.AreEqual(0, dealerShop.Vehicles.Count);
        }
        [Test]
        public void ConstructorThrowExceptionWhenCapacityIsLessThanOne()
        {
            Assert.Throws<ArgumentException>(() => new DealerShop(0), "Capacity must be a positive value.");
        }
        [Test]
        public void AddVehicle_ShouldAddVehicleCorrectly()
        {
            DealerShop dealerShop = new DealerShop(3);
            Vehicle vehicle = new Vehicle("BMW", "3", 2021);
            string result = dealerShop.AddVehicle(vehicle);
            Assert.AreEqual(1, dealerShop.Vehicles.Count);
            Assert.AreEqual("Added 2021 BMW 3", result);
        }

        [Test]
        public void AddVehicleThrowException_WhenCapacityIsFull()
        {
            DealerShop dealerShop = new DealerShop(2);
            dealerShop.AddVehicle(new Vehicle("BMW", "3", 2021));
            dealerShop.AddVehicle(new Vehicle("WV", "Passat", 2022));
            Assert.Throws<InvalidOperationException>(() => dealerShop.AddVehicle(new Vehicle("Seat", "Ibisa", 2021)), "Inventory is full");
        }
        [Test]
        public void SellVehicleTru_FalseWhenVehicleExists()
        {
            DealerShop dealerShop = new DealerShop(2);
            Vehicle vehicle = new Vehicle("BMW", "3", 2021);
            dealerShop.AddVehicle(vehicle);
            bool result = dealerShop.SellVehicle(vehicle);
            Assert.IsTrue(result);
            Assert.AreEqual(0, dealerShop.Vehicles.Count);
        }
        [Test]
        public void SellVehicle_ShouldReturnFalse_WhenVehicleDoesNotExist()
        {
            DealerShop dealerShop = new DealerShop(2);
            Vehicle vehicle1 = new Vehicle("BMW", "3", 2021);
            Vehicle vehicle2 = new Vehicle("WV", "Passat", 2022);
            dealerShop.AddVehicle(vehicle1);
            bool result = dealerShop.SellVehicle(vehicle2);
            Assert.IsFalse(result);
            Assert.AreEqual(1, dealerShop.Vehicles.Count);
        }
        [Test]
        public void ReportReturnCorrectReport()
        {
            DealerShop dealerShop = new DealerShop(2);
            Vehicle vehicle1 = new Vehicle("BMW", "3", 2021);
            Vehicle vehicle2 = new Vehicle("WV", "Passat", 2022);
            dealerShop.AddVehicle(vehicle1);
            dealerShop.AddVehicle(vehicle2);

            StringBuilder result = new StringBuilder();
            result.AppendLine("Inventory Report");
            result.AppendLine("Capacity: 2");
            result.AppendLine("Vehicles: 2");
            result.AppendLine("2021 BMW 3");
            result.AppendLine("2022 WV Passat");


            string actualReport = dealerShop.InventoryReport();
            Assert.AreEqual(result.ToString(), actualReport.ToString());
        }
    }
}
