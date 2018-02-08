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
				
				int mainMenuInput = Menu(MainMenuOptions);
                int purchaseMenuInput;
				if (mainMenuInput == 1)
				{
					DisplayVendingItems();
				}
				else if (mainMenuInput == 2) 
                {
                    purchaseMenuInput = Menu(PurchaseMenuOptions);
                }

            }

        }

        private void DisplayVendingItems()
        {
			Console.Clear();

			foreach (var kvp in vendingMachine.Inventory)
			{
				Console.WriteLine($"{kvp.Key} {kvp.Value.Name} ${kvp.Value.Cost}");
			}
			Console.WriteLine();
			Console.WriteLine("Press ENTER to resturn to main menu");
			Console.ReadLine();
		}

        private int Menu(string[] menuOptions)
        {
			Console.Clear();

			int userSelection = 0;
            string userInput = "";

            for (int i = 0; i < menuOptions.Length; i++)
            {
                Console.WriteLine($"({i + 1}) {menuOptions[i]}");
            }

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