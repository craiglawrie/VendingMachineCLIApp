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

            VendingIO vio = new VendingIO();
            Dictionary<string, VendableItems> Inventory = vio.ReadInput("vendingmachine.csv");


            Console.WriteLine("hello!");
        }

    }
}

