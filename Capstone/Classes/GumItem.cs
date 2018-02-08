using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public class GumItem : VendableItems
    {
        public GumItem(decimal cost, string productCode, string name) : base(cost, productCode, name)
        {
        }

        public override string ConsumeMessage { get { return "Chew Chew, Yum!"; } }
    }
}
