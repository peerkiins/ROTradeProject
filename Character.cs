using System;
using System.Collections.Generic;

namespace ROTradeProject
{
    class Character
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int UnusedStatPoints { get; set; }
        public string Job { get; set; }
        public int Str { get; set; }
        public int Int { get; set; }
        public int Dex { get; set; }
        public int Agi { get; set; }
        public int Vit { get; set; }
        public int Luk { get; set; }
        public List<Item> Inventory = new List<Item>();

        public void StrStat()
        {
            if (UnusedStatPoints >= 1)
            {
                Str += 1;
                UnusedStatPoints -= 1;
            }
        }

        public void AgiStat()
        {
            if (UnusedStatPoints >= 1)
            {
                Agi += 1;
                UnusedStatPoints -= 1;
            }
        }

        public void VitStat()
        {
            if (UnusedStatPoints >= 1)
            {
                Vit += 1;
                UnusedStatPoints -= 1;
            }
        }
        public void IntStat()
        {
            if (UnusedStatPoints >= 1)
            {
                Int += 1;
                UnusedStatPoints -= 1;
            }
        }

        public void DexStat()
        {
            if (UnusedStatPoints >= 1)
            {
                Dex += 1;
                UnusedStatPoints -= 1;
            }
        }

        public void LukStat()
        {
            if (UnusedStatPoints >= 1)
            {
                Luk += 1;
                UnusedStatPoints -= 1;
            }
        }

        public void StatReset()
        {
            Str = 0;
            Agi = 0;
            Vit = 0;
            Int = 0;
            Dex = 0;
            Luk = 0;
            UnusedStatPoints = 44;
        }

        public void InventoryItemIn(Item Transferable)
        {
            Inventory.Add(Transferable);
        }

        public void InventoryItemOut(Item Transferable)
        {
            Inventory.Remove(Transferable);
        }
    }
}