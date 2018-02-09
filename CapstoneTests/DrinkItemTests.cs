using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;

namespace CapstoneTests
{
    [TestClass]
    public class DrinkItemTests
    {
        DrinkItem drink;

        [TestInitialize]
        public void Initialize()
        {
            drink = new DrinkItem("DRINK_SLOT", "DRINK_NAME", 0.01M);
        }

        [TestMethod]
        [TestCategory("Drink Item")]
        public void DefaultStockQuantityIs5()
        {
            drink.Restock();
            Assert.AreEqual(5, drink.AmountRemaining);
        }

        [TestMethod]
        [TestCategory("Drink Item")]
        public void Removing1ResultsInQty4()
        {
            drink.Restock();
            drink.SellOne();
            Assert.AreEqual(4, drink.AmountRemaining);
        }

        [TestMethod]
        [TestCategory("Drink Item")]
        public void InventoryDoesntGoNegative()
        {
            drink.Restock(); // set to 5
            drink.SellOne(); // 4
            drink.SellOne(); // 3
            drink.SellOne(); // 2
            drink.SellOne(); // 1
            drink.SellOne(); // 0
            Assert.IsFalse(drink.SellOne());
            Assert.AreEqual(0, drink.AmountRemaining);
            drink.SellOne(); // still 0
            Assert.AreEqual(0, drink.AmountRemaining);
        }

        [TestMethod]
        [TestCategory("Drink Item")]
        public void ConsumeMessageIsCorrect()
        {
            drink.SellOne();
            Assert.AreEqual("Glug Glug, Yum!", drink.ConsumeMessage);
        }

        [TestMethod]
        [TestCategory("Drink Item")]
        public void ConstuctorPropertiesAreSet()
        {
            Assert.AreEqual("DRINK_NAME", drink.Name);
            Assert.AreEqual("DRINK_SLOT", drink.ProductCode);
            Assert.AreEqual(0.01M, drink.Cost);
        }

    }
}
