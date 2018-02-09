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
                Inventory[slot].SellOne();
                Balance -= Inventory[slot].Cost;
              //  Console.WriteLine(item.ConsumeMessage);//possibly remove
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
			Balance = 0;
			return change;
		}
	}
}
