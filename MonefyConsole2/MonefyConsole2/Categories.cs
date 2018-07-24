using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace MonifyConsole
{
    [Serializable]
    class Categories
    {
        private static SortedSet<Category> categoriesList;
        private static Categories categories;
        private static double totalExpense;
        public static double TotalExpense
        {
            get
            {
                totalExpense = 0;
                foreach(Category c in categoriesList)
                {
                    totalExpense += c.Expense;
                }
                return totalExpense;
            }
        }

        public static Categories GetInstance()
        {
            return categories == null ? categories = new Categories() : categories;
        }

        public static void Save()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("Categories.dat", FileMode.OpenOrCreate))
                binaryFormatter.Serialize(fs, categoriesList);
        }

        public static SortedSet<Category> Load()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("Categories.dat", FileMode.OpenOrCreate))
                return (SortedSet<Category>)binaryFormatter.Deserialize(fs);
        }

        private Categories()
        {
            if (File.Exists("Categories.dat"))
                categoriesList = Load();
            else
                categoriesList = new SortedSet<Category>();
        }

        public static void AddCategory(string idName)
        {
            if (!CategoryExists(idName))
                categoriesList.Add(new Category(idName, 0));
            else
            {
                Console.WriteLine(Literals.getLiterals("Category name or it's identity describer is not unique"));
                Console.ReadKey(true);
            }
        }

        public static void RemoveCategory(string idName)
        {
            if (categoriesList.RemoveWhere(Match(idName)) > 0)
                Save();
            else
            {
                Console.WriteLine(Literals.getLiterals("Category does not exist"));
                Console.ReadKey(true);
            }
        }

        private static Predicate<Category> Match(string idName)
        {
            return delegate (Category category)
            {
                return category.NameId.Equals(idName);
            };
        }


        public static void IncreaseExpense(string idName,int amount)
        {
            Category categoryLnk = GetCategory(idName);
            categoryLnk.expenseIncrease(amount);
            Save();
        }
        
        private static Category GetCategory(string idName)
        {
            try
            {
                return (from c in categoriesList
                        where c.NameId.CompareTo(idName) == 0
                        select c).First();
            }
            catch(Exception )
            {
                return null;
            }
        }

        public static bool CategoryExists(string idName)
        {
            return GetCategory(idName) != null;
        }
        
        public void Show()
        {
                        
            if (categoriesList == null || categoriesList.Count == 0)
                return;

            Console.WriteLine();
            Console.WriteLine(Literals.getLiterals("List of categories and their expences"));
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine();
            foreach (Category c in categoriesList)
            {
                Console.Write(c.NameId);
                for (int i = 0; i < 21 - c.NameId.Length; i++)
                    Console.Write(" ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.BackgroundColor = ConsoleColor.Red;


                double customerPercentage = 0;
                if (totalExpense!=0)
                    customerPercentage = c.Expense * 100 / totalExpense;
                for (int i = 0; i < customerPercentage / 2 + 1; i++)
                {
                    Console.Write(" ");
                }
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(" ");
                if (customerPercentage == 0)
                    Console.WriteLine(c.Expense.ToString("#.##") + " " + customerPercentage + " %");
                else
                    Console.WriteLine(c.Expense.ToString("#.##") + " " + customerPercentage.ToString("#.##") + " %");
                Console.WriteLine();
            }
            Console.WriteLine("----------------------------------------------------------------------");
        }

        public static void Indexify(double index)
        {
            foreach (Category cat in categoriesList)
                 cat.Expense =  cat.Expense / index;
        }

        public static void Deindexify(double index)
        {
            foreach (Category cat in categoriesList)
                cat.Expense = cat.Expense * index;
        }

        public static bool CategoriesExist()
        {
            return categoriesList.Count != 0;
        }
        
    }
}
