using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Capstone.Classes
{
    public class VendingMachine
    {
        public Dictionary<string, VendableItems> Inventory { get; } = VendingInput.RestockFromInputFile("vendingmachine.csv");
        
    }
}
