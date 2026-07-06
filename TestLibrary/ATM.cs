using System;

namespace TestLibrary
{
    public static class ATM
    {
        private static string _userName="", _password="";
        public static void login(string userName, string password)
        {
            DB.IsUser(userName, password);
            _userName = userName;
            _password = password;
            Console.Write($"User {_userName} logged in ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("successfully.");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void logout()
        {
            Console.Write("logout from user: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{_userName}");
            Console.ForegroundColor = ConsoleColor.White;
            _userName = "";
            _password = "";
        }
        public static void Withdraw(double amount)
        {
            DB.Withdraw(_userName, _password, amount);
            Console.Write($"Withdraw from {_userName}: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{amount}$");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Transfer(string toUserName, double amount)
        {
            double currentAmount = DB.getAmount(_userName, _password);
            DB.IsUser(toUserName, _password);
            if (currentAmount >= amount)
            {
                DB.setAmount(_userName, _password, toUserName, currentAmount - amount);
                Console.Write($"Transfer from {_userName} to {toUserName}: ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{amount}$");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                throw new InvalidOperationException("Insufficient funds for transfer.");
            }
        }
        public static void Deposit(double amount)
        {
            DB.setAmount(_userName, _password, amount);
            Console.Write($"Deposit to {_userName}: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{amount}$");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static double ShowAmount()
        {
            Console.Write($"The Amount of {_userName} is: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(DB.getAmount(_userName, _password) + "$");
            Console.ForegroundColor = ConsoleColor.White;
            return DB.getAmount(_userName, _password);
        }
    }

}
