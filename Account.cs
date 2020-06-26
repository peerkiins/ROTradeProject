using System;

namespace ROTradeProject
{
    class Account
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public Character[] Characters { get; set; }
        public int _cSlot { get; set; }

        public Account()
        {
            Characters = new Character[9];
            _cSlot = 0;
        }

        public void CreateChar(string Name)
        {
            Characters[_cSlot++] = new Character
            {
                Name = Name,
                Level = 10,
                StatPoints = 44,
                Job = "Novice",
                Str = 0,
                Int = 0,
                Agi = 0,
                Dex = 0,
                Vit = 0,
                Luk = 0
            };
        }

        public void ShowCharacterList()
        {
            Console.WriteLine("[Character List]");
            foreach (Character character in Characters)
            {
                for (int x = 0; x < _cSlot; x++)
                {
                    if (character != null)
                    {
                        Console.WriteLine($"\nName:{character.Name}\tlvl.{character.Level}\tJob: {character.Job}");
                        Console.WriteLine($"Stat Points:{character.StatPoints}");
                        Console.WriteLine($"Str:{character.Str}");
                        Console.WriteLine($"Int:{character.Int}");
                        Console.WriteLine($"Agi:{character.Agi}");
                        Console.WriteLine($"Dex:{character.Dex}");
                        Console.WriteLine($"Vit:{character.Vit}");
                        Console.WriteLine($"Luk:{character.Luk}");
                        Console.WriteLine("-----------------------------------");
                    }
                }
            }
        }


        public Character SelectCharacter(string Name)
        {
            foreach (Character character in Characters)
            {
                if (character != null && character.Name == Name)
                {
                    return character;
                }
            }
            return null;
        }

        public void DeleteChar(string Name)
        {
            var idx = CharacterIndex(Name);
            if (idx != -1)
            {
                var firstHalf = new Character[idx];
                CopyTo(Characters, firstHalf, 0, idx);
                var secondHalf = new Character[Characters.Length - firstHalf.Length - 1];
                CopyTo(Characters, secondHalf, idx + 1, idx + secondHalf.Length);
                var nCharacters = new Character[9];
                CopyTo(firstHalf, nCharacters, 0, firstHalf.Length);
                CopyTo(secondHalf, nCharacters, idx, firstHalf.Length + secondHalf.Length);
                Characters = nCharacters;
            }
        }

        public void CopyTo(Character[] charactersfrom, Character[] charactersTemp, int Startidx, int Endidx)
        {
            for (int x = Startidx; x < Endidx; x++)
            {
                charactersTemp[x] = charactersfrom[x];
            }
        }

        public int CharacterIndex(string Name)
        {
            foreach (Character character in Characters)
            {
                for (var n = 0; n < _cSlot; n++)
                {
                    if (character != null && character.Name == Name)
                    {
                        return n;
                    }
                }
            }
            return -1;
        }

        public Character CharacterReceiver(string Name)
        {
            foreach (Character character in Characters)
            {
                if (character != null && character.Name == Name)
                {
                    return character;
                }
            }
            return null;
        }
    }
}