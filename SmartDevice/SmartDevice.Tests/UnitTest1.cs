namespace SmartDevice.Tests
{
    using NUnit.Framework;    
    using System.Collections.Generic;
 

    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_DeviceConstructorWorkPropper() 
        {
            Device testDevice = new Device(500);
            int expectResult = 500;
            var propertyValue = testDevice.Applications;
            
            Assert.AreEqual(testDevice.AvailableMemory, expectResult);
            Assert.AreEqual(testDevice.MemoryCapacity, expectResult);
            Assert.IsTrue(testDevice.Photos == 0);
            Assert.That(propertyValue, Is.InstanceOf<List<string>>());
        }
    }
}
