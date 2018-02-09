using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Classes;

namespace Capstone.Classes
{
    internal class VendingMachineCLI
    {
        private VendingMachine vendingMachine = new VendingMachine();

        public void Run()
        {

            while (true)
            {

                int mainMenuInput = Menu(MainMenuOptions, null);
                int purchaseMenuInput;
                if (mainMenuInput == 1)
                {
                    DisplayVendingItems();
                }
                else if (mainMenuInput == 2)
                {
                    while (true)
                    {
                        string currentBalanceString = $"Current Money Provided: {vendingMachine.Balance.ToString("C")}";
                        purchaseMenuInput = Menu(PurchaseMenuOptions, currentBalanceString);
                        ManagePurchaseMenu(purchaseMenuInput);
                        if (purchaseMenuInput == 3) break;
                    }
                }

            }

        }

        private void ManagePurchaseMenu(int purchaseMenuInput)
        {
			string allYumText = "";
			Console.Clear();
            if (purchaseMenuInput == 1)
            {
                // Feed money
                int feedAmount;
                do
                {
                    feedAmount = GetInputInteger("Enter the number of dollars to feed (0 to finish): $");
                    vendingMachine.FeedMoney(feedAmount);
                    if (feedAmount != 0)
                    {
                        Console.WriteLine($"Deposit successful. Balance: {vendingMachine.Balance.ToString("C")}");
                        Console.WriteLine();
                    }
                } while (feedAmount != 0);
            }
            else if (purchaseMenuInput == 2)
            {
				
				// Select product
				Console.Write("Please enter desired selection: ");
				string selection = Console.ReadLine();
				VendableItems item = vendingMachine.Purchase(selection);

				if (item != null)
				{
					allYumText += item.ConsumeMessage + "\n";
					Console.WriteLine();
					Console.WriteLine("Press ENTER to continue");
					Console.ReadLine();
				}
            }
            else if (purchaseMenuInput == 3)
            {
				Change change = vendingMachine.ReturnChange();

				// Finish transaction
				Console.WriteLine(allYumText);
				Console.WriteLine();
				Console.WriteLine("Press ENTER to continue");
				Console.ReadLine();

			}
        }

        private int GetInputInteger(string prompt)
        {
            int userInput = -1;
            string inputString = "";

            do
            {
                Console.Write(prompt);
                inputString = Console.ReadLine();
            } while (!int.TryParse(inputString, out userInput) || userInput < 0);

            return userInput;
        }

        private void DisplayVendingItems()
        {
            Console.Clear();

            string[] slots = vendingMachine.Slots;
            foreach (string slot in slots)
            {
                VendableItems item = vendingMachine.GetItemAtSlot(slot);
                Console.WriteLine($"{slot} {item.Name} ${item.Cost}");
            }
            Console.WriteLine();
            Console.WriteLine("Press ENTER to return to main menu");
            Console.ReadLine();
        }

        private int Menu(string[] menuOptions, string extraString)
        {
            Console.Clear();

            int userSelection = 0;
            string userInput = "";

            for (int i = 0; i < menuOptions.Length; i++)
            {
                Console.WriteLine($"({i + 1}) {menuOptions[i]}");
            }

            if (!string.IsNullOrEmpty(extraString)) Console.WriteLine(extraString);

            Console.WriteLine();

            do
            {
                Console.Write(">> ");
                userInput = Console.ReadLine();
            } while (!int.TryParse(userInput, out userSelection) || userSelection < 1 || userSelection > menuOptions.Length);

            return userSelection;
        }

        private string[] MainMenuOptions
        {
            get
            {
                return new string[]
                {
                    "Display Vending Machine Items",
                    "Purchase"
                };
            }
        }

        private string[] PurchaseMenuOptions
        {
            get
            {
                return new string[]
                {
                    "Feed Money",
                    "Select Product",
                    "Finish Transation"
                };
            }
        }


    }
}