using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace MonifyConsole
{
    class Payments
    {
        private static Payments payments;
        public static LinkedList<Payment> paymentsList;
        private static object categories;

        private Payments()
        {
            if (File.Exists("Payments.dat"))
                paymentsList = Load();
            else
                paymentsList = new LinkedList<Payment>();
        }

        class PaymentToExport{
            public string AccountName { get; set; }
            public string CategoryName { get; set; }
            public int Amount { get; set; }
            public string PayDate { get; set; }
        }

        public static Payments GetInstance()
        {
            return payments == null ? payments = new Payments() : payments;
        }



        public static void Save()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("Payments.dat", FileMode.OpenOrCreate))
                binaryFormatter.Serialize(fs,paymentsList);                
        }

        public static LinkedList<Payment> Load()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("Payments.dat", FileMode.OpenOrCreate))
                return (LinkedList<Payment>)binaryFormatter.Deserialize(fs);
        }

        public static void addPayment(string AccountNameId, string categoryNameId, int Amount)
        {
            paymentsList.AddLast(new Payment(AccountNameId, categoryNameId, Amount));
            Save();
        }

        public static void ExportToFile()
        {
            using (StreamWriter sw = new StreamWriter("payments.csv"))
            {
                CsvWriter writer = new CsvWriter(sw);
                writer.WriteHeader(typeof(PaymentToExport));
                writer.NextRecord();
                foreach (Payment p in paymentsList)
                {
                    PaymentToExport paymentToExport = new PaymentToExport();
                    paymentToExport.AccountName = p.AccountNameId;
                    paymentToExport.CategoryName = p.CategoryNameId;
                    paymentToExport.Amount = p.Amount;
                    paymentToExport.PayDate = p.DateTime.ToString("yyyy.MM.dd.HH.mm.ss");
                    writer.WriteRecord(paymentToExport);
                    writer.NextRecord();
                }
            }
        }

        public static LinkedList<Payment> TransactionsPerMonth(int month, int year)
        {
            LinkedList<Payment> linkedList = GetPayments(month, year);
            if (linkedList == null) 
                return null;
            return linkedList;
        }

        private static LinkedList<Payment> GetPayments(int month, int year)
        {
            try
            {
                LinkedList<Payment> tempLinked = new LinkedList<Payment>();
                List<Payment> tempList = (from payments in paymentsList
                        where (payments.DateTime.Month == month 
                        && payments.DateTime.Year == year)
                        select payments).Cast<Payment>().ToList();
                tempList.ForEach((i) => tempLinked.AddLast(i));
                if (tempLinked.Count == 0)
                    return null;
                else
                    return tempLinked;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
