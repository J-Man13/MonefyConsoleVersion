using MonefyConsole;
using MonifyConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleFrontEnd
{
    class FirstMenu
    {
        private static Literals literals;
        private static Accounts accounts;
        private static Payments payments;
        private static Categories categories;
        private static OptionsMenu optionsMenu;
        private static string AccountNameId;
        private static int Amount;

        public static FirstMenu firstMenu;


        private FirstMenu()
        {
            literals = Literals.getInstance();
            accounts = Accounts.GetInstance();
            payments = Payments.GetInstance();
            categories = Categories.GetInstance();
            optionsMenu = OptionsMenu.GetInstance(accounts, categories);
        }

        public static FirstMenu GetInstance()
        {
            return firstMenu == null ? firstMenu = new FirstMenu() : firstMenu;
        }

        public void Run()
        {
            if (!Categories.CategoriesExist())
                TestValues();

            while (true)
            {                
                int select = RunFirstMenu();
                if (select == 1)
                    IncreaseBalanceMenu();
                else if (select == 2)
                    DecreaseBalanceMenu();
                else if (select == 3)
                    optionsMenu.Run();
                else if (select == 4)
                    break;
                else continue;
            }
        }

        private static void TestValues()
        {
            Categories.AddCategory("Alcohol");
            Categories.AddCategory("Home");
            Categories.AddCategory("Food");
            Categories.AddCategory("Car");
            Categories.AddCategory("Sports");
            Categories.AddCategory("Goods");
            Categories.AddCategory("Medicine");
            Categories.AddCategory("Clothes");
            Categories.AddCategory("Mobile phone");
            Categories.AddCategory("Transportation");
            Categories.AddCategory("Taxi");
            Accounts.AddAccount("Visa", 500);
            Accounts.AddAccount("MasterCard", 300);
            Accounts.AddAccount("BolCart", 200);
            TransactionServicecs.Transfer("Visa", "Car", 200);
            TransactionServicecs.Transfer("Visa", "Clothes", 50);
            TransactionServicecs.Transfer("BolCart", "Taxi", 50);
            TransactionServicecs.Transfer("MasterCard", "Taxi", 100);
        }

        private static int RunFirstMenu()
        {
            int select = 0;
            while (select == 0)
            {
                PrintFrontScreen();
                Console.WriteLine(Literals.getLiterals("1. Increase balance"));
                Console.WriteLine(Literals.getLiterals("2. Decrease balance"));
                Console.WriteLine(Literals.getLiterals("3. Options"));
                Console.WriteLine(Literals.getLiterals("4. Exit"));
                try
                {
                    select = Int32.Parse(Console.ReadKey(true).KeyChar.ToString());
                    if (select < 1 || select > 4)
                        throw new Exception();
                }
                catch (Exception)
                {
                    select = 0;
                }
                Console.Clear();
            }
            return select;
        }

        private void ChangeBalance(int a)
        {
            int amount = 0;
            bool exists = false;
            string input = "";
            while (amount <= 0 || exists == false)
            {
                Accounts.Show();
                Console.WriteLine(Literals.getLiterals("Input the name of account or it's identity describer , or 123 to exit"));
                input = Console.ReadLine();
                if (input.CompareTo("123") == 0)
                {
                    Console.Clear();
                    break;
                }
                exists = Accounts.AccountExists(input);
                if (exists)
                {
                    Console.WriteLine(Literals.getLiterals("Input amount"));
                    try
                    {
                        amount = Int32.Parse(Console.ReadLine().ToString());
                        if (amount <= 0)
                            throw new Exception();
                        if (a == 1)
                        {
                            Accounts.IncreaseAccountAmount(input, amount);

                        }
                        else if (a == -1)
                        {
                            AccountNameId = input;
                            Amount = amount;
                        }
                    }
                    catch (Exception)
                    {
                        amount = 0;
                    }
                }
                else
                {
                    Console.WriteLine(Literals.getLiterals("Account does not exist"));
                    Console.ReadKey(true);
                }
                Console.Clear();
            }
        }


        private void IncreaseBalanceMenu()
        {
            ChangeBalance(1);
        }

        private void DecreaseBalanceMenu()
        {
            ChangeBalance(-1);
            if (String.IsNullOrEmpty(AccountNameId))
                return;
            string categoryNameId = "";
            while (!Categories.CategoryExists(categoryNameId))
            {
                if (!Accounts.EnoughFunds(AccountNameId, Amount))
                {
                    Console.WriteLine(Literals.getLiterals("Not enough funds !"));
                    Console.ReadKey(true);
                    return;
                }
                categories.Show();
                Console.WriteLine(Literals.getLiterals("Input the name of category or it's identity describer , or 123 to exit"));
                categoryNameId = Console.ReadLine();
                if (categoryNameId.CompareTo("123") == 0)
                    break;
                if (Categories.CategoryExists(categoryNameId))
                {
                    TransactionServicecs.Transfer(AccountNameId,categoryNameId, Amount);
                }
                else
                {
                    Console.WriteLine(Literals.getLiterals("Category does not exist"));
                    Console.ReadKey(true);
                }
                Console.Clear();
            }
        }


        private void Options_Menu()
        {
            optionsMenu.Run();
        }

        public static void PrintFrontScreen()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(Literals.getLiterals("Currency")+ " - " + CurrencyModule.Currency.ToUpper() + "   " + Literals.getLiterals("Total balance") + " - " + Accounts.OverAllBalance.ToString("#.##") + "   ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(Literals.getLiterals("Total expences") + " - " + Categories.TotalExpense.ToString("#.##"));
            Console.ForegroundColor = ConsoleColor.Green;
            categories.Show();
        }

    }
}
