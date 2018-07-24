using MonifyConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleFrontEnd
{
    class OptionsMenu
    {
        private static Accounts accounts;
        private static Categories categories;
        private static OptionsMenu optionsMenu;
        private OptionsMenu(Accounts totalBalanceA, Categories categoriesA)
        {
            accounts = totalBalanceA;
            categories = categoriesA;
        }

        public static OptionsMenu GetInstance(Accounts totalBalance, Categories categories)
        {
            return optionsMenu == null ? optionsMenu = new OptionsMenu(totalBalance, categories) : optionsMenu;
        }

        public  void Run()
        {
            while (true)
            {
                int select = RunFirstMenu();
                if (select == 1)
                    ChangeCurrency();
                else if (select == 2)
                    ChangeLanguage();
                else if (select == 3)
                    ExportTofile();
                else if (select == 4)
                    AddCategory();
                else if (select == 5)
                    RemoveCategory();
                else if (select == 6)
                    AddAccount();
                else if (select == 7)
                    RemoveAccount();
                else if (select == 8)
                    TransactionsPerMonth();
                else if (select == 9)
                    break;
            }
        }
        private static int RunFirstMenu()
        {
            int select = 0;
            while (select == 0)
            {
                Console.WriteLine(Literals.getLiterals("1. Change currency"));
                Console.WriteLine(Literals.getLiterals("2. Change language"));
                Console.WriteLine(Literals.getLiterals("3. Export to file"));
                Console.WriteLine(Literals.getLiterals("4. Add category"));
                Console.WriteLine(Literals.getLiterals("5. Remove category"));
                Console.WriteLine(Literals.getLiterals("6. Add account"));
                Console.WriteLine(Literals.getLiterals("7. Remove account"));
                Console.WriteLine(Literals.getLiterals("8. Transactions per month"));
                Console.WriteLine(Literals.getLiterals("9. Back"));
                try
                {
                    select = Int32.Parse(Console.ReadKey(true).KeyChar.ToString());
                    if (select < 1 || select > 9)
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

        private static void ChangeCurrency()
        {
            string input = "";
            string pattern = "^[a-z]{3}$";
            while(true)
            {
                Console.WriteLine(Literals.getLiterals("Input the currency code or 123 to exit"));
                input = Console.ReadLine();
                if (input.Equals("123")) { Console.Clear(); return; }

                if(Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase))
                {
                    //try
                    //{
                        CurrencyModule.ChangeCurrency(input);
                        Console.Clear();
                        break;
                    //}
                   // catch(Exception e)
                    //{
                        Console.WriteLine(Literals.getLiterals("Invalid currency code"));
                        Console.ReadKey(true);
                    //}
                }
                else
                {
                    Console.WriteLine(Literals.getLiterals("Invalid currency code pattern"));
                    Console.ReadKey(true);
                }
                Console.Clear();
            }
        }

        private static void ChangeLanguage()
        {
            Console.WriteLine(Literals.getLiterals("Input the language code or 123 to exit"));
            string pattern = "^[a-z]{2}$";
            try
            {
                string str = Console.ReadLine();
                if (str.Equals("123")){Console.Clear(); return;}
                if (Regex.IsMatch(str, pattern, RegexOptions.IgnoreCase))
                    Literals.TranslateMenu(str);
                else
                {
                    Console.WriteLine(Literals.getLiterals("Invalid language code pattern"));
                    Console.ReadKey(true);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(Literals.getLiterals("Invalid language code or lost internet connection"));
                Console.ReadKey(true);
            }
            Console.Clear();
        }

        private static void ExportTofile()
        {
            Payments.ExportToFile();
        }

        private void AddCategory()
        {
            string pattern = "^[A-Za-z0-9]{3,20}$";
            while (true)
            {
                categories.Show();
                Console.WriteLine(Literals.getLiterals("Input the name of category or it's identity describer , or 123 to exit"));
                string idName = Console.ReadLine();
                if (idName.CompareTo("123") == 0) { Console.Clear(); break; }


                if (Regex.IsMatch(idName, pattern, RegexOptions.IgnoreCase))
                {
                    if (!Categories.CategoryExists(idName))
                    {
                        Categories.AddCategory(idName);
                        Console.Clear();
                        break;
                    }
                    else
                    {
                        Console.WriteLine(Literals.getLiterals("Category exists"));
                        Console.ReadKey(true);
                    }
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine(Literals.getLiterals("Invalid category pattern"));
                    Console.ReadKey(true);
                }
            }
        }
        private void RemoveCategory()
        {
            string pattern = "^[A-Za-z0-9]{3,20}$";
            while (true)
            {
                categories.Show();
                Console.WriteLine(Literals.getLiterals("Input the name of category or it's identity describer , or 123 to exit"));
                string idName = Console.ReadLine();
                if (idName.Equals("123")) { Console.Clear(); break; }


                if (Regex.IsMatch(idName, pattern, RegexOptions.IgnoreCase))
                {
                    if (Categories.CategoryExists(idName))
                    {
                        Categories.RemoveCategory(idName);
                        Console.Clear();
                        break;
                    }
                    else
                    {
                        Console.WriteLine(Literals.getLiterals("Category does not exists"));
                        Console.ReadKey(true);
                    }
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine(Literals.getLiterals("Invalid category pattern"));
                    Console.ReadKey(true);
                }
            }
        }

        private void AddAccount(){
            string pattern = "^[A-Za-z0-9]{3,20}$";
            while (true)
            {
                Accounts.Show();
                Console.WriteLine(Literals.getLiterals("Input the name of account or it's identity describer , or 123 to exit"));
                string idName = Console.ReadLine();
                if (idName.Equals("123")) { Console.Clear(); break; }
                int amount = 0;
                if (Regex.IsMatch(idName, pattern, RegexOptions.IgnoreCase))
                {
                    if (!Accounts.AccountExists(idName))
                    {
                        try
                        {
                            Console.WriteLine(Literals.getLiterals("Input amount"));
                            amount = Int32.Parse(Console.ReadLine().ToString());
                            if (amount <= 0)
                                throw new Exception();
                            Accounts.AddAccount(idName, amount);
                            Console.Clear();
                            break;
                        }
                        catch (Exception)
                        {
                            amount = 0;
                        }

                    }
                    else
                    {
                        Console.WriteLine(Literals.getLiterals("Account exists"));
                        Console.ReadKey(true);
                    }
                }
                else
                {
                    Console.WriteLine(Literals.getLiterals("Invalid account pattern"));
                    Console.ReadKey(true);
                }
                Console.Clear();
            }
        }

        private void RemoveAccount()
        {
            string pattern = "^[A-Za-z0-9]{3,20}$";
            while (true)
            {
                Accounts.Show();
                Console.WriteLine(Literals.getLiterals("Input the name of account or it's identity describer , or 123 to exit"));
                string idName = Console.ReadLine();
                int amount = 0;
                if (idName.Equals("123")) { Console.Clear(); break; }

                if (Regex.IsMatch(idName, pattern, RegexOptions.IgnoreCase))
                {
                    if (Accounts.AccountExists(idName))
                    {
                        try
                        {
                            Accounts.RemoveAccount(idName);
                            Console.Clear();
                            break;
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
                }
                else
                {
                    Console.WriteLine(Literals.getLiterals("Invalid account pattern"));
                    Console.ReadKey(true);                 
                }
                Console.Clear();
            }
        }

        private void TransactionsPerMonth()
        {
            while (true)
            {
                try
                {

                    Console.WriteLine(Literals.getLiterals("Input month"));
                    int month = Int32.Parse(Console.ReadLine());
                    Console.WriteLine(Literals.getLiterals("Input year"));
                    int year = Int32.Parse(Console.ReadLine());
                    LinkedList<Payment> linkedList = Payments.TransactionsPerMonth(month, year);
                    if (linkedList == null)
                    {
                        Console.WriteLine(Literals.getLiterals("Nothing to show for this month : ") + month + "." + year);
                        Console.ReadKey(true);
                        Console.Clear();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("---------------------------------------------------------------");
                        foreach (Payment pay in linkedList)
                        {
                            Console.Write( Literals.getLiterals("Account :") +" "+ pay.AccountNameId+" | ");
                            Console.Write( Literals.getLiterals("Category :") +" "+ pay.CategoryNameId+" | ");
                            Console.Write(Literals.getLiterals("Expence amount :") + " " + pay.Amount.ToString("#.##") + " | ");
                            Console.Write( Literals.getLiterals("Currency :") + " " + CurrencyModule.Currency.ToUpper()+ " | ");
                            Console.WriteLine( Literals.getLiterals("Date :") + " " + pay.DateTime.ToString("yyyy.MM.dd.HH.mm.ss"));
                        }
                        Console.WriteLine("---------------------------------------------------------------");
                        Console.ReadKey(true);
                        Console.Clear();
                        break;
                    }
                }           
                catch (Exception)
                {
                    Console.WriteLine(Literals.getLiterals("Only numeric input !"));
                    Console.ReadKey(true);
                }
                Console.Clear();
            }
        }

    }
    
}
