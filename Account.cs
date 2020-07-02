using System;
using System.Collections.Generic;

namespace ROTradeProject
{
    class Account
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public List<Character> Characters = new List<Character>();
        public List<Item> Storage = new List<Item>();

        public void CreateChar(string Name, Item Freebie1, Item Freebie2)
        {
            Characters.Add(new Character
            {
                Name = Name,
                Level = 10,
                Job = "Novice",
                UnusedStatPoints = 50,
                Str = 0,
                Agi = 0,
                Vit = 0,
                Int = 0,
                Dex = 0,
                Luk = 0,
                Inventory = new List<Item>()
                {
                    Freebie1,
                    Freebie2
                }
            });
        }

        public void DeleteChar(Character CurrentCharacter)
        {
            Characters.Remove(CurrentCharacter);
        }

        public Character Characterselect(string CharacterName)
        {
            foreach (Character character in Characters)
            {
                if (character != null && character.Name == CharacterName)
                    return character;
            }
            return null;
        }

        public void StorageDeposit(Item Transferable)
        {
            Storage.Add(Transferable);
        }

        public void StorageWithdraw(Item Transferable)
        {
            Storage.Remove(Transferable);
        }
    }
}