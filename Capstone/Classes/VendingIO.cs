using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace Capstone.Classes
{
    public class VendingIO
    {

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

        public Dictionary<string, VendableItems> ReadInput(string inputFile)
        {
            Dictionary<string, VendableItems> inventory = new Dictionary<string, VendableItems>();

            try
            {
                using (StreamReader sr = new StreamReader(inputFile))
                {
                    string[] itemDetails = sr.ReadLine().Split('|');
                    Type type = itemTypes[itemDetails[1]];
                    ConstructorInfo ctor = type.GetConstructor(new[] { typeof(decimal), typeof(string), typeof(string) });
                    Object item = ctor.Invoke(new object[] { itemDetails[0], itemDetails[1], decimal.Parse(itemDetails[2]) });

                    inventory[itemDetails[0]] = (VendableItems)item;
                }
            }
            catch(IOException e)
            {
                Console.WriteLine("Something went wrong with reading the input file.");
                Console.WriteLine(e.Message);
            }

            return inventory;
        }

    }
}
