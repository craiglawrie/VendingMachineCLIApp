using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;

namespace CapstoneTests
{
    [TestClass]
    public class ExceptionTests
    {
        [TestMethod]
        [TestCategory("Exceptions")]
        public void OutOfStockExMessageIsCorrect()
        {
            OutOfStockException oos = new OutOfStockException();
            Assert.AreEqual("\nThis item is out of stock!",oos.Message);
        }

        [TestMethod]
        [TestCategory("Exceptions")]
        public void InvalidSlotExMessageIsCorrect()
        {
            InvalidSlotException ivs = new InvalidSlotException();
            Assert.AreEqual("\nThat is not a valid slot!", ivs.Message);
        }

        [TestMethod]
        [TestCategory("Exceptions")]
        public void InsufficientFundsExMessageIsCorrect()
        {
            InsufficientFundsException isf = new InsufficientFundsException();
            Assert.AreEqual("\nInsufficient funds. Please add more funds.", isf.Message);
        }
    }
}
