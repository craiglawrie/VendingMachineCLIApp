using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public class OutOfStockException : VendingMachineException
    {
        public OutOfStockException() : base("\nThis item is out of stock!")
        {
        }
    }
}
