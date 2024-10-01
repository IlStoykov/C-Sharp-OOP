namespace SmartDevice.Tests
{
    using NUnit.Framework;    
    using System.Collections.Generic;
    using System.Text;

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
        [Test]
        public void Test_InsttalApp_AddAppNameToApplications()
        {
            Device newTestDevice = new Device(devicMemory);
            int testAppSize = devicMemory / 4;
            string firstAppName = "FisrtApp";
            string secondAppName = "SecondApp";

            newTestDevice.InstallApp(firstAppName, testAppSize);
            Assert.AreEqual(newTestDevice.Applications[0], firstAppName);
            
            newTestDevice.InstallApp(secondAppName, testAppSize);
            Assert.AreEqual(newTestDevice.Applications[1], secondAppName);
        }
        [Test]
        public void Test_FormatDevice_PhtotCountWorkProper() {
            int expectedResult = 0;
            int numOfPictures = 2;

            int firstPhoto = 100;
            int secondPhoto = 100;

            testDevice.TakePhoto(firstPhoto);
            testDevice.TakePhoto(secondPhoto);
            Assert.AreEqual(testDevice.Photos, numOfPictures);

            testDevice.FormatDevice();
            Assert.AreEqual(testDevice.Photos, expectedResult);
        }
        [Test]
        public void Test_FormatDevice_ApplicationListIsEmpty() {           
            string firstAppName = "FisrtApp";
            string secondAppName = "SecondApp";
            int testAppSize = 100;
            int expectedResult = testDevice.Applications.Count;

            testDevice.InstallApp(firstAppName, testAppSize);
            testDevice.InstallApp(secondAppName, testAppSize);

            testDevice.FormatDevice();
            Assert.IsTrue( expectedResult == 0);
        }
        [Test]
        public void Test_FormatDevice_AvailableMemory() {
            int firstPhoto = 100;
            int secondPhoto = 100;
            int firstExpectResult = devicMemory - firstPhoto - secondPhoto;

            testDevice.TakePhoto(firstPhoto);
            testDevice.TakePhoto(secondPhoto);
            Assert.AreEqual(testDevice.AvailableMemory, firstExpectResult);

            testDevice.FormatDevice();
            Assert.AreEqual(testDevice.MemoryCapacity, devicMemory);            
        }
        [Test]
        public void Test_GetDeviceStatus() {
            string firstAppName = "FisrtApp";
            string secondAppName = "SecondApp";
            int testAppSize = 100;
            int testPhotoSize = 75;
            
            
            testDevice.InstallApp(firstAppName, testAppSize);
            testDevice.InstallApp(secondAppName, testAppSize);

            testDevice.TakePhoto(testPhotoSize);

            string testResult = testDevice.GetDeviceStatus();

            StringBuilder expectedResult = new StringBuilder();
            expectedResult.AppendLine($"Memory Capacity: {testDevice.MemoryCapacity} MB, Available Memory: {testDevice.AvailableMemory} MB");
            expectedResult.AppendLine($"Photos Count: {testDevice.Photos}");
            expectedResult.AppendLine($"Applications Installed: {string.Join(", ", testDevice.Applications)}");

            Assert.That(testResult, Is.EqualTo(expectedResult.ToString().Trim()));
        }
    }
}
