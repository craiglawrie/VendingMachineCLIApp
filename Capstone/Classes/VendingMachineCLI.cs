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
		private VendingMachine vendingMachine = new VendingMachine();

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
				else if (mainMenuInput == 3)
				{
					break;
				}
			}
		}

		/// <summary>
		/// Acts on the purchase menu selection from the user.
		/// </summary>
		/// <param name="purchaseMenuInput"></param>
		private void ManagePurchaseMenu(int purchaseMenuInput)
		{
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
				Change change = vendingMachine.ReturnChange();
				WriteChangeMessage(change);
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
					vendingMachine.FeedMoney(feedAmount);
					Console.WriteLine($"Deposit successful. Balance: {vendingMachine.Balance.ToString("C")}");
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
			VendableItems item = vendingMachine.Purchase(selection);
			if (item != null)
			{
				ResponsiveYumText += item.ConsumeMessage + "\n";
				Console.WriteLine();
				Console.WriteLine($"Dispensing {item.Name}...");
				Console.WriteLine();
				Console.WriteLine("Press ENTER to continue");
				Console.ReadLine();
			}
		}

		/// <summary>
		/// Gets a string input from the user, for identifying a product
		/// by its product code / "slot".
		/// </summary>
		/// <returns></returns>
		private string GetUserSelectedProduct()
		{
			Console.Write("Please enter desired selection: ");
			return Console.ReadLine();
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
			Console.WriteLine("Press ENTER to continue");
			Console.ReadLine();

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


		/// <summary>
		/// Displays vending machine inventory as a list. Items with no remaining
		/// stock are displayed as "SOLD OUT!"
		/// </summary>
		private void DisplayVendingItems()
		{
			Console.Clear();

			string[] slots = vendingMachine.Slots;
			Console.WriteLine("----------------------------------------------------------------------------------------");
			foreach (char c in "ABCD".ToCharArray())
			{
				//Console.WriteLine();


				for (int s = 1; s < 5; s++)
				{
					string address = c.ToString() + s.ToString();

					VendableItems item = vendingMachine.GetItemAtSlot(address);
					

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

					VendableItems item = vendingMachine.GetItemAtSlot(address);
					
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

					VendableItems item = vendingMachine.GetItemAtSlot(address);
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
			Console.WriteLine("Press ENTER to return to main menu");
			Console.ReadLine();
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