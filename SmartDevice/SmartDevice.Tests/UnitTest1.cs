namespace SmartDevice.Tests
{
    using NUnit.Framework;    
    using System.Collections.Generic;
 

    public class Tests
    {
        Device testDevice = new Device(500);

        [SetUp]
        public void Setup()
        {           
        }

        [Test]
        public void Test_DeviceConstructorWorkPropper() 
        {
            
            int expectResult = 500;
            var propertyValue = testDevice.Applications;
            
            Assert.AreEqual(testDevice.AvailableMemory, expectResult);
            Assert.AreEqual(testDevice.MemoryCapacity, expectResult);
            Assert.IsTrue(testDevice.Photos == 0);
            Assert.That(propertyValue, Is.InstanceOf<List<string>>());
        }
        [Test]
        public void Test_DeviceTakePhotoMemorySetProper() {
            int testResultGraeterMemory = 1000;
            int testResultLowerMemory = 200;

            Assert.IsFalse(testDevice.TakePhoto(testResultGraeterMemory));
            Assert.IsTrue(testDevice.TakePhoto(testResultLowerMemory));
        }
        [Test]
        public void Test_DeviceTakePhotoCountProper() {
            int expectResult = 1;
            testDevice.TakePhoto(100);

            Assert.AreEqual(expectResult, testDevice.Photos);
        }
        [Test]
        public void Test_DevicetakePhotosMemoryWorkProper() {
            int memoryUsage = 100;
            int expectResult = testDevice.MemoryCapacity - memoryUsage;
            
            testDevice.TakePhoto(memoryUsage);

            Assert.AreEqual(testDevice.AvailableMemory, expectResult);            

        }
    }
}
