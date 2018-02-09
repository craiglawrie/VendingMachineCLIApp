using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
	public class Change
	{
		private const decimal QUARTER_VALUE = 0.25M;
		private const decimal DIME_VALUE = 0.10M;
		private const decimal NICKEL_VALUE = 0.05M;

        /// <summary>
        /// The number of nickels in the change, given minimum total number of coins.
        /// </summary>
		public int Nickels
		{
			get
			{
				return (int)((Total - Quarters * QUARTER_VALUE - Dimes * DIME_VALUE) / (NICKEL_VALUE));

			}
		}

        /// <summary>
        /// The number of dimes in the change, given minimum total number of coins.
        /// </summary>
		public int Dimes
		{
			get
			{
				return (int)((Total - Quarters * QUARTER_VALUE) / (DIME_VALUE));
			}
		}

        /// <summary>
        /// The number of quarters in the change, given minimum total number of coins.
        /// </summary>
		public int Quarters
		{
			get
			{
				return (int)(Total / QUARTER_VALUE);
			}
		}

        /// <summary>
        /// The total value of the change.
        /// </summary>
		public decimal Total { get; }

        /// <summary>
        /// A change object, calculates the number of quarters, dimes, and nickels
        /// needed to make change for an input of decimal total.
        /// </summary>
        /// <param name="total"></param>
		public Change(decimal total)
		{
			this.Total = total;
		}
	}
}
