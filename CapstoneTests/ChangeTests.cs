using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;

namespace CapstoneTests
{
    [TestClass]
    public class ChangeTests
    {
        [TestMethod]
        [TestCategory("Change")]
        public void Change_For1Dollar_Is4Quarters()
        {
            // Arrange
            Change change = new Change(1);

            // Assert
            Assert.AreEqual(4, change.Quarters);
            Assert.AreEqual(0, change.Dimes);
            Assert.AreEqual(0, change.Nickels);
        }

        [TestMethod]
        [TestCategory("Change")]
        public void Change_For1Dollar10_Is4Quarters1Dime()
        {
            // Arrange
            Change change = new Change(1.1M);

            // Assert
            Assert.AreEqual(4, change.Quarters);
            Assert.AreEqual(1, change.Dimes);
            Assert.AreEqual(0, change.Nickels);
        }

        [TestMethod]
        [TestCategory("Change")]
        public void Change_For90Cents_Is3Quarters1Dime1Nickel()
        {
            // Arrange
            Change change = new Change(0.9M);

            // Assert
            Assert.AreEqual(3, change.Quarters);
            Assert.AreEqual(1, change.Dimes);
            Assert.AreEqual(1, change.Nickels);
        }
    }
}
