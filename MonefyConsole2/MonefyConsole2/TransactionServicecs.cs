using MonifyConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonefyConsole
{
    class TransactionServicecs
    {
        public static void Transfer(string accountId, string category, int amount)
        {
            Categories.IncreaseExpense(category, amount);
            Accounts.DecreaseAccountAmount(accountId, amount);
            Payments.addPayment(accountId, category, amount);
        }
    }
}
