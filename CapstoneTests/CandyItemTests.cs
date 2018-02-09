using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;

namespace CapstoneTests
{
    [TestClass]
    public class CandyItemTests
    {
        CandyItem candy;

        [TestInitialize]
        public void Initialize()
        {
            candy = new CandyItem("CANDY_SLOT", "CANDY_NAME", 0.01M);
        }

        [TestMethod]
        [TestCategory("Candy Item")]
        public void DefaultStockQuantityIs5()
        {
            candy.Restock();
            Assert.AreEqual(5, candy.AmountRemaining);
        }

        [TestMethod]
        [TestCategory("Candy Item")]
        public void Removing1ResultsInQty4()
        {
            candy.Restock();
            candy.SellOne();
            Assert.AreEqual(4, candy.AmountRemaining);
        }

        [TestMethod]
        [TestCategory("Candy Item")]
        public void InventoryDoesntGoNegative()
        {
            candy.Restock(); // set to 5
            candy.SellOne(); // 4
            candy.SellOne(); // 3
            candy.SellOne(); // 2
            candy.SellOne(); // 1
            candy.SellOne(); // 0
            Assert.IsFalse(candy.SellOne());
            Assert.AreEqual(0, candy.AmountRemaining);
            candy.SellOne(); // still 0
            Assert.AreEqual(0, candy.AmountRemaining);
        }

        [TestMethod]
        [TestCategory("Candy Item")]
        public void ConsumeMessageIsCorrect()
        {
            candy.SellOne();
            Assert.AreEqual("Munch Munch, Yum!", candy.ConsumeMessage);
        }

        [TestMethod]
        [TestCategory("Candy Item")]
        public void ConstuctorPropertiesAreSet()
        {
            Assert.AreEqual("CANDY_NAME", candy.Name);
            Assert.AreEqual("CANDY_SLOT", candy.ProductCode);
            Assert.AreEqual(0.01M, candy.Cost);
        }

    }
}
