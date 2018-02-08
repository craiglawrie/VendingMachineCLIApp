using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public class ChipItem : VendableItems
    {
        public ChipItem(decimal cost, string productCode, string name) : base(cost, productCode, name)
        {
        }

        public override string ConsumeMessage { get { return "Crunch Crunch, Yum!"; } }
    }
}
