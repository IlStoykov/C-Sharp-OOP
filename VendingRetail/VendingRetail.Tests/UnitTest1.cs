using NUnit.Framework;

namespace VendingRetail.Tests
{
    public class Tests
    {        
        CoffeeMat TestCoffeeMat = null;
        int testWaterCapacity = 500;
        int testButtonsCount = 5;
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void Test_SetCoffeeMatConstructorProper() {
            

            TestCoffeeMat = new CoffeeMat(testWaterCapacity, testButtonsCount);

            Assert.AreEqual(TestCoffeeMat.WaterCapacity, testWaterCapacity);
            Assert.AreEqual(TestCoffeeMat.ButtonsCount, testButtonsCount);
            Assert.AreEqual(TestCoffeeMat.Income, 0);           
        }
        [Test]
        public void Test_FillWaterTankIfFull() {
            string expectedResult = "Water tank is already full!";
            string expectedResultAddedAmountOfWater = $"Water tank is filled with {testWaterCapacity}ml";

            TestCoffeeMat = new CoffeeMat(testWaterCapacity, testButtonsCount);

            string tankAddWaterMessage = TestCoffeeMat.FillWaterTank();
            Assert.AreEqual(tankAddWaterMessage, expectedResultAddedAmountOfWater);

            TestCoffeeMat.FillWaterTank();
            string tankFullMessage = TestCoffeeMat.FillWaterTank();
            Assert.AreEqual(tankFullMessage, expectedResult);       

        }
    }
}
