using System;

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
        public Item[] Items { get; set; }
        private int _bagslot { get; set; }

        public Character()
        {
            Items = new Item[30];
            _bagslot = 0;
            Items[_bagslot++] = new Item
            {
                ID = 0,
                Name = "Adventurer Suit[4]",
                Type = "Body"
            };
            Items[_bagslot++] = new Item
            {
                ID = 1,
                Name = "Knife[4]",
                Type = "Weapon"
            };
        }

        public void ReceiveItem(int ItemId)
        {
            Items[_bagslot++] = ItemReceived(ItemId);
        }

        public void SendItem(int ItemId)
        {
            var idx = ItemIdx(ItemId);
            if (idx != -1)
            {
                var firstHalf = new Item[idx];
                CopyTo(Items, firstHalf, 0, idx);
                var secondHalf = new Item[Items.Length - firstHalf.Length - 1];
                CopyTo(Items, secondHalf, idx + 1, idx + secondHalf.Length);
                var nItems = new Item[firstHalf.Length + secondHalf.Length];
                CopyTo(firstHalf, nItems, 0, firstHalf.Length);
                CopyTo(secondHalf, nItems, idx, firstHalf.Length + secondHalf.Length);
                Items = nItems;
            }
        }

        public int ItemIdx(int ItemId)
        {
            foreach (Item item in Items)
            {
                for (var i = 0; i < _bagslot; i++)
                {
                    if (item != null && item.ID == ItemId)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
        public Item ItemReceived(int ItemId)
        {
            foreach (Item item in Items)
            {
                for (var i = 0; i < _bagslot; i++)
                {
                    if (item != null && item.ID == ItemId)
                    {
                        return item;
                    }
                }
            }
            return null;
        }

        public void CopyTo(Item[] Itemsfrom, Item[] ItemsTemp, int Startidx, int Endidx)
        {
            for (int x = Startidx; x < Endidx; x++)
            {
                ItemsTemp[x] = Itemsfrom[x];
            }
        }

        public void DisplayInventory()
        {
            Console.WriteLine("[Bag]");
            foreach (Item item in Items)
            {
                for (int i = 0; i < _bagslot; i++)
                {
                    if (item != null)
                    {
                        Console.WriteLine($"{item.ID}\t{item.Type}\t{item.Name}\n");
                    }
                }
            }
        }
    }
}