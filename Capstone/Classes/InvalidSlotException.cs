using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public class InvalidSlotException : VendingMachineException
    {
        public InvalidSlotException() : base("\nThat is not a valid slot!")
        {
        }
    }
}
