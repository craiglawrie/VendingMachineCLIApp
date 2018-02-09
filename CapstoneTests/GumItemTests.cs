using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;

namespace CapstoneTests
{
    [TestClass]
    public class GumItemTests
    {
        GumItem gum;

        [TestInitialize]
        public void Initialize()
        {
            gum = new GumItem("GUM_SLOT", "GUM_NAME", 0.01M);
        }

        [TestMethod]
        [TestCategory("Gum Item")]
        public void DefaultStockQuantityIs5()
        {
            gum.Restock();
            Assert.AreEqual(5, gum.AmountRemaining);
        }

        [TestMethod]
        [TestCategory("Gum Item")]
        public void Removing1ResultsInQty4()
        {
            gum.Restock();
            gum.SellOne();
            Assert.AreEqual(4, gum.AmountRemaining);
        }

        [TestMethod]
        [TestCategory("Gum Item")]
        public void InventoryDoesntGoNegative()
        {
            gum.Restock(); // set to 5
            gum.SellOne(); // 4
            gum.SellOne(); // 3
            gum.SellOne(); // 2
            gum.SellOne(); // 1
            gum.SellOne(); // 0
            Assert.IsFalse(gum.SellOne());
            Assert.AreEqual(0, gum.AmountRemaining);
            gum.SellOne(); // still 0
            Assert.AreEqual(0, gum.AmountRemaining);
        }

        [TestMethod]
        [TestCategory("Gum Item")]
        public void ConsumeMessageIsCorrect()
        {
            gum.SellOne();
            Assert.AreEqual("Chew Chew, Yum!", gum.ConsumeMessage);
        }

        [TestMethod]
        [TestCategory("Gum Item")]
        public void ConstuctorPropertiesAreSet()
        {
            Assert.AreEqual("GUM_NAME", gum.Name);
            Assert.AreEqual("GUM_SLOT", gum.ProductCode);
            Assert.AreEqual(0.01M, gum.Cost);
        }

    }
}
