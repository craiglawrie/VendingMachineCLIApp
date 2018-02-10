using Capstone.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            VendingMachineCLI cli = new VendingMachineCLI();
            cli.Run();
            
            VendingSalesReport.WriteMonthlyReport("FebSalesReport.txt", "transactionLog.txt", 2);
        }
    }
}

