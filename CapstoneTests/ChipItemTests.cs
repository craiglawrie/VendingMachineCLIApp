using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;

namespace CapstoneTests
{
    [TestClass]
    public class ChipItemTests
    {
        ChipItem chip;

        [TestInitialize]
        public void Initialize()
        {
            chip = new ChipItem("CHIP_SLOT", "CHIP_NAME", 0.01M);
        }

        [TestMethod]
        [TestCategory("Chip Item")]
        public void DefaultStockQuantityIs5()
        {
            chip.Restock();
            Assert.AreEqual(5, chip.AmountRemaining);
        }

        [TestMethod]
        [TestCategory("Chip Item")]
        public void Removing1ResultsInQty4()
        {
            chip.Restock();
            chip.SellOne();
            Assert.AreEqual(4, chip.AmountRemaining);
        }

        [TestMethod]
        [TestCategory("Chip Item")]
        public void InventoryDoesntGoNegative()
        {
            chip.Restock(); // set to 5
            chip.SellOne(); // 4
            chip.SellOne(); // 3
            chip.SellOne(); // 2
            chip.SellOne(); // 1
            chip.SellOne(); // 0
            Assert.IsFalse(chip.SellOne());
            Assert.AreEqual(0, chip.AmountRemaining);
            chip.SellOne(); // still 0
            Assert.AreEqual(0, chip.AmountRemaining);
        }

        [TestMethod]
        [TestCategory("Chip Item")]
        public void ConsumeMessageIsCorrect()
        {
            chip.SellOne();
            Assert.AreEqual("Crunch Crunch, Yum!", chip.ConsumeMessage);
        }

        [TestMethod]
        [TestCategory("Chip Item")]
        public void ConstuctorPropertiesAreSet()
        {
            Assert.AreEqual("CHIP_NAME", chip.Name);
            Assert.AreEqual("CHIP_SLOT", chip.ProductCode);
            Assert.AreEqual(0.01M, chip.Cost);
        }

    }
}
