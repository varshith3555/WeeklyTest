using System;

namespace MediSure_Clinic
{
    #region Entity Class

    /// <summary>
    /// Represents all billing information of a patient.
    /// </summary>
    class PatientBill
    {
        public string BillId;               /// Unique Bill Identifier                        
        public string PatientName;          /// Name of the patient
        public bool HasInsurance;          /// Indicates whether the patient has insurance
        public decimal ConsultationFee;    /// Consultation fee  
        public decimal LabCharges;         /// Laboratory charges
        public decimal MedicineCharges;    /// Medicine charges
        public decimal GrossAmount;        /// Total amount before discount
        public decimal DiscountAmount;     /// Discount amount
        public decimal FinalPayable;       /// Final payable amount
    }

    #endregion


    #region Billing Service

    /// <summary>
    /// Handles all operations related to patient billing.
    /// </summary>
    class BillingService
    {
        public static PatientBill LastBill;    // Stores the most recent bill

        public static bool HasLastBill = false;    // Indicates whether any bill is available

        #region Create Bill

        /// <summary>
        /// Creates a new patient bill by collecting input, validating data, calculating totals, and saving the bill.
        /// </summary>
        public static void CreateNewBill()
        {

            PatientBill bill = new PatientBill();   //  Creates a new PatientBill object to store billing data.

            /// Reads and validates Bill Id.
            Console.Write("Enter Bill Id: ");
            bill.BillId = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(bill.BillId))
            {
                Console.WriteLine("Bill Id cannot be empty.");
                return;
            }

            /// Reads patient name.
            Console.Write("Enter Patient Name: ");
            bill.PatientName = Console.ReadLine();

            /// Reads insurance information.
            Console.Write("Is the patient insured? (Y/N): ");
            string insuranceInput = Console.ReadLine();

            bill.HasInsurance = (insuranceInput == "Y" || insuranceInput == "y");

            #region Charges Input & Validation

            /// <summary>
            /// Reads and validates consultation fee.
            /// </summary>
            Console.Write("Enter Consultation Fee: ");
            if (!decimal.TryParse(Console.ReadLine(), out bill.ConsultationFee) || bill.ConsultationFee <= 0)
            {
                Console.WriteLine("Consultation fee must be greater than 0.");
                return;
            }

            /// <summary>
            /// Reads and validates lab charges.
            /// </summary>
            Console.Write("Enter Lab Charges: ");
            if (!decimal.TryParse(Console.ReadLine(), out bill.LabCharges) || bill.LabCharges < 0)
            {
                Console.WriteLine("Lab charges cannot be negative.");
                return;
            }

            /// <summary>
            /// Reads and validates medicine charges.
            /// </summary>
            Console.Write("Enter Medicine Charges: ");
            if (!decimal.TryParse(Console.ReadLine(), out bill.MedicineCharges) || bill.MedicineCharges < 0)
            {
                Console.WriteLine("Medicine charges cannot be negative.");
                return;
            }

            #endregion


            #region Calculations

            bill.GrossAmount = bill.ConsultationFee + bill.LabCharges + bill.MedicineCharges; // Calculates gross amount.

            bill.DiscountAmount = bill.HasInsurance ? bill.GrossAmount * 0.10m : 0; // Calculates discount based on insurance.

            bill.FinalPayable = bill.GrossAmount - bill.DiscountAmount;  // Calculates final payable amount.

            #endregion


            #region Save & Display Result

            /// Stores the bill as the last generated bill.
            LastBill = bill;
            HasLastBill = true;

            /// Displays billing summary.
            Console.WriteLine("\nBill created successfully.");
            Console.WriteLine("Gross Amount: " + bill.GrossAmount.ToString("0.00"));
            Console.WriteLine("Discount Amount: " + bill.DiscountAmount.ToString("0.00"));
            Console.WriteLine("Final Payable: " + bill.FinalPayable.ToString("0.00"));

            #endregion
        }

        #endregion


        #region View Bill

        /// <summary>
        /// It will Displays the most recently created bill.
        /// </summary>
        public static void ViewLastBill()
        {
            if (!HasLastBill)
            {
                Console.WriteLine("No bill available. Please create a new bill first.");
                return;
            }

            Console.WriteLine("BillId: " + LastBill.BillId);
            Console.WriteLine("Patient: " + LastBill.PatientName);
            Console.WriteLine("Insured: " + (LastBill.HasInsurance ? "Yes" : "No"));
            Console.WriteLine("Consultation Fee: " + LastBill.ConsultationFee.ToString("0.00"));
            Console.WriteLine("Lab Charges: " + LastBill.LabCharges.ToString("0.00"));
            Console.WriteLine("Medicine Charges: " + LastBill.MedicineCharges.ToString("0.00"));
            Console.WriteLine("Gross Amount: " + LastBill.GrossAmount.ToString("0.00"));
            Console.WriteLine("Discount Amount: " + LastBill.DiscountAmount.ToString("0.00"));
            Console.WriteLine("Final Payable: " + LastBill.FinalPayable.ToString("0.00"));
        }

        #endregion


        #region Clear Bill

        /// <summary>
        /// Clears the stored last bill.
        /// </summary>
        public static void ClearLastBill()
        {
            LastBill = null;
            HasLastBill = false;
            Console.WriteLine("Last bill cleared.");
        }

        #endregion
    }

    #endregion


    #region Program

    /// <summary>
    /// Entry point of the MediSure Clinic Billing System.
    /// </summary>
    class Program
    {
        static void Main()
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("1. Create New Bill");
                Console.WriteLine("2. View Last Bill");
                Console.WriteLine("3. Clear Last Bill");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your option: ");

                string choice = Console.ReadLine();

                ///switch case
                switch (choice)
                {
                    case "1":
                        BillingService.CreateNewBill();
                        break;

                    case "2":
                        BillingService.ViewLastBill();
                        break;

                    case "3":
                        BillingService.ClearLastBill();
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