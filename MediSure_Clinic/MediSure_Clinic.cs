using System;

namespace MediSure_Clinic
{
    class PatientBill
    {
        public string BillId;
        public string PatientName;
        public bool HasInsurance;

        public decimal ConsultationFee;
        public decimal LabCharges;
        public decimal MedicineCharges;

        public decimal GrossAmount;
        public decimal DiscountAmount;
        public decimal FinalPayable;
    }

    class BillingService
    {
        public static PatientBill LastBill;
        public static bool HasLastBill = false;

        public static void CreateNewBill()
        {
            PatientBill bill = new PatientBill();

            Console.Write("Enter Bill Id: ");
            bill.BillId = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(bill.BillId))
            {
                Console.WriteLine("Bill Id cannot be empty.");
                return;
            }

            Console.Write("Enter Patient Name: ");
            bill.PatientName = Console.ReadLine();

            Console.Write("Is the patient insured? (Y/N): ");
            string insuranceInput = Console.ReadLine();

            if (insuranceInput == "Y" || insuranceInput == "y")
                bill.HasInsurance = true;
            else
                bill.HasInsurance = false;

            Console.Write("Enter Consultation Fee: ");
            if (!decimal.TryParse(Console.ReadLine(), out bill.ConsultationFee) || bill.ConsultationFee <= 0)
            {
                Console.WriteLine("Consultation fee must be greater than 0.");
                return;
            }

            Console.Write("Enter Lab Charges: ");
            if (!decimal.TryParse(Console.ReadLine(), out bill.LabCharges) || bill.LabCharges < 0)
            {
                Console.WriteLine("Lab charges cannot be negative.");
                return;
            }

            Console.Write("Enter Medicine Charges: ");
            if (!decimal.TryParse(Console.ReadLine(), out bill.MedicineCharges) || bill.MedicineCharges < 0)
            {
                Console.WriteLine("Medicine charges cannot be negative.");
                return;
            }

            // Calculations
            bill.GrossAmount = bill.ConsultationFee + bill.LabCharges + bill.MedicineCharges;

            if (bill.HasInsurance)
                bill.DiscountAmount = bill.GrossAmount * 0.10m;
            else
                bill.DiscountAmount = 0;

            bill.FinalPayable = bill.GrossAmount - bill.DiscountAmount;

            LastBill = bill;
            HasLastBill = true;

            Console.WriteLine("\nBill created successfully.");
            Console.WriteLine("Gross Amount: " + bill.GrossAmount.ToString("0.00"));
            Console.WriteLine("Discount Amount: " + bill.DiscountAmount.ToString("0.00"));
            Console.WriteLine("Final Payable: " + bill.FinalPayable.ToString("0.00"));
        }

        public static void ViewLastBill()
        {
            if (!HasLastBill)
            {
                Console.WriteLine("No bill available. Please create a new bill first.");
                return;
            }

            Console.WriteLine("\n----------- Last Bill -----------");
            Console.WriteLine("BillId: " + LastBill.BillId);
            Console.WriteLine("Patient: " + LastBill.PatientName);
            Console.WriteLine("Insured: " + (LastBill.HasInsurance ? "Yes" : "No"));
            Console.WriteLine("Consultation Fee: " + LastBill.ConsultationFee.ToString("0.00"));
            Console.WriteLine("Lab Charges: " + LastBill.LabCharges.ToString("0.00"));
            Console.WriteLine("Medicine Charges: " + LastBill.MedicineCharges.ToString("0.00"));
            Console.WriteLine("Gross Amount: " + LastBill.GrossAmount.ToString("0.00"));
            Console.WriteLine("Discount Amount: " + LastBill.DiscountAmount.ToString("0.00"));
            Console.WriteLine("Final Payable: " + LastBill.FinalPayable.ToString("0.00"));
            Console.WriteLine("--------------------------------");
        }

        public static void ClearLastBill()
        {
            LastBill = null;
            HasLastBill = false;
            Console.WriteLine("Last bill cleared.");
        }
    }

    class Program
    {
        static void Main()
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n================== MediSure Clinic Billing ==================");
                Console.WriteLine("1. Create New Bill (Enter Patient Details)");
                Console.WriteLine("2. View Last Bill");
                Console.WriteLine("3. Clear Last Bill");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your option: ");

                string choice = Console.ReadLine();

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
}



