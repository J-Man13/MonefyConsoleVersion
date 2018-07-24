using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;


namespace MonifyConsole
{
    [Serializable]
    class Accounts
    {
        private static double overAllBalance;
        public static double OverAllBalance
        {
            get
            {
                overAllBalance = 0;
                foreach (Account a in accountsList)
                {
                    overAllBalance += a.Balance;
                }
                return overAllBalance;
            }
        }
        private static SortedSet<Account> accountsList;
        private static Accounts accounts;
        private Accounts()
        {
            if (File.Exists("Accounts.dat"))
                accountsList = Load();
            else
                accountsList = new SortedSet<Account>();
        }

        public static bool AccountExists(string idName)
        {
            return GetAccount(idName) != null;
        }

        public static void AddAccount(string accountNameId, int accountBalance)
        {
            if (!AccountExists(accountNameId))
            {
                accountsList.Add(new Account(accountNameId, accountBalance));
                Save();
            }
            else { 
                Console.WriteLine(Literals.getLiterals("Account name or it's identity describer is not unique"));
                Console.ReadKey(true);
            }
        }

        public static void RemoveAccount(string accountNameId)
        {
            if(accountsList.RemoveWhere(Match(accountNameId)) > 0)
                Save();
            else
            {
                Console.WriteLine(Literals.getLiterals("Account does not exist"));
                Console.ReadKey(true);
            }
        }

        private static Predicate<Account> Match(string accountNameId)
        {
            return delegate (Account account)
            {
                return account.NameId.Equals(accountNameId);
            };
        }

        public static Accounts GetInstance()
        {
            return accounts == null ? accounts = new Accounts() : accounts;
        }

        public static void Save()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("Accounts.dat", FileMode.OpenOrCreate))
                binaryFormatter.Serialize(fs, accountsList);
        }

        public static SortedSet<Account> Load()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("Accounts.dat", FileMode.OpenOrCreate))
                return (SortedSet<Account>)binaryFormatter.Deserialize(fs);
        }

        public static void Indexify(double index)
        {
            foreach (Account acc in accountsList)
                acc.Balance = acc.Balance / index;
        }

        public static void Deindexify(double index)
        {
            foreach (Account acc in accountsList)
                acc.Balance = acc.Balance * index;
        }

        private static Account GetAccount(string idName)
        {
            try
            {
                return (from ac in accountsList
                        where ac.NameId.CompareTo(idName) == 0
                        select ac).First();
            }
            catch(Exception)
            {
                return null;
            }
        }

        public static void IncreaseAccountAmount(string idName,int amount)
        {
            Account ac = GetAccount(idName);
            ac.IncreaseBalance(amount);
            Save();
        }

        public static void DecreaseAccountAmount(string idName, int amount)
        {
            Account ac = GetAccount(idName);
            ac.DecreaseBalance(amount);
            Save();
        }

        public static void Show()
        {
            if (accountsList == null || accountsList.Count == 0)
                return;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(Literals.getLiterals("List of accounts and their balances"));
            foreach (Account ac in accountsList)
                Console.WriteLine(ac.NameId + ":" + ac.Balance.ToString("#.##"));
            Console.ForegroundColor = ConsoleColor.Green;
        }

        public static bool EnoughFunds(string accountId, int amount)
        {
            return GetAccount(accountId).Balance >= amount;
        }

    }
}
