using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public class InsufficientFundsException : VendingMachineException
    {
        public InsufficientFundsException() : base("\nInsufficient funds. Please add more funds.")
        {
        }
    }
}
