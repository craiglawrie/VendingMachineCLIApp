using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public class ChipItem : VendableItems
    {
        public ChipItem(string productCode, string name, decimal cost) : base(productCode, name, cost)
        {
        }

        public override string ConsumeMessage { get { return "Crunch Crunch, Yum!"; } }
    }
}
