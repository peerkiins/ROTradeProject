using System;
using System.Collections.Generic;

namespace ROTradeProject
{
    class Character
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int StatPoints { get; set; }
        public string Job { get; set; }
        public int Str { get; set; }
        public int Int { get; set; }
        public int Dex { get; set; }
        public int Agi { get; set; }
        public int Vit { get; set; }
        public int Luk { get; set; }
        public List<Item> Inventory = new List<Item>();

        // public Character()
        // {
        //     Inventory.Add(new Item { ID = 2305, Name = "Adventurer's Suit", Slot = 0, Type = "Body" });
        //     Inventory.Add(new Item { ID = 1207, Name = "Main Gauche", Slot = 3, Type = "One Hand" });
        // }

        public void StrStat()
        {
            if (StatPoints >= 1)
            {
                Str += 1;
                StatPoints -= 1;
            }
        }

        public void AgiStat()
        {
            if (StatPoints >= 1)
            {
                Agi += 1;
                StatPoints -= 1;
            }
        }

        public void VitStat()
        {
            if (StatPoints >= 1)
            {
                Vit += 1;
                StatPoints -= 1;
            }
        }
        public void IntStat()
        {
            if (StatPoints >= 1)
            {
                Int += 1;
                StatPoints -= 1;
            }
        }

        public void DexStat()
        {
            if (StatPoints >= 1)
            {
                Dex += 1;
                StatPoints -= 1;
            }
        }

        public void LukStat()
        {
            if (StatPoints >= 1)
            {
                Luk += 1;
                StatPoints -= 1;
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
            StatPoints = 44;
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