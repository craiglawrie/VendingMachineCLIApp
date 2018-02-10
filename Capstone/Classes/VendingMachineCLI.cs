using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Classes;

namespace Capstone.Classes
{
    public class VendingMachineCLI
    {
        /// <summary>
        /// The vending machine managed and displayed by this class.
        /// </summary>
        private VendingMachine VendoMatic500 { get; }
        private string StockingFile { get; }

        /// <summary>
        /// Creates a new vending machine CLI for a vending machine that will be stocked according to the provided
        /// input file.
        /// </summary>
        /// <param name="stockingFile"></param>
        public VendingMachineCLI(string stockingFile)
        {
            this.StockingFile = stockingFile;
            this.VendoMatic500 = new VendingMachine(VendingInput.RestockFromInputFile(StockingFile));
        }

        /// <summary>
        /// A string used for displaying "consume messages" only when the transaction is closed
        /// and change is given.
        /// </summary>
        private string ResponsiveYumText { get; set; }

        /// <summary>
        /// Begins the vending machine processes.
        /// </summary>
        public void Run()
        {
            while (true)
            {
                int mainMenuInput = Menu(MainMenuOptions, null);
                if (mainMenuInput == 1)
                {
                    DisplayVendingItemsGrid();
                    PauseOperation();
                }
                else if (mainMenuInput == 2)
                {
                    ManagePurchaseMenu();
                }
                else if (mainMenuInput == 3)
                {
                    //ManageConfigMenu();
                }
                else if (mainMenuInput == 4)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Acts on the purchase menu selection from the user.
        /// </summary>
        /// <param name="purchaseMenuInput"></param>
        private void ManagePurchaseMenu()
        {
            int purchaseMenuInput;

            while (true)
            {
                string currentBalanceString = $"Current Money Provided: {VendoMatic500.Balance.ToString("C")}";

                purchaseMenuInput = Menu(PurchaseMenuOptions, currentBalanceString);
                Console.Clear();

                if (purchaseMenuInput == 1) // FEED MONEY
                {
                    FeedMoneyUntilCancel();
                }
                else if (purchaseMenuInput == 2) // SELECT PRODUCT
                {
                    string selectedItem = GetUserSelectedProduct();
                    ProcessPurchaseOfSelectedProduct(selectedItem);
                }
                else if (purchaseMenuInput == 3) // FINISH TRANSACTION
                {
                    Change change = VendoMatic500.ReturnChange();
                    WriteChangeMessage(change);
                }
                if (purchaseMenuInput == 3) break;
            }
        }

        /// <summary>
        /// Allows the user to deposit money into the vending machine in whole dollar amounts.
        /// </summary>
        private void FeedMoneyUntilCancel()
        {
            int feedAmount;
            do
            {
                feedAmount = GetInputPositiveInteger("Enter the number of dollars to feed (ENTER when finished): $");

                if (feedAmount != 0)
                {
                    VendoMatic500.FeedMoney(feedAmount);
                    Console.WriteLine($"Deposit successful. Balance: {VendoMatic500.Balance.ToString("C")}");
                    Console.WriteLine();
                }
            } while (feedAmount != 0);
        }

        /// <summary>
        /// Purchases the selected product, if valid.
        /// </summary>
        /// <param name="selection">The user input product code / "slot".</param>
        private void ProcessPurchaseOfSelectedProduct(string selection)
        {
            VendableItems item = VendoMatic500.Purchase(selection);
            if (item != null)
            {
                ResponsiveYumText += item.ConsumeMessage + "\n";
                Console.WriteLine();
                Console.WriteLine($"Dispensing {item.Name}...");
                Console.WriteLine();
                PauseOperation();
            }
        }

        /// <summary>
        /// Pauses until the user presses ENTER.
        /// </summary>
        private static void PauseOperation()
        {
            Console.Write("Press ENTER to continue...");
            Console.ReadLine();
        }

        /// <summary>
        /// Gets a string input from the user, for identifying a product
        /// by its product code / "slot".
        /// </summary>
        /// <returns></returns>
        private string GetUserSelectedProduct()
        {
            DisplayVendingItemsGrid();
            Console.Write("Please enter desired selection: ");
            return Console.ReadLine().ToUpper();
        }

        /// <summary>
        /// Writes a message to the user after completing purchase.
        /// Provides munchy, crunchy, chewie, gulpy yums, resets the
        /// list of those yums, and provides coin distribution of change.
        /// </summary>
        /// <param name="change"></param>
        private void WriteChangeMessage(Change change)
        {
            Console.WriteLine(ResponsiveYumText);
            Console.WriteLine();
            Console.WriteLine("Please remember to take your change:");
            Console.WriteLine($"{change.Quarters} Quarter(s), {change.Dimes} Dime(s), {change.Nickels} Nickel(s)");
            Console.WriteLine();
            PauseOperation();

            ResponsiveYumText = "";
        }

        /// <summary>
        /// Prompts the user for a positive integer. The prompt string must be provided.
        /// There is no upper limit on the integer, except int.MaxValue. This can be
        /// used for feeding dollars or in response to menus.
        /// </summary>
        /// <param name="prompt"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Displays vending machine inventory as a list. Items with no remaining
        /// stock are displayed as "SOLD OUT!"
        /// </summary>
        private void DisplayVendingItemsList()
        {
            Console.Clear();

            string[] slots = VendoMatic500.Slots;
            foreach (string slot in slots)
            {
                VendableItems item = VendoMatic500.GetItemAtSlot(slot);
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
        }


        /// <summary>
        /// Displays vending machine inventory as a list. Items with no remaining
        /// stock are displayed as "SOLD OUT!"
        /// </summary>
        private void DisplayVendingItemsGrid()
        {
            Console.Clear();

            string[] slots = VendoMatic500.Slots;
            Console.WriteLine("----------------------------------------------------------------------------------------");
            foreach (char c in "ABCD".ToCharArray())
            {

                for (int s = 1; s < 5; s++)
                {
                    string address = c.ToString() + s.ToString();

                    VendableItems item = VendoMatic500.GetItemAtSlot(address);

                    if (item.AmountRemaining > 0)
                    {
                        Console.Write($"|   {address,-17}|");
                    }
                    else
                    {
                        Console.Write("|");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write($"   {address,-17}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("|");
                    }
                }
                Console.WriteLine();

                for (int s = 1; s < 5; s++)
                {
                    string address = c.ToString() + s.ToString();

                    VendableItems item = VendoMatic500.GetItemAtSlot(address);

                    if (item.AmountRemaining > 0)
                    {
                        Console.Write($"| {item.Name,-19}|");
                    }
                    else
                    {
                        Console.Write("|");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write($" {item.Name,-19}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("|");
                    }
                }
                Console.WriteLine();

                for (int s = 1; s < 5; s++)
                {
                    string address = c.ToString() + s.ToString();

                    VendableItems item = VendoMatic500.GetItemAtSlot(address);
                    if (item.AmountRemaining > 0)
                    {
                        Console.Write($"|  {item.Cost.ToString("C"),-18}|");
                    }
                    else
                    {
                        Console.Write("|");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write($"    SOLD OUT!       ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("|");
                    }
                }
                Console.WriteLine();
                Console.WriteLine("----------------------------------------------------------------------------------------");
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Provides a menu to the user and prompts for their selection.
        /// </summary>
        /// <param name="menuOptions">A list of options to select from.</param>
        /// <param name="extraString">A message after the menu.</param>
        /// <returns></returns>
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

        /// <summary>
        /// The array of options in the main menu.
        /// </summary>
        private string[] MainMenuOptions
        {
            get
            {
                return new string[]
                {
                    "Display Vending Machine Items",
                    "Purchase",
                    "Config",
                    "Quit"
                };
            }
        }

        /// <summary>
        /// The array of options in the purchase menu.
        /// </summary>
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