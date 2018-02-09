using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Capstone.Classes
{
    public class VendingMachine
    {
        public decimal Balance { get; private set; }
        private Dictionary<string, VendableItems> Inventory { get; } = VendingInput.RestockFromInputFile("vendingmachine.csv");
		private TransactionLog Log { get; } = new TransactionLog();
        public string[] Slots
        {
            get
            {
                return Inventory.Keys.ToArray();
            }
        }
        
        public void FeedMoney(int dollars)
        {
            Balance += dollars;
			Log.RecordDeposit(dollars, Balance);
        }

        public VendableItems GetItemAtSlot(string slot)
        {
            return Inventory.ContainsKey(slot) ? Inventory[slot] : null;
        }

        public VendableItems Purchase(string slot)
        {
            VendableItems item = null;

            try
            {
                if (!Inventory.ContainsKey(slot)) throw new InvalidSlotException();
                if (Inventory[slot].AmountRemaining == 0) throw new OutOfStockException();
                if (Balance < Inventory[slot].Cost) throw new InsufficientFundsException();

                item = Inventory[slot];
                item.SellOne();
                Balance -= item.Cost;
				Log.RecordPurchase(slot, item.Name, Balance + item.Cost, Balance);
            }
            catch(VendingMachineException e)
            {
                Console.WriteLine(e.Message);
				Console.WriteLine();
				Console.WriteLine("Press ENTER to continue");
				Console.ReadLine();
            }

            return item;
        }

		public Change ReturnChange()
		{
			Change change = new Change(Balance);
			Log.RecordCompleteTransaction(Balance);
			Balance = 0;
			return change;
		}
	}
}
