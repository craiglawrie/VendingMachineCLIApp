using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Capstone.Classes
{
	public class TransactionLog
	{
		private string FilePath = "transactionLog.txt";
		string CurrentDate { get { return DateTime.Now.ToString("MM/dd/yyy hh:mm:ss tt"); } }

		public void RecordDeposit(decimal amount, decimal finalBalance)
		{
			try
			{
				using (StreamWriter sw = new StreamWriter(FilePath, true))
				{
					string actionStamp = CurrentDate + " FEED MONEY:";
					sw.WriteLine($"{actionStamp,-36} {amount.ToString("C"),-10} {finalBalance.ToString("C")}");
				}
			}
			catch (IOException e)
			{
				Console.WriteLine("Error occured");
				Console.WriteLine(e.Message);
			}
		}

		public void RecordPurchase(string slot, string product, decimal initialBalance, decimal finalBalance)
		{
			try
			{
				using (StreamWriter sw = new StreamWriter(FilePath, true))
				{
					string actionStamp = CurrentDate + " " + product.Substring(0,8) + " " + slot;
					sw.WriteLine($"{actionStamp,-36} {initialBalance.ToString("C"),-10} {finalBalance.ToString("C")}");
				}
			}
			catch (IOException e)
			{
				Console.WriteLine("Error occured");
				Console.WriteLine(e.Message);
			}
		}

		public void RecordCompleteTransaction(decimal remainingBalance)
		{
			try
			{
				using (StreamWriter sw = new StreamWriter(FilePath, true))
				{
					string actionStamp = CurrentDate + " GIVE CHANGE: ";
					decimal finalBalance = 0M;
					sw.WriteLine($"{actionStamp,-36} {remainingBalance.ToString("C"),-10} {finalBalance.ToString("C")}");
				}
			}
			catch (IOException e)
			{
				Console.WriteLine("Error occured");
				Console.WriteLine(e.Message);
			}
		}
	}
}
