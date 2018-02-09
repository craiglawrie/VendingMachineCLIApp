using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Capstone.Classes
{
    public class VendingInput
    {
        /// <summary>
        /// Lookup table of items and corresponding vendable item types.
        /// Do not remove or modify items in this table. If new items are
        /// available in input file, those items should be appended to this
        /// dictionary.
        /// </summary>
        private static Dictionary<string, Type> itemTypes = new Dictionary<string, Type>
        {
            {"Potato Crisps", typeof(ChipItem) },
            {"Stackers", typeof(ChipItem) },
            {"Grain Waves", typeof(ChipItem) },
            {"Cloud Popcorn", typeof(ChipItem) },
            {"Moonpie", typeof(CandyItem) },
            {"Cowtales", typeof(CandyItem) },
            {"Wonka Bar", typeof(CandyItem) },
            {"Crunchie", typeof(CandyItem) },
            {"Cola", typeof(DrinkItem) },
            {"Dr. Salt", typeof(DrinkItem) },
            {"Mountain Melter", typeof(DrinkItem) },
            {"Slurm", typeof(DrinkItem) },
            {"Heavy", typeof(DrinkItem) },
            {"U-Chews", typeof(GumItem) },
            {"Little League Chew", typeof(GumItem) },
            {"Chiclets", typeof(GumItem) },
            {"Triplemint", typeof(GumItem) }
        };

        /// <summary>
        /// Creates a dictionary of vendable items keyed by product code / "slot".
        /// </summary>
        /// <param name="inputFile"></param>
        /// <returns></returns>
        public static Dictionary<string, VendableItems> RestockFromInputFile(string inputFile)
        {
            Dictionary<string, VendableItems> inventory = new Dictionary<string, VendableItems>();

            try
            {
                using (StreamReader sr = new StreamReader(inputFile))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] itemDetails = sr.ReadLine().Split('|');
                        Type type = itemTypes[itemDetails[1]];
                        Object item = Activator.CreateInstance(type, new object[] { itemDetails[0], itemDetails[1], decimal.Parse(itemDetails[2]) });

                        inventory[itemDetails[0]] = (VendableItems)item;
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Something went wrong with reading the input file.");
                Console.WriteLine(e.Message);
            }

            return inventory;
        }

    }
}
