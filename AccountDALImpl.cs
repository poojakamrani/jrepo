using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Assignment2
{
    class AccountDALImpl
    {
        SqlConnection con = null;
        SqlCommand cmd = null;


        internal SqlConnection GetConnection()
        {
            con = new SqlConnection(
                "Data Source = LAPTOP-GDR4HNTG; Initial Catalog = dbBank; Integrated Security = true");
            con.Open();
            return con;
        }


        internal void AddAnAccount()
        {
            double AccBalance;
            string AccPassword;

            con = GetConnection();
            try
            {
                Console.WriteLine("Enter the Balance");
                AccBalance = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Enter the Password");
                AccPassword = Console.ReadLine();

                Console.WriteLine("Enter 1 for Savings Account and 2 for Current Account");
                int AccountType = Convert.ToInt32(Console.ReadLine());
                switch (AccountType)
                {
                    case 1:
                        Console.WriteLine("Enter the Minimum Amount");
                        double MinBalance = Convert.ToDouble(Console.ReadLine());

                        cmd = new SqlCommand("spu_AddSavingsAccount", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@AccBalance", AccBalance);
                        cmd.Parameters.AddWithValue("@AccPassword", AccPassword);
                        cmd.Parameters.AddWithValue("@MinBalance", MinBalance);
                        int rows1 = cmd.ExecuteNonQuery();
                        Console.WriteLine($"Rows Affected {rows1}");
                        break;
                    case 2:
                        Console.WriteLine("Enter the Overdraft Limit Amount");
                        double OverdraftLimitAmmount = Convert.ToDouble(Console.ReadLine());

                        cmd = new SqlCommand("spu_AddCurrentAccount", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@AccBalance", AccBalance);
                        cmd.Parameters.AddWithValue("@AccPassword", AccPassword);
                        cmd.Parameters.AddWithValue("@OverdraftLimitAmmount", OverdraftLimitAmmount);
                        int rows2 = cmd.ExecuteNonQuery();
                        Console.WriteLine($"Rows Affected {rows2}");
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            finally
            {
                con.Close();
            }
        }

        internal void ViewAllAccount()
        {
            con = GetConnection();
            cmd = new SqlCommand("spu_DisplayAllAcounts", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = null;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Console.WriteLine($"{dr[0]} | {dr[1]} | {dr[2]} | {dr[3]} | {dr[4]}");
            }
        }

        internal void GetAccountDetailsAdmin()
        {
            con = GetConnection();

            Console.WriteLine("Enter the Account Number");
            double AccNumber = Convert.ToDouble(Console.ReadLine());
            cmd = new SqlCommand("spu_AccountDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AccNumber", AccNumber);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Console.WriteLine($"{dr[0]} | {dr[1]} | {dr[2]}");
                }

            }
            else
            {
                Console.WriteLine("Entered Account Number is Invalid");
            }
        }

        internal void GetAccountDetailsUser()
        {
            con = GetConnection();
            Console.WriteLine("Enter the Account Number");
            double AccNumber = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter Password");
            string AccPassword = Console.ReadLine();
            cmd = new SqlCommand("CheckAccPass", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AccNumber", AccNumber);
            cmd.Parameters.AddWithValue("@AccPassword", AccPassword);
            bool bit = (bool)cmd.ExecuteScalar();
            if (bit)
            {
                cmd = new SqlCommand("spu_AccountDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AccNumber", AccNumber);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Console.WriteLine($"{dr[0]} | {dr[1]} | {dr[2]}");
                    }

                }
                else
                {
                    Console.WriteLine("Entered Account Number is Invalid");
                }
            }
            else
            {
                Console.WriteLine("Incorrect AccNumber/Password");
            }

        }


        internal void DiplayBalance()
        {
            con = GetConnection();
            Console.WriteLine("Enter the Account Number");
            double AccNumber = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter Password");
            string AccPassword = Console.ReadLine();
            cmd = new SqlCommand("CheckAccPass", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AccNumber", AccNumber);
            cmd.Parameters.AddWithValue("@AccPassword", AccPassword);
            bool bit = (bool)cmd.ExecuteScalar();
            if (bit)
            {
                cmd = new SqlCommand("spu_DisplayAccountBalance", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AccNumber", AccNumber);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Console.WriteLine($"{dr[0]}");
                    }
                }
                else
                {
                    Console.WriteLine("Entered Account Number is Invalid");
                }
            }
            else
            {
                Console.WriteLine("Incorrect AccNumber/Password");
            }
        }

        internal void ChangePassword()
        {
            con = GetConnection();
            Console.WriteLine("Enter Account Number");
            double AccNumber = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Old Password");
            string OldPassword = Console.ReadLine();
            Console.WriteLine("Enter New Password");
            string NewPassword = Console.ReadLine();

            cmd = new SqlCommand("spu_ChangePassword", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AccNumber", AccNumber);
            cmd.Parameters.AddWithValue("@OldPassword", OldPassword);
            cmd.Parameters.AddWithValue("@NewPassword", NewPassword);
            int rowsCheck = cmd.ExecuteNonQuery();

            if (rowsCheck == -1)
            {
                Console.WriteLine("Either Incorrect Old Password or Incorrect Account Number");
            }
            else
            {
                Console.WriteLine($"Rows Affected {rowsCheck}");
            }
        }

        internal void Withdraw()
        {
            con = GetConnection();
            Console.WriteLine("Enter the Account Number");
            int AccNumber = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Password");
            string AccPassword = Console.ReadLine();
            cmd = new SqlCommand("CheckAccPass", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AccNumber", AccNumber);
            cmd.Parameters.AddWithValue("@AccPassword", AccPassword);
            bool bit = (bool)cmd.ExecuteScalar();
            if (bit)
            {
                Console.WriteLine("Enter Account Type");
                Console.WriteLine("1. Savings");
                Console.WriteLine("2. Current");
                int Type = Convert.ToInt32(Console.ReadLine());
                switch (Type)
                {
                    case 1:
                        cmd = new SqlCommand("RetrieveBalance", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@AccNumber", AccNumber);
                        cmd.Parameters.AddWithValue("@AccPassword", AccPassword);
                        double SAccBalance = Convert.ToDouble(cmd.ExecuteScalar());

                        cmd = new SqlCommand("RetrieveMinBalance", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@AccNumber", AccNumber);
                        double MinBalance = Convert.ToDouble(cmd.ExecuteScalar());

                        Console.WriteLine("Enter the Amount to withdraw");
                        double SAmount = Convert.ToDouble(Console.ReadLine());

                        SavingsAccount savingsAccount = new SavingsAccount(AccNumber, SAccBalance, AccPassword, MinBalance);
                        savingsAccount.Withdraw(SAmount);

                        cmd = new SqlCommand("UpdateAccBalance", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@AccNumber", AccNumber);
                        cmd.Parameters.AddWithValue("@AccBalance", savingsAccount.AccBalance);
                        int RowsS = cmd.ExecuteNonQuery();
                        Console.WriteLine($"Rows Affected {RowsS}");
                        break;
                    case 2:
                        cmd = new SqlCommand("RetrieveBalance", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@AccNumber", AccNumber);
                        double CAccBalance = Convert.ToDouble(cmd.ExecuteScalar());

                        cmd = new SqlCommand("RetrieveOverdraftLimitAmount", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@AccNumber", AccNumber);
                        double OverdraftLimitAmmount = Convert.ToDouble(cmd.ExecuteScalar()); ;

                        Console.WriteLine("Enter the Amount to withdraw");
                        double CAmount = Convert.ToDouble(Console.ReadLine());

                        CurrentAccount currentAccount = new CurrentAccount(AccNumber, CAccBalance, AccPassword, OverdraftLimitAmmount);
                        currentAccount.Withdraw(CAmount);

                        cmd = new SqlCommand("UpdateAccBalance", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@AccNumber", AccNumber);
                        cmd.Parameters.AddWithValue("@AccBalance", currentAccount.AccBalance);
                        int RowsCB = cmd.ExecuteNonQuery();
                        Console.WriteLine($"Rows Affected {RowsCB}");

                        cmd = new SqlCommand("UpdateOverdraftLimitAmount", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@AccNumber", AccNumber);
                        cmd.Parameters.AddWithValue("@AccBalance", currentAccount.OverdraftLimitAmount);
                        int RowsCO = cmd.ExecuteNonQuery();
                        Console.WriteLine($"Rows Affected {RowsCO}");

                        break;
                }
            }
            else
            {
                Console.WriteLine("Incorrect AccNumber/Password");
            }
        }
    }
}
