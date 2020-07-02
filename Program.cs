using System;

namespace ROTradeProject
{
    class Program
    {
        static string TempUsername { get; set; }
        static string TempPassword { get; set; }
        static string TempCharName { get; set; }
        static Account ReceiverAccount { get; set; }
        static Character ReceiverCharacter { get; set; }
        static Account CurrentAccount { get; set; }
        static Character CurrentChar { get; set; }
        static string Gender { get; set; }
        static Server PRO = new Server();

        static void Main(string[] args)
        {
            PRO.Name = "Perkins RO";
            Console.Clear();
            Console.WriteLine($"\nWelcome to {PRO.Name}!");
            bool ShouldExit = false;
            while (!ShouldExit)
            {
                Console.Clear();
                Console.WriteLine($"[{PRO.Name}]");
                Console.WriteLine("\nPlease select your option:");
                switch (ShowMenu("[Register an Account]", "[Account Login]", "[Exit]"))
                {
                    case '1':
                        ShowRegistration();
                        continue;
                    case '2':
                        ShowLogin();
                        continue;
                    case '3':
                        ShouldExit = true;
                        break;
                    default:
                        Console.Clear();
                        continue;
                }
            }
        }

        static char ShowMenu(params string[] options)
        {
            string MenuString = "Press ";
            for (int i = 0; i < options.Length; i++)
            {
                string PostFix = i == options.Length - 1 ? string.Empty : ", ";
                MenuString += $"{i + 1} to {options[i]}{PostFix}";
            }
            Console.WriteLine($"{MenuString}, ");
            ConsoleKeyInfo key = Console.ReadKey();
            Console.WriteLine();
            return key.KeyChar;
        }

        static void ShowRegistration()
        {
            Console.Clear();
            Console.WriteLine("[Account Registration]");
            Console.Write("\nPlease enter new Username: ");
            TempUsername = Console.ReadLine().Trim();
            Console.Clear();
            if (TempUsername.Length >= 4)
            {
                if (!PRO.AccountIsExist(TempUsername))
                {
                    Console.WriteLine("[Account Registration]");
                    Console.Write("\nenter new Password: ");
                    TempPassword = Console.ReadLine();
                    Console.Clear();
                    Console.WriteLine("[Account Registration]");
                    Console.Write("\nEnter [M] for Male, or [F] for Female: ");
                    char key;
                    char.TryParse(Console.ReadLine().ToUpper(), out key);
                    Gender = GenderSelect(key);
                    if (Gender != null)
                    {
                        PRO.Registration(TempUsername, TempUsername, Gender);
                        Console.WriteLine("New account registered! You may now login.");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Invalid gender input. Please try again.");
                        Console.ReadKey();
                    }

                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\nAccount already existed.");
                    Console.ReadLine();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("\nInvalid username.");
                Console.ReadLine();
            }
        }

        static string GenderSelect(char G)
        {
            switch (G)
            {
                case 'M':
                    return "Male";
                case 'F':
                    return "Female";
                default:
                    return null;
            }
        }

        static void ShowLogin()
        {
            Console.Clear();
            Console.WriteLine("[Account Login]");
            Console.Write("\nUsername: ");
            TempUsername = Console.ReadLine();
            Console.Write("Password: ");
            TempPassword = Console.ReadLine();
            CurrentAccount = PRO.Login(TempUsername, TempPassword);
            if (CurrentAccount != null)
            {
                bool shouldLogout = false;
                while (!shouldLogout)
                {
                    Console.Clear();
                    Console.WriteLine($"\n[{CurrentAccount.Username} > Character Board]\n");
                    ShowCharacters();
                    switch (ShowMenu("[Create Character]", "[Select Character]", "[Logout]"))
                    {
                        case '1':
                            ShowCreateChar();
                            continue;
                        case '2':
                            ShowSelectChar();
                            continue;
                        case '3':
                            shouldLogout = true;
                            Console.Clear();
                            continue;
                        default:
                            Console.Clear();
                            continue;
                    }
                }
            }
            else if (CurrentAccount == null)
            {
                Console.Clear();
                Console.WriteLine("Login failed, please try again.");
                Console.ReadKey();
            }
        }

        static void ShowCreateChar()
        {
            Console.Clear();
            Console.WriteLine($"\n[{CurrentAccount.Username} > Create Character]\n");
            Console.Write("\nName: ");
            TempCharName = Console.ReadLine().Trim();
            if (TempCharName.Length >= 4 && TempCharName.Length <= 20)
            {
                if (!PRO.IsCharacterExist(TempCharName))
                {
                    if (CurrentAccount.Characters.Count < 9)
                    {
                        Item Freebie1 = Freebies(2305);
                        Item Freebie2 = Freebies(1207);
                        CurrentAccount.CreateChar(TempCharName, Freebie1, Freebie2);
                        Console.Clear();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Maximum number of characters reached.");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Name is already taken.");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Name must be 4 to 20 characters.");
                Console.ReadKey();
            }
        }

        static void ShowSelectChar()
        {
            Console.Clear();
            Console.WriteLine($"\n[{CurrentAccount.Username} > Character Select]\n");
            Console.Write("\nEnter Character's Name: ");
            TempCharName = Console.ReadLine();
            CurrentChar = CurrentAccount.Characterselect(TempCharName);
            if (CurrentChar != null)
            {
                bool toChangeChar = false;
                Console.Clear();
                while (!toChangeChar)
                {
                    Console.Clear();
                    Console.WriteLine($"[{CurrentAccount.Username} > {CurrentChar.Name} > Character Options]");
                    Console.WriteLine("---------------------------------------------");
                    Console.WriteLine($"Character : {CurrentChar.Name}\tLvl : {CurrentChar.Level}\tJob : {CurrentChar.Job}");
                    Console.WriteLine($"Statpoint : {CurrentChar.StatPoints}\nStr : {CurrentChar.Str}\nAgi : {CurrentChar.Agi}\nVit : {CurrentChar.Vit}\nInt : {CurrentChar.Int}\nDex : {CurrentChar.Dex}\nLuk : {CurrentChar.Luk}");
                    Console.WriteLine("---------------------------------------------");
                    switch (ShowMenu("[Stats]", "[Inventory]", "[Delete Character]", "[Change Character]"))
                    {
                        case '1':
                            ShowStats();
                            continue;
                        case '2':
                            ShowInventory();
                            continue;
                        case '3':
                            CurrentAccount.DeleteChar(CurrentChar);
                            toChangeChar = true;
                            continue;
                        case '4':
                            toChangeChar = true;
                            continue;
                        default:
                            Console.Clear();
                            continue;
                    }
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Character does not exist.");
                Console.ReadKey();
            }
        }

        static void ShowStats()
        {
            bool IsAssignout = false;
            while (!IsAssignout)
            {
                Console.Clear();
                Console.WriteLine($"[{CurrentAccount.Username} > {CurrentChar.Name} > Stats]\n");
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine($"Character : {CurrentChar.Name}\tLvl : {CurrentChar.Level}\tJob : {CurrentChar.Job}");
                Console.WriteLine($"Statpoint : {CurrentChar.StatPoints}\nStr : {CurrentChar.Str}\nAgi : {CurrentChar.Agi}\nVit : {CurrentChar.Vit}\nInt : {CurrentChar.Int}\nDex : {CurrentChar.Dex}\nLuk : {CurrentChar.Luk}");
                Console.WriteLine("---------------------------------------------");
                switch (ShowMenu($"[Str+1]", "[Agi+1]", "[Vit+1]", "[Int+1]", "[Dex+1]", "[Luk+1]", "[Reset]", "[Back]"))
                {
                    case '1':
                        CurrentChar.StrStat();
                        continue;
                    case '2':
                        CurrentChar.AgiStat();
                        continue;
                    case '3':
                        CurrentChar.VitStat();
                        continue;
                    case '4':
                        CurrentChar.IntStat();
                        continue;
                    case '5':
                        CurrentChar.DexStat();
                        continue;
                    case '6':
                        CurrentChar.LukStat();
                        continue;
                    case '7':
                        CurrentChar.StatReset();
                        continue;
                    case '8':
                        IsAssignout = true;
                        continue;
                }
            }
        }

        static void ShowInventory()
        {
            bool IsBagClose = false;
            while (!IsBagClose)
            {
                Console.Clear();
                Console.WriteLine($"[{CurrentAccount.Username} > {CurrentChar.Name} > Inventory]\n");
                ShowEquipBag();
                switch (ShowMenu("[Storage]", "[Mail Item]", "[Back]"))
                {
                    case '1':
                        Console.Clear();
                        ShowEquipStorage();
                        ShowEquipBag();
                        bool IsStorageClose = false;
                        while (!IsStorageClose)
                        {
                            switch (ShowMenu("[Deposit]", "[Withdraw]", "[Back]"))
                            {
                                case '1':
                                    Console.Clear();
                                    Console.WriteLine($"[{CurrentAccount.Username} > {CurrentChar.Name} > Inventory > Storage > Deposit]\n");
                                    Console.Write("Enter item id: ");
                                    int DepItemId;
                                    int.TryParse(Console.ReadLine(), out DepItemId);
                                    if (PRO.ItemDatabase.ContainsKey(DepItemId))
                                    {
                                        Item Transferable;
                                        PRO.ItemDatabase.TryGetValue(DepItemId, out Transferable);
                                        if (CurrentChar.Inventory.Contains(Transferable))
                                        {
                                            CurrentAccount.StorageDeposit(Transferable);
                                            CurrentChar.InventoryItemOut(Transferable);
                                        }
                                        else
                                        {
                                            Console.WriteLine("You don't have that item.");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid item id.");
                                    }
                                    continue;
                                case '2':
                                    Console.WriteLine($"[{CurrentAccount.Username} > {CurrentChar.Name} > Inventory > Storage > Deposit]\n");
                                    Console.Write("Enter item id: ");
                                    int WthItemId;
                                    int.TryParse(Console.ReadLine(), out WthItemId);
                                    if (PRO.ItemDatabase.ContainsKey(WthItemId))
                                    {
                                        Item Transferable;
                                        PRO.ItemDatabase.TryGetValue(WthItemId, out Transferable);
                                        if (CurrentChar.Inventory.Contains(Transferable))
                                        {
                                            CurrentAccount.StorageWithdraw(Transferable);
                                            CurrentChar.InventoryItemIn(Transferable);
                                            Console.Clear();
                                        }
                                        else
                                        {
                                            Console.WriteLine("You don't have that item.");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid item id.");
                                    }
                                    continue;
                                case '3':
                                    IsStorageClose = true;
                                    continue;
                            }
                        }
                        continue;

                    case '2':
                        Console.Clear();
                        Console.WriteLine($"[{CurrentAccount.Username} > {CurrentChar.Name} > Inventory > Mail Item]\n");
                        ShowEquipBag();
                        Console.Write("\nMail to character(Name): ");
                        TempCharName = Console.ReadLine();
                        if (PRO.IsCharacterExist(TempCharName))
                        {
                            Character Receiver = PRO.MailReceiver(TempCharName);
                            Console.Clear();
                            Console.WriteLine($"[{CurrentAccount.Username} > {CurrentChar.Name} > Inventory > Mail Item]\n");
                            ShowEquipBag();
                            Console.Write("Enter item id: ");
                            int Itemid;
                            int.TryParse(Console.ReadLine(), out Itemid);
                            if (PRO.ItemDatabase.ContainsKey(Itemid))
                            {
                                Item Transferable;
                                PRO.ItemDatabase.TryGetValue(Itemid, out Transferable);
                                if (CurrentChar.Inventory.Contains(Transferable))
                                {
                                    CurrentChar.InventoryItemOut(Transferable);
                                    Receiver.InventoryItemIn(Transferable);
                                    Console.Clear();
                                }
                                else
                                {
                                    Console.WriteLine("You don't have that item.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid item id.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Character does not exist.");
                        }
                        continue;
                    case '3':
                        IsBagClose = true;
                        continue;
                    default:
                        Console.Clear();
                        continue;
                }
            }
        }

        static void ShowCharacters()
        {
            foreach (Character character in CurrentAccount.Characters)
            {
                Console.WriteLine($"Character : {character.Name}\t\tLvl : {character.Level}\tJob : {character.Job}");
                Console.WriteLine($"Str : {character.Str}\t\tInt : {character.Int}\nAgi : {character.Agi}\t\tDex : {character.Dex}\nVit : {character.Vit}\t\tLuk : {character.Luk}\t\tStatpoint : {character.StatPoints}");
                Console.WriteLine("---------------------------------------------------");
            }
        }

        static void ShowEquipBag()
        {
            if (CurrentChar.Inventory.Count > 0)
            {
                Console.WriteLine("[Bag Items]");
                Console.WriteLine("Item ID:\tItem:");
                foreach (Item item in CurrentChar.Inventory)
                {
                    Console.WriteLine($"{item.ID}\t\t{item.Name}[{item.Slot}]");
                }
                Console.WriteLine("---------------------------------------------");
            }
            else
            {
                Console.WriteLine("[Bag Items]");
                Console.WriteLine("\n\t[Empty]\n\n");
                Console.WriteLine("---------------------------------------------");
            }
        }

        static void ShowEquipStorage()
        {
            if (CurrentAccount.Storage.Count > 0)
            {
                Console.WriteLine("[Storage Items]");
                Console.WriteLine("Item ID:\tItem:");
                foreach (Item item in CurrentAccount.Storage)
                {
                    Console.WriteLine($"{item.ID}\t\t{item.Name}[{item.Slot}]");
                }
                Console.WriteLine("---------------------------------------------");
            }
            else
            {
                Console.WriteLine("[Storage Items]");
                Console.WriteLine("\n\t[Empty]\n\n");
                Console.WriteLine("---------------------------------------------");
            }
        }
        static Item Freebies(int f)
        {
            if (PRO.ItemDatabase.ContainsKey(f))
            {
                Item Freebies;
                PRO.ItemDatabase.TryGetValue(f, out Freebies);
                return Freebies;
            }
            return null;
        }
    }
}
