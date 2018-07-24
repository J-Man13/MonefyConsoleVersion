using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonifyConsole
{
    [Serializable]
    class Payment : IComparable
    {
        private string accountNameId;
        public string AccountNameId
        {
            get
            {
                return String.Copy(accountNameId);
            }

            set
            {
                accountNameId = String.Copy(value);
            }
        }

        private string categoryNameId;
        public string CategoryNameId
        {
            get
            {
                return String.Copy(categoryNameId);
            }

            set
            {
                categoryNameId = String.Copy(value);
            }
        }

        private int amount;
        public int Amount
        {
            get
            {
                return amount;
            }
            set
            {
                amount = value;
            }
        }

        private DateTime dateTime;
        public DateTime DateTime
        {
            get
            {
                return new DateTime(dateTime.Year,dateTime.Month,dateTime.Day,dateTime.Hour,dateTime.Minute,dateTime.Second);
            }
            set
            {
                dateTime = new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second);
            }
        }

        public Payment(string accountNameId, string categoryNameId, int amount)
        {
            AccountNameId = accountNameId;
            CategoryNameId = categoryNameId;
            Amount = amount;
            dateTime = DateTime.Now;
        }

        public Payment(string accountNameId, string categoryNameId, int amount, DateTime dateTime)
        {
            AccountNameId = accountNameId;
            CategoryNameId = categoryNameId;
            Amount = amount;
            dateTime = DateTime.Now;
        }

        public int CompareTo(object obj)
        {
            try
            {
                return accountNameId.CompareTo((obj as Payment).accountNameId);
            }
            catch (InvalidCastException)
            {
                Console.WriteLine("Invalid Cast");
                return -999999;
            }
        }

        public override string ToString()
        {
            return accountNameId + " " + categoryNameId + " " + amount + " " + dateTime;
        }

    }
}
