using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public class DrinkItem : VendableItems
    {
        public DrinkItem(string productCode, string name, decimal cost) : base(productCode, name, cost)
        {
        }

        public override string ConsumeMessage { get { return "Glug Glug, Yum!"; } }
    }
}
