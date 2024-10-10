using NUnit.Framework;

namespace VendingRetail.Tests
{
    public class Tests
    {        
        CoffeeMat TestCoffeeMat = null;
        int testWaterCapacity = 500;
        int testWaterCapacity2 = 120;
        int testButtonsCount = 5;
        int testButtonsCount2 = 2;
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
        [Test]
        public void Test_AddDrinsAddDrinksCountProperly() {
           
            TestCoffeeMat = new CoffeeMat(testWaterCapacity, testButtonsCount2);
            bool resultAddDrink = TestCoffeeMat.AddDrink("Cola", 1.50);

            Assert.IsTrue(resultAddDrink);

            TestCoffeeMat.AddDrink("Soda", 0.50);   
            bool resultCoffeMatIsFull = TestCoffeeMat.AddDrink("Beer", 6.50);

            Assert.IsFalse(resultCoffeMatIsFull);
        }
        [Test]
        public void Test_AddDrinsAddDrinksTYpesProperly() {
            TestCoffeeMat = new CoffeeMat(testWaterCapacity, testButtonsCount2);
            TestCoffeeMat.AddDrink("Soda", 0.50);
            TestCoffeeMat.AddDrink("Soda", 0.50);
            bool resultTypeCheck = TestCoffeeMat.AddDrink("Beer", 6.50);
            
            Assert.IsTrue(resultTypeCheck);
        }
        [Test]
        public void Test_ByDrinkWaterLevel() {
            string expectedResult = "CoffeeMat is out of water!";
            TestCoffeeMat = new CoffeeMat(testWaterCapacity2, testButtonsCount2);
            TestCoffeeMat.AddDrink("Espresso", 1.25);

            TestCoffeeMat.BuyDrink("Espresso");
            string testResult = TestCoffeeMat.BuyDrink("Espresso");

            Assert.AreEqual(testResult, expectedResult);
        }
        [Test]
        public void Test_ByDrinkPayDrinkWorkProper() {
            double expectedSumm = 2.5;
            TestCoffeeMat = new CoffeeMat(testWaterCapacity, testButtonsCount);
            TestCoffeeMat.FillWaterTank();
            TestCoffeeMat.AddDrink("Espresso", 1.25);

            TestCoffeeMat.BuyDrink("Espresso");
            TestCoffeeMat.BuyDrink("Espresso");
            double testResult = TestCoffeeMat.Income;

            Assert.AreEqual(testResult, expectedSumm);            
        }

    }
}
