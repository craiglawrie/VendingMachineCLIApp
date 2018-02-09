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
        private string ResponsiveYumText { get; set; }

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


            Console.Clear();

            if (purchaseMenuInput == 1)
            {
                // Feed money
                int feedAmount;
                do
                {
                    feedAmount = GetInputPositiveInteger("Enter the number of dollars to feed (ENTER when finished): $");
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
                    ResponsiveYumText += item.ConsumeMessage + "\n";
                    Console.WriteLine();
                    Console.WriteLine("Press ENTER to continue");
                    Console.ReadLine();
                }
            }
            else if (purchaseMenuInput == 3)
            {
                Change change = vendingMachine.ReturnChange();

                // Finish transaction
                Console.WriteLine(ResponsiveYumText);
                Console.WriteLine();
                Console.WriteLine("Please remember to take your change:");
                Console.WriteLine($"{change.Quarters} Quarter(s), {change.Dimes} Dime(s), {change.Nickels} Nickel(s)");
                Console.WriteLine();
                Console.WriteLine("Press ENTER to continue");
                Console.ReadLine();

                ResponsiveYumText = "";

            }
        }

        private int GetInputPositiveInteger(string prompt)
        {
            int userInput = 0;
            string inputString = "";

            do
            {
                Console.Write(prompt);
                inputString = Console.ReadLine();
                if (inputString == "") inputString = "0";
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
                if (item.AmountRemaining > 0)
                {
                    Console.WriteLine($"{slot,-4} {item.Name,-19} ${item.Cost}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"{slot,-4} {item.Name,-19} SOLD OUT!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
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