using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonifyConsole
{
    [Serializable]
    class Category : IComparable
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
        private double expense;
        public double Expense
        {
            get
            {
                return expense;
            }
            set
            {
                expense = value;
            }

        }

        
        public Category(string nameId, double expense)
        {
            NameId = nameId;
            Expense = expense;
        }

        public int CompareTo(object obj)
        {
            return nameId.CompareTo((obj as Category).nameId);
        }

        public void expenseIncrease(double amount)
        {
            Expense += amount;
        }
    }
}
