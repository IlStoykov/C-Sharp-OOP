namespace SmartDevice.Tests
{
    using NUnit.Framework;    
    using System.Collections.Generic;


    public class Tests
    {
        private static int devicMemory = 500;
        Device testDevice = new Device(devicMemory);


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
            int testResultGraeterMemory = devicMemory * 2;
            int testResultLowerMemory = devicMemory / 2;

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
        [Test]
        public void Test_InstallApp_IfMemoryIsFull() {
            int testAppSize = devicMemory + 1;
            string appName = "testAppName";
            string expectedResult = "Not enough available memory to install the app.";

            var testResult = Assert.Throws<System.InvalidOperationException>(() => testDevice.InstallApp(appName, testAppSize));

            Assert.That(testResult.Message, Is.EqualTo(expectedResult));
        }
        [Test]
        public void Test_InstallApp_DecreaseAvailableMemoryProperly() {
            int testAppSize = devicMemory / 2;
            string appName = "testAppName";
            int expectedResult = devicMemory - testAppSize;

            testDevice.InstallApp(appName, testAppSize);

            Assert.AreEqual(testDevice.AvailableMemory, expectedResult);
        }     

    }
}
