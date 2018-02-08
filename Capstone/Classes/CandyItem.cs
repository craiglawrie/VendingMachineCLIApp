using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public class CandyItem : VendableItems
    {
        public CandyItem(string productCode, string name, decimal cost) : base(productCode, name, cost)
        {
        }

        public override string ConsumeMessage { get { return "Munch Munch, Yum!"; } }
    }
}
