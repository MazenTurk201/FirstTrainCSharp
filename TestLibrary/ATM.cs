using System;

namespace TestLibrary
{
    public static class ATM
    {
        private static User? _currentUser { get; set; }

        public static User? CurrentUser
        {
            get { return _currentUser; }
        }

        public static bool isLoggedIn()
        {
            return _currentUser != null;
        }

        private static void Warrning()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("No user is currently logged in.");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void login(User user)
        {
            _currentUser = DB.FindUserAndPasswordInList(user.userName, user.password);
            if (_currentUser != null)
            {
                Console.Write($"User {_currentUser!.userName} logged in ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("successfully.");
                Console.ForegroundColor = ConsoleColor.White;
            } else {
                Console.WriteLine("User Not Found!!");
            }
        }
        public static void logout()
        {
            if (_currentUser != null)
            {
            Console.Write("logout from user: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{_currentUser.userName}");
            Console.ForegroundColor = ConsoleColor.White;
            _currentUser = null;
            } else {
                Warrning();
            }
        }
        public static void Withdraw(double amount)
        {
            if (_currentUser != null)
            {
                DB.Withdraw(_currentUser, amount);
                Console.Write($"Withdraw from {_currentUser.userName}: ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{amount}$");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Warrning();
            }
        }

        public static void Transfer(User toUserName, double amount)
        {
            if (_currentUser != null)
            {
                if (_currentUser.amount >= amount)
                {
                    DB.setAmount(_currentUser, toUserName, _currentUser.amount - amount);
                    Console.Write($"Transfer from {_currentUser.userName} to {toUserName.userName}: ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{amount}$");
                    Console.ForegroundColor = ConsoleColor.White;
                } else {
                    throw new InvalidOperationException("Insufficient funds for transfer.");
                }
            }
            else
            {
                Warrning();
            }
        }
        public static void Deposit(double amount)
        {
            if (_currentUser != null)
            {
                DB.setAmount(_currentUser, amount);
                Console.Write($"Deposit to {_currentUser.userName}: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{amount}$");
                Console.ForegroundColor = ConsoleColor.White;
            } else {
                Warrning();
            }
        }
        public static double ShowAmount()
        {
            if (_currentUser != null)
            {
                Console.Write($"The Amount of {_currentUser.userName} is: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(_currentUser.amount + "$");
                Console.ForegroundColor = ConsoleColor.White;
                return DB.getAmount(_currentUser);
            }
            else
            {
                Warrning();
                return 0;
            }
        }
    }

}
