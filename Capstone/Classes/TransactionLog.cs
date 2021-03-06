﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Capstone.Classes
{
	public class TransactionLog
	{
        /// <summary>
        /// Audit log output file location.
        /// </summary>
		private string FilePath = "transactionLog.txt";

        /// <summary>
        /// The current date formatted for the audit log.
        /// </summary>
		string CurrentDate { get { return DateTime.Now.ToString("MM/dd/yyy hh:mm:ss tt"); } }

        /// <summary>
        /// Logs a money feed into the audit log.
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="finalBalance"></param>
		public void RecordDeposit(decimal amount, decimal finalBalance)
		{
            string actionStamp = CurrentDate + " FEED MONEY:";
            WriteToFile($"{actionStamp,-50} {amount.ToString("C"),-10} {finalBalance.ToString("C")}");           
		}

        /// <summary>
        /// Logs a purchase into the audit log.
        /// </summary>
        /// <param name="slot"></param>
        /// <param name="product"></param>
        /// <param name="initialBalance"></param>
        /// <param name="finalBalance"></param>
		public void RecordPurchase(string slot, string product, decimal initialBalance, decimal finalBalance)
		{
            string actionStamp = CurrentDate + " " + product + " " + slot;
            WriteToFile($"{actionStamp,-50} {initialBalance.ToString("C"),-10} {finalBalance.ToString("C")}");            
		}

        /// <summary>
        /// Logs change given into the audit log.
        /// </summary>
        /// <param name="remainingBalance"></param>
		public void RecordCompleteTransaction(decimal remainingBalance)
		{
            string actionStamp = CurrentDate + " GIVE CHANGE: ";
            decimal finalBalance = 0M;
            WriteToFile($"{actionStamp,-50} {remainingBalance.ToString("C"),-10} {finalBalance.ToString("C")}");            
		}


        private void WriteToFile(string message)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(FilePath, true))
                {
                    sw.WriteLine(message);
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
