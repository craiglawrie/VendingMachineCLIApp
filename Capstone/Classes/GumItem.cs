using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public class GumItem : VendableItems
    {
        public GumItem(string productCode, string name, decimal cost) : base(productCode, name, cost)
        {
        }

        public override string ConsumeMessage { get { return "Chew Chew, Yum!"; } }
    }
}
