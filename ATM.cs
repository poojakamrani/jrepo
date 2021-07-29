//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Assignment2
//{
//    interface ATM
//    {
//        public void Withdraw(int AccNumber, double Amount);
//        public void ChangePassword(int AccNumber,String OldPassword,String NewPassword);
//        public void CheckBalance();
//    }

//    class SBIATM : ATM
//    {
//        public void Withdraw(int AccNumber, double Amount)
//        {
//            Console.WriteLine("Enter the Account Number");
//            int AccNo = Convert.ToInt32(Console.ReadLine());
//            Console.WriteLine("Enter the amount to withdraw");
//            double Amt = Convert.ToDouble(Console.ReadLine());
//        }
//        public void ChangePassword(int AccNumber, String OldPassword, String NewPassword)
//        {

//        }
//        public void CheckBalance()
//        {

//        }
//    }

//    class ICICIATM : ATM
//    {
//        public void Withdraw(int AccNumber, double Amount);
//        public void ChangePassword(int AccNumber, String OldPassword, String NewPassword);
//        public void CheckBalance();
//    }
//}
