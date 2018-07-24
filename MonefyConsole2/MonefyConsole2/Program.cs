using ConsoleFrontEnd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonifyConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Accounts accounts = Accounts.GetInstance();
            Categories categories = Categories.GetInstance();
            Payments payments = Payments.GetInstance();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            AppFront.Run();
            Accounts.Save();
            Categories.Save();
            Payments.Save();
            CurrencyModule.SaveCurrency();
        }
        
    }
}