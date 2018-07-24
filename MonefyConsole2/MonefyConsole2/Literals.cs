using MonefyConsole;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace MonifyConsole
{
    class Literals
    {
        private static Literals literals;

        private static Dictionary<string, string> stringTrans;

        private Literals()
        {
            if (File.Exists("Literals.dat"))
                stringTrans = Load();
            else
            {
                stringTrans = new Dictionary<string, string>();
                stringTrans.Add("Account name or it's identity describer is not unique", "Account name or it's identity describer is not unique");
                stringTrans.Add("Category name or it's identity describer is not unique", "Category name or it's identity describer is not unique");
                stringTrans.Add("1. Increase balance", "1. Increase balance");
                stringTrans.Add("2. Decrease balance", "2. Decrease balance");
                stringTrans.Add("3. Options", "3. Options");
                stringTrans.Add("4. Exit", "4. Exit");
                stringTrans.Add("Input the name of account or it's identity describer , or 123 to exit", "Input the name of account or it's identity describer , or 123 to exit");
                stringTrans.Add("Input amount", "Input amount");
                stringTrans.Add("Account does not exist", "Account does not exist");
                stringTrans.Add("Input the name of category or it's identity describer , or 123 to exit", "Input the name of category or it's identity describer , or 123 to exit");
                stringTrans.Add("Category does not exist", "Category does not exist");
                stringTrans.Add("1. Change currency", "1. Change currency");
                stringTrans.Add("2. Change language", "2. Change language");
                stringTrans.Add("3. Export to file", "3. Export to file");
                stringTrans.Add("4. Add category", "4. Add category");
                stringTrans.Add("5. Remove category", "5. Remove category");
                stringTrans.Add("6. Add account", "6. Add account");
                stringTrans.Add("7. Remove account", "7. Remove account");
                stringTrans.Add("8. Transactions per month", "8. Transactions per month");
                stringTrans.Add("9. Back", "9. Back");
                
                stringTrans.Add("Account exists", "Account exists");
                stringTrans.Add("Category exists", "Category exists");

                stringTrans.Add("Total balance", "Total balance");
                stringTrans.Add("Total expences", "Total expences");
                stringTrans.Add("List of accounts and their balances", "List of accounts and their balances");
                stringTrans.Add("List of categories and their expences", "List of categories and their expences");
                stringTrans.Add("Currency", "Currency");


                stringTrans.Add("Invalid language code or lost internet connection", "Invalid language code or lost internet connection");
                stringTrans.Add("Input the language code or 123 to exit", "Input the language code or 123 to exit");
                stringTrans.Add("Input the currency code or 123 to exit", "Input the currency code or 123 to exit");
                stringTrans.Add("Invalid currency code", "Invalid currency code");
                stringTrans.Add("Invalid currency code pattern", "Invalid currency code pattern");
                stringTrans.Add("Customer name is not unique", "Customer name is not unique");
                stringTrans.Add("Invalid category pattern", "Invalid customer pattern");
                stringTrans.Add("Invalid language code pattern", "Invalid language code pattern");

                stringTrans.Add("Invalid account pattern", "Invalid account pattern");

                
                stringTrans.Add("Input month", "Input month");
                stringTrans.Add("Input year", "Input year");
                stringTrans.Add("Nothing to show for this month : ", "Nothing to show for this month : ");

                stringTrans.Add("Only numeric input !", "Only numeric input !");

                stringTrans.Add("Account :", "Account :");
                stringTrans.Add("Category :", "Category :");
                stringTrans.Add("Amount :", "Currency :");
                stringTrans.Add("Date :", "Date :");
                stringTrans.Add("Currency :", "Currency :");

                stringTrans.Add("Not enough funds !", "Not enough funds !");
                stringTrans.Add("Expence amount :", "Expence amount :");
                
            }
        }

        public static void Save()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("Literals.dat", FileMode.OpenOrCreate))
                binaryFormatter.Serialize(fs, stringTrans);
        }

        public static Dictionary<string, string> Load()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("Literals.dat", FileMode.OpenOrCreate))
                return (Dictionary<string, string>)binaryFormatter.Deserialize(fs);
        }

        public static string getLiterals(string key)
        {
            return String.Copy(stringTrans[key]);
        }
        public static Literals getInstance()
        {
            return literals == null ? literals = new Literals() : literals;
            
        }

        public static void TranslateMenu(string langCode)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (langCode.Equals("en"))
            {
                foreach (KeyValuePair<string, string> KeyValuePair in stringTrans)
                    dict.Add(KeyValuePair.Key, KeyValuePair.Key);
            }
            else
            {
                foreach (KeyValuePair<string, string> KeyValuePair in stringTrans)
                {
                    dict.Add(KeyValuePair.Key, TranslationService.Translate("en", langCode, KeyValuePair.Value));
                }
            }
            stringTrans = dict;
            Save();
        }        
    }
}
                                                                                                   