using System;

namespace QuickMart_Traders
{
    #region Entity Class

    /// <summary>
    /// Represents one sales transaction including purchase, selling and profit details.
    /// </summary>
    class SaleTransaction
    {
        public string InvoiceNo;            /// Unique invoice number
        public string CustomerName;         /// Name of the customer
        public string ItemName;             /// Name of the sold item
        public int Quantity;                /// Quantity of items sold
        public decimal PurchaseAmount;      /// Total purchase amount
        public decimal SellingAmount;       /// Total selling amount
        public string ProfitOrLossStatus;   /// Profit, Loss or Break-Even status
        public decimal ProfitOrLossAmount;  /// Profit or loss amount
        public decimal ProfitMarginPercent; /// Profit margin in percentage
    }

    #endregion


    #region Transaction Service

    /// <summary>
    /// Provides services for creating, viewing and recalculating sales transactions.
    /// </summary>
    class TransactionService
    {
        public static SaleTransaction LastTransaction;      // Stores the most recent transaction
        public static bool HasLastTransaction = false;      // Indicates if a transaction exists


        #region Create Transaction

        /// <summary>
        /// Creates a new sales transaction by collecting input and calculating results.
        /// </summary>
        public static void CreateTransaction()
        {
            SaleTransaction t = new SaleTransaction();   // Creates new transaction object

            /// Reads invoice number
            Console.Write("Enter Invoice No: ");
            t.InvoiceNo = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(t.InvoiceNo))
            {
                Console.WriteLine("Invoice number cannot be empty.");
                return;
            }

            /// Reads customer name
            Console.Write("Enter Customer Name: ");
            t.CustomerName = Console.ReadLine();

            /// Reads item name
            Console.Write("Enter Item Name: ");
            t.ItemName = Console.ReadLine();

            /// Reads and validates quantity
            Console.Write("Enter Quantity: ");
            if (!int.TryParse(Console.ReadLine(), out t.Quantity) || t.Quantity <= 0)
            {
                Console.WriteLine("Quantity must be greater than 0.");
                return;
            }

            /// Reads and validates purchase amount
            Console.Write("Enter Purchase Amount (total): ");
            if (!decimal.TryParse(Console.ReadLine(), out t.PurchaseAmount) || t.PurchaseAmount <= 0)
            {
                Console.WriteLine("Purchase amount must be greater than 0.");
                return;
            }

            /// Reads and validates selling amount
            Console.Write("Enter Selling Amount (total): ");
            if (!decimal.TryParse(Console.ReadLine(), out t.SellingAmount) || t.SellingAmount < 0)
            {
                Console.WriteLine("Selling amount cannot be negative.");
                return;
            }

            /// Calculates profit or loss
            Calculate(t);

            /// Stores last transaction
            LastTransaction = t;
            HasLastTransaction = true;

            /// Displays result
            Console.WriteLine("\nTransaction saved successfully.");
            PrintResult(t);
        }

        #endregion


        #region View Transaction

        /// <summary>
        /// Displays the most recent transaction.
        /// </summary>
        public static void ViewTransaction()
        {
            if (!HasLastTransaction)
            {
                Console.WriteLine("No transaction available. Please create a new transaction first.");
                return;
            }

            SaleTransaction t = LastTransaction;

            Console.WriteLine("InvoiceNo: " + t.InvoiceNo);
            Console.WriteLine("Customer: " + t.CustomerName);
            Console.WriteLine("Item: " + t.ItemName);
            Console.WriteLine("Quantity: " + t.Quantity);
            Console.WriteLine("Purchase Amount: " + t.PurchaseAmount.ToString("0.00"));
            Console.WriteLine("Selling Amount: " + t.SellingAmount.ToString("0.00"));
            Console.WriteLine("Status: " + t.ProfitOrLossStatus);
            Console.WriteLine("Profit/Loss Amount: " + t.ProfitOrLossAmount.ToString("0.00"));
            Console.WriteLine("Profit Margin (%): " + t.ProfitMarginPercent.ToString("0.00"));
        }

        #endregion


        #region Recalculate

        /// <summary>
        /// Recalculates profit/loss for the last transaction.
        /// </summary>
        public static void Recalculate()
        {
            if (!HasLastTransaction)
            {
                Console.WriteLine("No transaction available. Please create a new transaction first.");
                return;
            }

            /// Recomputes values
            Calculate(LastTransaction);

            Console.WriteLine("\nRecalculation Completed:");
            PrintResult(LastTransaction);
        }

        #endregion


        #region Core Calculation

        /// <summary>
        /// Performs profit/loss calculation.
        /// </summary>
        private static void Calculate(SaleTransaction t)
        {
            /// Determines profit or loss
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

            /// Calculates profit margin percentage
            t.ProfitMarginPercent = (t.ProfitOrLossAmount / t.PurchaseAmount) * 100;
        }

        #endregion


        #region Print Result

        /// <summary>
        /// Prints profit and margin details.
        /// </summary>
        private static void PrintResult(SaleTransaction t)
        {
            Console.WriteLine("Status: " + t.ProfitOrLossStatus);
            Console.WriteLine("Profit/Loss Amount: " + t.ProfitOrLossAmount.ToString("0.00"));
            Console.WriteLine("Profit Margin (%): " + t.ProfitMarginPercent.ToString("0.00"));
        }

        #endregion
    }

    #endregion


    #region Program

    /// <summary>
    /// Entry point for QuickMart Traders application.
    /// </summary>
    class Program
    {
        static void Main()
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("1. Create New Transaction");
                Console.WriteLine("2. View Last Transaction");
                Console.WriteLine("3. Calculate Profit/Loss");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your option: ");

                string choice = Console.ReadLine();

                /// switch case
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

    #endregion
}
