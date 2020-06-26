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
        static Server A = new Server();

        static void Main(string[] args)
        {
            A.Name = "Perkins RO";
            Console.Clear();
            Console.WriteLine($"\nWelcome to {A.Name}!");
            bool ShouldExit = false;
            while (!ShouldExit)
            {
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
            if (TempUsername.Length <= 3)
            {
                Console.WriteLine("\nInvalid Username");
            }
            else if (A.IsUserExisted(TempUsername) == true)
            {
                Console.Clear();
                Console.WriteLine("\nUsername is already Taken.");
            }
            else
            {
                Console.Write("\nenter new Password: ");
                TempPassword = Console.ReadLine();
                Console.Write("\nEnter [M] for Male, or [F] for Female: ");
                char key;
                char.TryParse(Console.ReadLine().ToUpper(), out key);
                while (key == '\0')
                    switch (key)
                    {
                        case 'M':
                            Gender = "Male";
                            continue;
                        case 'F':
                            Gender = "Female";
                            continue;
                        default:
                            Console.WriteLine("Incorrect input, please try again.");
                            break;
                    }
                A.Registration(TempUsername, TempUsername, Gender);
                Console.Clear();
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
            CurrentAccount = A.Login(TempUsername, TempPassword);
            if (CurrentAccount == null)
            {
                Console.WriteLine("Username and Password does not match.");
            }
            else if (CurrentAccount != null)
            {
                bool shouldLogout = false;
                while (!shouldLogout)
                {
                    Console.Clear();
                    Console.WriteLine($"Hello {CurrentAccount.Username}, Welcome to {A.Name}");
                    Console.WriteLine("\n[Character Options]\n");
                    switch (ShowMenu("[Create Character]", "[Select Character]", "[Delete Character]", "[Logout]"))
                    {
                        case '1':
                            ShowCreateChar();
                            continue;
                        case '2':
                            ShowSelectChar();
                            continue;
                        case '3':
                            ShowDeleteChar();
                            continue;
                        case '4':
                            shouldLogout = true;
                            Console.Clear();
                            continue;
                        default:
                            Console.Clear();
                            continue;
                    }
                }
            }
        }

        static void ShowCreateChar()
        {
            Console.Clear();
            Console.WriteLine("[Create Character]");
            Console.Write("\nName: ");
            TempCharName = Console.ReadLine().Trim();
            if (TempCharName.Length <= 3 || TempCharName.Length > 8)
            {
                Console.WriteLine("Name must be 4 to 8 characters.");
            }
            else if (A.IsCharNameTaken(TempCharName))
            {
                Console.WriteLine("Name is already taken.");
            }
            else
            {
                A.NamesCopy(TempCharName);
                CurrentAccount.CreateChar(TempCharName);
            }
        }

        static void ShowSelectChar()
        {
            Console.Clear();
            CurrentAccount.ShowCharacterList();
            Console.WriteLine("\n[Select Character]");
            Console.Write("\nEnter Character's Name: ");
            TempCharName = Console.ReadLine();
            CurrentChar = CurrentAccount.SelectCharacter(TempCharName);
            Console.Clear();
            if (CurrentChar == null)
            {
                Console.WriteLine("Character does not exist.");
            }
            else if (CurrentChar != null)
            {
                bool toChangeChar = false;
                Console.Clear();
                while (!toChangeChar)
                {
                    Console.Clear();
                    Console.WriteLine($"[Character : {CurrentChar.Name}]\n");
                    switch (ShowMenu("[Stats]", "[Inventory]", "[Change Character]"))
                    {
                        case '1':
                            ShowStats();
                            continue;
                        case '2':
                            ShowInventory();
                            continue;
                        case '3':
                            toChangeChar = true;
                            continue;
                        default:
                            Console.Clear();
                            continue;
                    }
                }
            }
        }
        static void ShowCharStats()
        {
            Console.Clear();
            Console.WriteLine($"[{CurrentChar.Name}]\tlvl.{CurrentChar.Level}\tJob:{CurrentChar.Job}");
            Console.WriteLine($"Statpoints : {CurrentChar.StatPoints}\nStr > {CurrentChar.Str}\nInt > {CurrentChar.Int}\nDex > {CurrentChar.Dex}\nAgi > {CurrentChar.Agi}\nVit > {CurrentChar.Vit}\nLuk > {CurrentChar.Luk}\n");
        }
        static void ShowStats()
        {
            bool IsAssignout = false;
            while (!IsAssignout)
            {
                ShowCharStats();
                switch (ShowMenu($"[Str+1]", "[Int+1]", "[Dex+1]", "[Agi+1]", "[Vit+1]", "[Luk+1]", "[Reset]", "[Back]"))
                {
                    case '1':
                        if (CurrentChar.StatPoints >= 1)
                        {
                            CurrentChar.Str += 1;
                            CurrentChar.StatPoints -= 1;
                        }
                        continue;
                    case '2':
                        if (CurrentChar.StatPoints >= 1)
                        {
                            CurrentChar.Int += 1;
                            CurrentChar.StatPoints -= 1;
                        }
                        continue;
                    case '3':
                        if (CurrentChar.StatPoints >= 1)
                        {
                            CurrentChar.Dex += 1;
                            CurrentChar.StatPoints -= 1;
                        }
                        continue;
                    case '4':
                        if (CurrentChar.StatPoints >= 1)
                        {
                            CurrentChar.Agi += 1;
                            CurrentChar.StatPoints -= 1;
                        }
                        continue;
                    case '5':
                        if (CurrentChar.StatPoints >= 1)
                        {
                            CurrentChar.Vit += 1;
                            CurrentChar.StatPoints -= 1;
                        }
                        continue;
                    case '6':
                        if (CurrentChar.StatPoints >= 1)
                        {
                            CurrentChar.Luk += 1;
                            CurrentChar.StatPoints -= 1;
                        }
                        continue;
                    case '7':
                        CurrentChar.Str = 0;
                        CurrentChar.Int = 0;
                        CurrentChar.Dex = 0;
                        CurrentChar.Agi = 0;
                        CurrentChar.Vit = 0;
                        CurrentChar.Luk = 0;
                        CurrentChar.StatPoints = 44;
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
                Console.WriteLine($"[Inventory : {CurrentChar.Name}]\n");
                CurrentChar.DisplayInventory();
                switch (ShowMenu("[Mail Item]", "[Back]"))
                {
                    case '1':
                        Console.Clear();
                        Console.WriteLine("[Mail Items]");
                        Console.Write("Item ID: ");
                        int TempItemId;
                        int.TryParse(Console.ReadLine(), out TempItemId);
                        Console.Write("\nMail to account: ");
                        TempUsername = Console.ReadLine();
                        Console.Write("\nMail to character: ");
                        TempCharName = Console.ReadLine();
                        if (A.IsCharNameTaken(TempCharName))
                        {
                            CurrentChar.SendItem(TempItemId); //Item Delete from Source
                            ReceiverAccount = A.SendToAccnt(TempUsername);
                            ReceiverCharacter = ReceiverAccount.CharacterReceiver(TempCharName);
                            ReceiverCharacter.ReceiveItem(TempItemId);
                        }
                        else if (!A.IsCharNameTaken(TempCharName))
                        {
                            Console.WriteLine("\nCharacter does not exist!.");
                        }
                        continue;
                    case '2':
                        IsBagClose = true;
                        continue;
                    default:
                        Console.Clear();
                        continue;
                }
            }
        }

        static void ShowDeleteChar()
        {
            Console.Clear();
            Console.WriteLine("\n[Delete Character]");
            Console.Write("Choose a character to delete: ");
            TempCharName = Console.ReadLine();
            CurrentAccount.DeleteChar(TempCharName);
            A.DeleteCharacter(TempCharName);
        }
    }
}
