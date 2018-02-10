using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Capstone.Classes
{
    public class VendingSalesReport
    {
        public static bool WriteMonthlyReport(string destinationFile, string auditLogFile, int month)
        {
            Regex balance = new Regex(@"\$\d*\,?\d+\.\d{2}");
            Regex date = new Regex($"^{month:00}");
            Regex productName = new Regex(@"M\s[\w\s\d\.-]+\w\d");
            Dictionary<string, int> salesLog = new Dictionary<string, int>();

            decimal amountTendered = 0M;
            decimal amountReturned = 0M;
            decimal totalRevenue = 0M;

            bool success = true;

            try
            {
                using (StreamReader sr = new StreamReader(auditLogFile))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        if (date.IsMatch(line) && !line.Contains("FEED MONEY:") && !line.Contains("GIVE CHANGE:"))
                        {
                            string product = productName.Match(line).Value;
                            product = product.Substring(2, product.Length - 5);

                            salesLog[product] = salesLog.ContainsKey(product) ? salesLog[product] + 1 : 1;
                            var transactionMoney = balance.Matches(line);
                            amountTendered = decimal.Parse(transactionMoney[0].Value.Substring(1));
                            amountReturned = decimal.Parse(transactionMoney[1].Value.Substring(1));

                            totalRevenue += amountTendered - amountReturned;
                        }
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine();
                Console.WriteLine("Something went wrong reading the audit log.");
                Console.WriteLine(e.Message);
                success = false;
            }

            try
            {
                using (StreamWriter sw = new StreamWriter(destinationFile, false))
                {
                    foreach (var kvp in salesLog)
                    {
                        sw.WriteLine($"{kvp.Key}|{kvp.Value}");
                    }
                    sw.WriteLine();
                    sw.WriteLine($"**TOTAL SALES** {totalRevenue.ToString("C")}");
                }
            }
            catch (IOException e)
            {
                Console.WriteLine();
                Console.WriteLine("Something went wrong writing the sales report.");
                Console.WriteLine(e.Message);
                success = false;
            }

            return success;
        }
    }
}
