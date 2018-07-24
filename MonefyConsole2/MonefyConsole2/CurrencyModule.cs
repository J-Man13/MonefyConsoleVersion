using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MonifyConsole
{
    [Serializable]
    class CurrencyModule
    {
        private static string currency = InitializeCurrency();
        private static Dictionary<string,double> codeValute;

        public static string Currency
        {
            get
            {
                return String.Copy(currency);
            }
        }

        public static void SaveCurrency()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("InitializeCurrency.dat", FileMode.OpenOrCreate))
                binaryFormatter.Serialize(fs, currency);
        }

        public static string InitializeCurrency()
        {
            if (File.Exists("InitializeCurrency.dat"))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                using (FileStream fs = new FileStream("InitializeCurrency.dat", FileMode.OpenOrCreate))
                    return (String)binaryFormatter.Deserialize(fs);
            }
            else
            {
                return "AZN";
            }
        }

        public static void ChangeCurrency(string newCurrencyCode)
        {
            if (currency.Equals(newCurrencyCode))
                return;
            newCurrencyCode = newCurrencyCode.ToUpper();
            InitializeValute();

            if (newCurrencyCode.Equals("AZN"))
            {
                Accounts.Deindexify(codeValute[currency]);
                Categories.Deindexify(codeValute[currency]);
            }
            else
            {
                if (!currency.Equals("AZN"))
                {
                    Accounts.Deindexify(codeValute[currency]);
                    Categories.Deindexify(codeValute[currency]);
                }
                Accounts.Indexify(codeValute[newCurrencyCode]);
                Categories.Indexify(codeValute[newCurrencyCode]);
            }
            Accounts.Save();
            Categories.Save();
            currency = newCurrencyCode;
            SaveCurrency();
        }

        public static void InitializeValute() {
            if (codeValute == null)
                codeValute = new Dictionary<string, double>();
            else
                return;
            WebClient webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            string data = webClient.DownloadString("https://www.cbar.az/currencies/" + DateTime.Now.ToString("dd.MM.yyyy") + ".xml");
            XDocument xdoc = XDocument.Parse(data);
            List<XElement> ValTypes = xdoc.Descendants("ValType").ToList();
            List<XElement> Valutes = ValTypes[1].Descendants("Valute").ToList();

            foreach (XElement xElement in Valutes)
            {
                string name = xElement.Attribute("Code").Value;
                double index = Double.Parse(xElement.Descendants("Value").ToList()[0].Value);
                string nominal = xElement.Descendants("Nominal").ToList()[0].Value;
                if (!"1".Equals(nominal))
                    continue;
                codeValute.Add(name, index);
              
            }
        }




    }
}

