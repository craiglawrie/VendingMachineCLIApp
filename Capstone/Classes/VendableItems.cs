using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public abstract class VendableItems
    {
        /// <summary>
        /// All items in the vending machine have the same restock quantity.
        /// </summary>
        public static int RestockQuantity { get { return 5; } }

        /// <summary>
        /// Create a new item.
        /// </summary>
        /// <param name="cost">The item cost.</param>
        /// <param name="productCode">The item product code. I.e. "A4".</param>
        /// <param name="name">The item name.</param>
        public VendableItems(decimal cost, string productCode, string name)
        {
            this.Cost = cost;
            this.ProductCode = productCode;
            this.Name = name;
        }

        /// <summary>
        /// The item cost. Must be provided in constructor.
        /// </summary>
        public decimal Cost { get; }

        /// <summary>
        /// The item product code. Must be provided in constructor.
        /// </summary>
        public string ProductCode { get; }

        /// <summary>
        /// The item name. Must be provided in constructor.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The number of this item sold since the last restock.
        /// </summary>
        public int NumberSold { get; protected set; }

        /// <summary>
        /// A message to the user specific to the type of item.
        /// </summary>
        public abstract string ConsumeMessage { get; }

        /// <summary>
        /// The current inventory of this item. Derived from the restock quantity and the
        /// current number sold.
        /// </summary>
        public int AmountRemaining
        {
            get
            {
                return RestockQuantity - NumberSold;
            }
        }

        /// <summary>
        /// Removes qty 1 from this item inventory. Returns true if successful. If there are no
        /// items to sell, returns false.
        /// </summary>
        /// <returns></returns>
        public bool SellOne()
        {
            bool result = false;

            if (AmountRemaining > 0)
            {
                this.NumberSold++;
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Resets the number sold to zero. All items will be set to the restock quantity.
        /// (See VendableItems.RestockQuantity)
        /// </summary>
        public void Restock()
        {
            this.NumberSold = 0;
        }

    }
}
