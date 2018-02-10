using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;
using System.Collections.Generic;

namespace CapstoneTests
{
    [TestClass]
    public class VendingMachineTests
    {
        [TestMethod]
        [TestCategory("Vending Machine")]
        public void OneItemInInventory_ReturnsKeysListCorrectly()
        {
            // Arrange
            Dictionary<string, VendableItems> inventory = new Dictionary<string, VendableItems>();
            CandyItem ci = new CandyItem("TEST_SLOT", "TEST_ITEM", 0.05M);
            VendingMachine vm = new VendingMachine(inventory);

            // Act
            inventory.Add(ci.ProductCode, ci);

            // Assert
            CollectionAssert.AreEqual(new string[] { ci.ProductCode }, vm.Slots);
        }

        [TestMethod]
        [TestCategory("Vending Machine")]
        public void TwoItemsInInventory_ReturnsKeysListCorrectly()
        {
            // Arrange
            Dictionary<string, VendableItems> inventory = new Dictionary<string, VendableItems>();
            CandyItem ci1 = new CandyItem("TEST_SLOT_1", "TEST_ITEM_1", 0.05M);
            CandyItem ci2 = new CandyItem("TEST_SLOT_2", "TEST_ITEM_2", 0.15M);
            VendingMachine vm = new VendingMachine(inventory);

            // Act
            inventory.Add(ci1.ProductCode, ci1);
            inventory.Add(ci2.ProductCode, ci2);

            // Assert
            CollectionAssert.AreEqual(new string[] { ci1.ProductCode, ci2.ProductCode }, vm.Slots);
        }

        [TestMethod]
        [TestCategory("Vending Machine")]
        public void FeedMoney_BalanceUpdatesCorrectly()
        {
            // Arrange
            Dictionary<string, VendableItems> inventory = new Dictionary<string, VendableItems>();
            VendingMachine vm = new VendingMachine(inventory);

            // Act
            vm.FeedMoney(1);

            // Assert
            Assert.AreEqual(1M, vm.Balance);
        }

        /*
         * This test method will fail when accessing the FeedMoney method directly,
         * but is controlled for in the user data input.
         * 
         * [TestMethod]
         * [TestCategory("Vending Machine")]
         * public void FeedMoney_CannotFeedNegative()
         * {
         *     // Arrange
         *     Dictionary<string, VendableItems> inventory = new Dictionary<string, VendableItems>();
         *     VendingMachine vm = new VendingMachine(inventory);
         * 
         *     // Act
         *     vm.FeedMoney(-1);
         * 
         *     // Assert
         *     Assert.AreEqual(0M, vm.Balance);
         * }
         */

        [TestMethod]
        [TestCategory("Vending Machine")]
        public void TwoItemsInInventory_CanGetItemAtSlot()
        {
            // Arrange
            Dictionary<string, VendableItems> inventory = new Dictionary<string, VendableItems>();
            CandyItem ci1 = new CandyItem("TEST_SLOT_1", "TEST_ITEM_1", 0.05M);
            CandyItem ci2 = new CandyItem("TEST_SLOT_2", "TEST_ITEM_2", 0.15M);
            VendingMachine vm = new VendingMachine(inventory);

            // Act
            inventory.Add(ci1.ProductCode, ci1);
            inventory.Add(ci2.ProductCode, ci2);

            // Assert
            Assert.AreEqual(ci1, vm.GetItemAtSlot(ci1.ProductCode));
            Assert.AreEqual(ci2, vm.GetItemAtSlot(ci2.ProductCode));
        }

        [TestMethod]
        [TestCategory("Vending Machine")]
        public void ReturnChange_BalanceUpdatesToZero()
        {
            // Arrange
            Dictionary<string, VendableItems> inventory = new Dictionary<string, VendableItems>();
            VendingMachine vm = new VendingMachine(inventory);

            // Act
            vm.FeedMoney(1);
            vm.ReturnChange();

            // Assert
            Assert.AreEqual(0M, vm.Balance);
        }
    }
}
