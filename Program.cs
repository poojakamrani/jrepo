using System;

namespace CsharpAssignment
{
    class Program
    {
        static void Main(string[] args)
        {
            AccountDALImpl accountDALImpl = new AccountDALImpl();
            Console.WriteLine("Welcome To HDFC Bank");
            Console.WriteLine("1. Admin \n2. Customer");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Console.WriteLine("1. Add an Account");
                    Console.WriteLine("2. View All Accounts");
                    Console.WriteLine("3. Get Account Details");
                    int AdminChoice = Convert.ToInt32(Console.ReadLine());
                    switch (AdminChoice)
                    {
                        case 1:
                            accountDALImpl.AddAnAccount();
                            break;
                        case 2:
                            accountDALImpl.ViewAllAccount();
                            break;
                        case 3:
                            accountDALImpl.GetAccountDetailsAdmin();
                            break;
                    }
                    break;
                case 2:
                    Console.WriteLine("1. Withdraw");
                    Console.WriteLine("2. Check Balance");
                    Console.WriteLine("3. Change Password");
                    Console.WriteLine("4. Get Account Details");
                    int UserChoice = Convert.ToInt32(Console.ReadLine());
                    switch (UserChoice)
                    {
                        case 1:
                            accountDALImpl.Withdraw();
                            break;
                        case 2:
                            accountDALImpl.DiplayBalance();
                            break;
                        case 3:
                            accountDALImpl.ChangePassword();
                            break;
                        case 4:
                            accountDALImpl.GetAccountDetailsUser();
                            break;
                    }
                    break;
            }
        }
    }
}
