using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public class DrinkItem : VendableItems
    {
        public DrinkItem(decimal cost, string productCode, string name) : base(cost, productCode, name)
        {
        }

        public override string ConsumeMessage { get { return "Glug Glug, Yum!"; } }
    }
}
