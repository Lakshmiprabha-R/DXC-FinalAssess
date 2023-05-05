// See https://aka.ms/new-console-template for more information


using System;
using System.Collections.Generic;
using System.IO;

namespace ExpenseTrackerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Transaction> transactions = new List<Transaction>();
            double availableBalance = ReadAvailableBalanceFromFile();

            while (true)
            {

                Console.WriteLine("   Expense Tracker App   ");

                Console.WriteLine("Please select an option from the menu:");
                Console.WriteLine("1. Add Transaction");
                Console.WriteLine("2. View Expenses");
                Console.WriteLine("3. View Income");
                Console.WriteLine("4. Check Available Balance");
                Console.Write("Enter your choice : ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Add Transaction");
                        Console.Write("Enter title: ");
                        string title = Console.ReadLine();
                        Console.Write("Enter description: ");
                        string description = Console.ReadLine();
                        Console.Write("Enter amount: ");
                        double amount = double.Parse(Console.ReadLine());
                        Console.Write("Enter date (dd-mm-yyy): ");
                        DateTime date = DateTime.Parse(Console.ReadLine());

                        Transaction transaction = new Transaction(title, description, amount, date);
                        transactions.Add(transaction);

                        if (amount < 0)
                        {
                            availableBalance -= Math.Abs(amount);
                        }
                        else
                        {
                            availableBalance += amount;
                        }

                        Console.WriteLine("Transaction added successfully.");
                        Console.WriteLine();
                        break;

                    case "2":
                        Console.WriteLine("Expense Transactions");
                        Console.WriteLine("--------------------");
                        foreach (Transaction t in transactions)
                        {
                            if (t.Amount < 0)
                            {
                                Console.WriteLine(t.ToString());
                            }
                        }
                        Console.WriteLine();
                        break;

                    case "3":
                        Console.WriteLine("Income Transactions");
                        Console.WriteLine("-------------------");
                        foreach (Transaction t in transactions)
                        {
                            if (t.Amount >= 0)
                            {
                                Console.WriteLine(t.ToString());
                            }
                        }
                        Console.WriteLine();
                        break;

                    case "4":
                        Console.WriteLine("Available Balance: " + availableBalance);
                        Console.WriteLine();
                        break;

                    default:
                        Console.WriteLine("Wrong Choice Entered");
                        Console.WriteLine();
                        break;
                }


                WriteAvailableBalanceToFile(availableBalance);
            }
        }

        static double ReadAvailableBalanceFromFile()
        {
            double availableBalance = 0.0;
            if (File.Exists("availableBalance.txt"))
            {
                string[] lines = File.ReadAllLines("availableBalance.txt");
                if (lines.Length > 0)
                {
                    double.TryParse(lines[0], out availableBalance);
                }
            }
            return availableBalance;
        }

        static void WriteAvailableBalanceToFile(double availableBalance)
        {
            File.WriteAllText("availableBalance.txt", availableBalance.ToString());
        }
    }
    class Transaction
    {
    
    
        public string Title { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }

        public Transaction(string title, string description, double amount, DateTime date)
        {
            Title = title;
            Description = description;
            Amount = amount;
            Date = date;
        }

        public override string ToString()
        {
            string type = Amount < 0 ? "Expense" : "Income";
            return $"{Date.ToString("dd-MM-yyy")} - {Title} - {Description}- {type} - {Amount}";
        }
        
    }
}
