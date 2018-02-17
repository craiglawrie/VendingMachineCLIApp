using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Capstone.Classes
{
    public class VendingMachine
    {
        /// <summary>
        /// Creates a new vending machine object stocked according to the input dictionary.
        /// </summary>
        /// <param name="inventory"></param>
        public VendingMachine(Dictionary<string, VendableItems> inventory)
        {
            this.Inventory = inventory;
            this.Log = new TransactionLog();
        }

        /// <summary>
        /// The current balance of deposited money. Resets to zero upon giving change (completion of the purchase menu).
        /// Increase by using FeedMoney(int dollars).
        /// </summary>
        public decimal Balance { get; private set; }

        /// <summary>
        /// Populates the inventory from the input file.
        /// </summary>
        private Dictionary<string, VendableItems> Inventory { get; }

        /// <summary>
        /// Audit transaction log handle.
        /// </summary>
		private TransactionLog Log { get; }

        /// <summary>
        /// The list of vending machine slots which have been stocked. Includes "sold out" items.
        /// </summary>
        public string[] Slots
        {
            get
            {
                return Inventory.Keys.ToArray();
            }
        }

        /// <summary>
        /// Allows the user to increase the balance by depositing an integer number of dollars into the machine.
        /// </summary>
        /// <param name="dollars"></param>
        public void FeedMoney(int dollars)
        {
            Balance += dollars;
            Log.RecordDeposit(dollars, Balance);
        }

        /// <summary>
        /// Returns the item with a particular item code, or "slot" in the vending machine.
        /// </summary>
        /// <param name="slot"></param>
        /// <returns></returns>
        public VendableItems GetItemAtSlot(string slot)
        {
            return Inventory.ContainsKey(slot) ? Inventory[slot] : null;
        }

        /// <summary>
        /// If the selection is valid, in stock, and costs less than the current balance,
        /// reduce the inventory count by 1 and reduce the balance by the cost.
        /// </summary>
        /// <param name="slot">The item code, or "slot" in the vending machine.</param>
        /// <returns></returns>
        public VendableItems Purchase(string slot)
        {
            VendableItems item = null;

            if (!Inventory.ContainsKey(slot))
            {
                throw new InvalidSlotException();
            }

            if (Inventory[slot].AmountRemaining == 0)
            {
                throw new OutOfStockException();
            }

            if (Balance < Inventory[slot].Cost)
            {
                throw new InsufficientFundsException();
            }

            item = Inventory[slot];
            item.SellOne();
            Balance -= item.Cost;
            Log.RecordPurchase(slot, item.Name, Balance + item.Cost, Balance);

            return item;
        }

        /// <summary>
        /// Calculates the change in quarters, dimes and nickels for the current balance.
        /// Sets the balance to zero.
        /// </summary>
        /// <returns></returns>
		public Change ReturnChange()
        {
            Change change = new Change(Balance);
            Log.RecordCompleteTransaction(Balance);

            Balance = 0;

            return change;
        }
    }
}
