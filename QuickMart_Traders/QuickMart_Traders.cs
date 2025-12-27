using System;

namespace QuickMart_Traders
{
    /// <summary>
    /// This is entity class 
    /// </summary>
    class SaleTransaction
    {
        public string InvoiceNo;
        public string CustomerName;
        public string ItemName;
        public int Quantity;
        public decimal PurchaseAmount;
        public decimal SellingAmount;

        public string ProfitOrLossStatus; 
        public decimal ProfitOrLossAmount;
        public decimal ProfitMarginPercent;
    }

    /// <summary>
    /// This is TransactionService class
    /// </summary>
    class TransactionService
    {
        public static SaleTransaction LastTransaction;
        public static bool HasLastTransaction = false;

        public static void CreateTransaction()
        {
            /// creating object
            SaleTransaction t = new SaleTransaction();

            Console.Write("Enter Invoice No: ");
            t.InvoiceNo = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(t.InvoiceNo))
            {
                Console.WriteLine("Invoice number cannot be empty.");
                return;
            }

            Console.Write("Enter Customer Name: ");
            t.CustomerName = Console.ReadLine();

            Console.Write("Enter Item Name: ");
            t.ItemName = Console.ReadLine();

            Console.Write("Enter Quantity: ");
            if (!int.TryParse(Console.ReadLine(), out t.Quantity) || t.Quantity <= 0)
            {
                Console.WriteLine("Quantity must be greater than 0.");
                return;
            }

            Console.Write("Enter Purchase Amount (total): ");
            if (!decimal.TryParse(Console.ReadLine(), out t.PurchaseAmount) || t.PurchaseAmount <= 0)
            {
                Console.WriteLine("Purchase amount must be greater than 0.");
                return;
            }

            Console.Write("Enter Selling Amount (total): ");
            if (!decimal.TryParse(Console.ReadLine(), out t.SellingAmount) || t.SellingAmount < 0)
            {
                Console.WriteLine("Selling amount cannot be negative.");
                return;
            }

            Calculate(t);

            LastTransaction = t;
            HasLastTransaction = true;

            Console.WriteLine("\nTransaction saved successfully.");
            PrintResult(t);
            Console.WriteLine("------------------------------------------------------");
        }

        public static void ViewTransaction()
        {
            if (!HasLastTransaction)
            {
                Console.WriteLine("No transaction available. Please create a new transaction first.");
                return;
            }

            SaleTransaction t = LastTransaction;

            Console.WriteLine("\n-------------- Last Transaction --------------");
            Console.WriteLine("InvoiceNo: " + t.InvoiceNo);
            Console.WriteLine("Customer: " + t.CustomerName);
            Console.WriteLine("Item: " + t.ItemName);
            Console.WriteLine("Quantity: " + t.Quantity);
            Console.WriteLine("Purchase Amount: " + t.PurchaseAmount.ToString("0.00"));
            Console.WriteLine("Selling Amount: " + t.SellingAmount.ToString("0.00"));
            Console.WriteLine("Status: " + t.ProfitOrLossStatus);
            Console.WriteLine("Profit/Loss Amount: " + t.ProfitOrLossAmount.ToString("0.00"));
            Console.WriteLine("Profit Margin (%): " + t.ProfitMarginPercent.ToString("0.00"));
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("------------------------------------------------------");
        }

        /// <summary>
        /// Method To Recalculate 
        /// </summary>
        public static void Recalculate()
        {
            if (!HasLastTransaction)
            {
                Console.WriteLine("No transaction available. Please create a new transaction first.");
                return;
            }

            Calculate(LastTransaction);

            Console.WriteLine("\nRecalculation Completed:");
            PrintResult(LastTransaction);
            Console.WriteLine("------------------------------------------------------");
        }


        private static void Calculate(SaleTransaction t)
        {
            if (t.SellingAmount > t.PurchaseAmount)
            {
                t.ProfitOrLossStatus = "PROFIT";
                t.ProfitOrLossAmount = t.SellingAmount - t.PurchaseAmount;
            }
            else if (t.SellingAmount < t.PurchaseAmount)
            {
                t.ProfitOrLossStatus = "LOSS";
                t.ProfitOrLossAmount = t.PurchaseAmount - t.SellingAmount;
            }
            else
            {
                t.ProfitOrLossStatus = "BREAK-EVEN";
                t.ProfitOrLossAmount = 0;
            }

            t.ProfitMarginPercent = (t.ProfitOrLossAmount / t.PurchaseAmount) * 100;
        }

        private static void PrintResult(SaleTransaction t)
        {
            Console.WriteLine("Status: " + t.ProfitOrLossStatus);
            Console.WriteLine("Profit/Loss Amount: " + t.ProfitOrLossAmount.ToString("0.00"));
            Console.WriteLine("Profit Margin (%): " + t.ProfitMarginPercent.ToString("0.00"));
        }
    }

    class Program
    {
        static void Main()
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n================== QuickMart Traders ==================");
                Console.WriteLine("1. Create New Transaction (Enter Purchase & Selling Details)");
                Console.WriteLine("2. View Last Transaction");
                Console.WriteLine("3. Calculate Profit/Loss (Recompute & Print)");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        TransactionService.CreateTransaction();
                        break;

                    case "2":
                        TransactionService.ViewTransaction();
                        break;

                    case "3":
                        TransactionService.Recalculate();
                        break;

                    case "4":
                        Console.WriteLine("Thank you. Application closed normally.");
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
    }

}
