
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    abstract class Account
    {
        internal static string BankName = "HDFC";
        internal int AccNo { get; set; }
        internal double AccBalance { get; set; }
        internal string AccPassword { get; set; }

        internal Account(int AccNo, double AccBalance, string AccPassword)
        {
            this.AccNo = AccNo;
            this.AccBalance = AccBalance;
            this.AccPassword = AccPassword;
        }

        internal virtual void DisplayAccount()
        {
            Console.WriteLine($"Bank Name: {BankName} | Account No: {AccNo} | Account Balance: {AccBalance} | Account Password: {AccPassword}");
        }

        internal abstract void Withdraw(double Amount);
    }

    class SavingsAccount : Account
    {
        internal double MinBalanceAmount { get; set; }

        internal SavingsAccount(int AccNo, double AccBalance, string AccPassword, double MinBalanceAmount) : base(AccNo, AccBalance, AccPassword)
        {
            this.MinBalanceAmount = MinBalanceAmount;
        }

        internal override void DisplayAccount()
        {
            base.DisplayAccount();
            Console.WriteLine($"Minimum Balance Amount: {MinBalanceAmount}");
        }
        internal override void Withdraw(double Amount)
        {
            try
            {
                if (Amount <= 0)
                {
                    throw new InvalidAmountException("Invalid Amount");
                }
                else
                {
                    if (AccBalance - MinBalanceAmount - Amount < 0)
                    {
                        throw new InsufficientFundException("Insufficient Funds");
                    }
                    else
                    {
                        AccBalance = AccBalance - Amount;
                        Console.WriteLine($"Available Balance: {AccBalance}");
                    }
                }
            }
            catch (ApplicationException e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }

    class CurrentAccount : Account
    {
        internal double OverdraftLimitAmount { get; set; }

        internal CurrentAccount(int AccNo, double AccBalance, string AccPassword, double OverdraftLimitAmount) : base(AccNo, AccBalance, AccPassword)
        {
            this.OverdraftLimitAmount = OverdraftLimitAmount;
        }

        internal override void DisplayAccount()
        {
            base.DisplayAccount();
            Console.WriteLine($"Minimum Balance Amount: {OverdraftLimitAmount}");
        }

        internal override void Withdraw(double Amount)
        {
            try
            {
                if (Amount <= 0)
                {
                    throw new InvalidAmountException("Invalid Amount");
                }
                else
                {
                    if (AccBalance + OverdraftLimitAmount - Amount < 0)
                    {
                        throw new InsufficientFundException("Insufficient Funds");
                    }
                    else
                    {
                        if (Amount > AccBalance)
                        {
                            OverdraftLimitAmount -= (Amount - AccBalance);
                            AccBalance = 0;
                        }
                        else
                        {
                            AccBalance -= Amount;
                        }
                        Console.WriteLine($"Remaining balance : {AccBalance}, Overdraft Limit Amount: {OverdraftLimitAmount}");
                    }
                }
            }
            catch (ApplicationException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}