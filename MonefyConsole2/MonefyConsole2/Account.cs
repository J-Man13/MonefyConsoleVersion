using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonifyConsole
{
    [Serializable]
    class Account : IComparable
    {
        private string nameId;
        public string NameId
        {
            get
            {
                return String.Copy(nameId);
            }

            set
            {
                nameId = String.Copy(value);
            }
        }
        private double balance;
        public double Balance
        {
            get
            {
                return balance;
            }
            set
            {
                balance = value;
            }
        }

        public Account(string nameId, int balance)
        {
            NameId = nameId;
            Balance = balance;
        }

        public int CompareTo(object obj)
        {
            try
            { 
            return NameId.CompareTo((obj as Account).NameId);
            }
            catch (InvalidCastException)
            {
                Console.WriteLine("Invalid Cast");
                return -999999;
            }
        }

        public void IncreaseBalance(int amount)
        {
            Balance += amount;
        }

        public void DecreaseBalance(int amount)
        {
            Balance -= amount;
        }
    }
}
