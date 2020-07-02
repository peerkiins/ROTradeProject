using System.Collections.Generic;

namespace ROTradeProject
{
    class Server
    {
        public string Name { get; set; }
        public List<Account> Accounts = new List<Account>();
        private int _totalAccounts { get; set; }
        private int _totalCharacters { get; set; }


        public Server()
        {
            _totalAccounts = 0;
            _totalCharacters = 0;
        }

        public bool AccountIsExist(string Username)
        {
            foreach (Account account in Accounts)
            {
                if (account != null && account.Username == Username)
                    return true;
            }
            return false;
        }

        public void Registration(string Username, string Password, string Gender)
        {
            Accounts.Add(new Account { Username = Username, Password = Password, Gender = Gender });
        }

        public Account Login(string Username, string Password)
        {
            foreach (Account account in Accounts)
            {
                if (account != null && account.Username == Username && account.Password == Password)
                {
                    return account;
                }
            }
            return null;
        }

        public bool IsCharacterExist(string Name)
        {
            foreach (Account account in Accounts)
            {
                foreach (Character character in account.Characters)
                {
                    if (character != null && character.Name == Name)
                        return true;
                }
            }
            return false;
        }

        public Character MailReceiver(string CharacterName)
        {
            foreach (Account account in Accounts)
            {
                foreach (Character character in account.Characters)
                {
                    if (character != null && character.Name == Name)
                        return character;
                }
            }
            return null;
        }

        public Dictionary<int, Item> ItemDatabase = new Dictionary<int, Item>()
        {
            {2305, new Item{ID = 2305, Name = "Adventurer's Suit", Slot = 0, Type = "Body" } },
            {2306, new Item{ID = 2306, Name = "Adventurer's Suit", Slot = 1, Type = "Body" } },
            {1209, new Item{ID = 1209, Name = "Main Gauche", Slot = 0, Type = "One Hand" } },
            {1207, new Item{ID = 1207, Name = "Main Gauche", Slot = 3, Type = "One Hand" } },
            {1208, new Item{ID = 1208, Name = "Main Gauche", Slot = 4, Type = "One Hand" } },
        };
    }
}