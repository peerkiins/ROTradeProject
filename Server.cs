namespace ROTradeProject
{
    class Server
    {
        public string Name { get; set; }
        public Account[] Accounts { get; set; }
        public string[] CharacterNames { get; set; }
        private int _totalAccounts { get; set; }
        private int _totalCharacters { get; set; }

        public Server()
        {
            Accounts = new Account[10];
            CharacterNames = new string[90];
            _totalAccounts = 0;
            _totalCharacters = 0;
        }

        public bool IsUserExisted(string TempUsername)
        {
            foreach (Account account in Accounts)
            {
                if (account != null && account.Username == TempUsername)
                    return true;
            }
            return false;
        }

        public void Registration(string Username, string Password, string Gender)
        {
            if (_totalAccounts == Accounts.Length - 3)
            {
                AccountArrResize();
                Accounts[_totalAccounts++] = new Account
                {
                    Username = Username,
                    Password = Password,
                    Gender = Gender,
                };
            }
            else
            {
                Accounts[_totalAccounts++] = new Account
                {
                    Username = Username,
                    Password = Password,
                    Gender = Gender,
                };
            }
        }

        public void AccountArrResize()
        {
            Account[] TempAccounts = new Account[Accounts.Length + 10];
            for (int x = 0; x < _totalAccounts; x++)
            {
                TempAccounts[x] = Accounts[x];
                Accounts = TempAccounts;
            }
            string[] TempCharnames = new string[Accounts.Length + 90];
            for (int y = 0; y < _totalCharacters; y++)
            {
                TempCharnames[y] = CharacterNames[y];
                CharacterNames = TempCharnames;
            }
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

        public bool IsCharNameTaken(string Name)
        {
            for (int z = 0; z < _totalAccounts * 9; z++)
            {
                if (CharacterNames != null && CharacterNames[z] == Name)
                    return true;
            }
            return false;
        }

        public void NamesCopy(string Name)
        {
            CharacterNames[_totalCharacters++] = Name;
        }

        public void DeleteCharacter(string Name)
        {
            var idx = CharNameIdx(Name);
            if (idx != -1)
            {
                var firstHalf = new string[idx];
                CopyTo(CharacterNames, firstHalf, 0, idx);
                var secondHalf = new string[CharacterNames.Length - firstHalf.Length - 1];
                CopyTo(CharacterNames, secondHalf, idx + 1, idx + secondHalf.Length);
                var TempCharnames = new string[firstHalf.Length + secondHalf.Length];
                CopyTo(firstHalf, TempCharnames, 0, firstHalf.Length);
                CopyTo(secondHalf, TempCharnames, idx, firstHalf.Length + secondHalf.Length);
                CharacterNames = TempCharnames;
            }
        }

        public void CopyTo(string[] CharactersNameFrom, string[] CharactersNameTemp, int Startidx, int Endidx)
        {
            for (int x = Startidx; x < Endidx; x++)
            {
                CharactersNameTemp[x] = CharactersNameFrom[x];
            }
        }

        public int CharNameIdx(string Name)
        {
            for (var n = 0; n < _totalCharacters; n++)
            {
                if (CharacterNames != null && CharacterNames[n] == Name)
                {
                    return n;
                }
            }
            return -1;
        }

        public Account SendToAccnt(string Username)
        {
            foreach (Account account in Accounts)
            {
                if (account != null && account.Username == Username)
                {
                    return account;
                }
                // return account.CharacterReceiver(Name);
            }
            return null;
        }


        public void SendItem(int ItemId, string Name)
        {

        }

    }
}