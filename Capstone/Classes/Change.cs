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

		public int Nickels
		{
			get
			{
				return (int)((Total - Quarters * QUARTER_VALUE - Dimes * DIME_VALUE) / (NICKEL_VALUE));

			}
		}
		public int Dimes
		{
			get
			{
				return (int)((Total - Quarters * QUARTER_VALUE) / (DIME_VALUE));
			}
		}
		public int Quarters
		{
			get
			{
				return (int)(Total / QUARTER_VALUE);
			}
		}


		public decimal Total { get; }

		public Change(decimal total)
		{
			this.Total = total;
		}
	}
}
