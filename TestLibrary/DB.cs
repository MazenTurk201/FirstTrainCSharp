using System;
using System.Collections.Generic;

namespace TestLibrary
{
    public static class DB
    {
        private static readonly List<string> _userName = new();
        private static readonly List<string> _password = new();
        private static readonly List<double> _amount = new();
        private const string _key = "12345678901234567890123456789012"; // 32 chars for AES-256

        public static void AddUser(string userName, string password, double Amount)
        {
            _userName.Add(userName);
            _password.Add(SimpleAesHelperBase.Encrypt(password, _key));
            _amount.Add(Amount);
        }

        public static double getAmount(string userName, string password)
        {
            IsUser(userName, password);
            int idx = _userName.IndexOf(userName);
            return _amount[idx];
        }
        public static void setAmount(string fromUserName, string fromPassword, string toUserName, double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Amount must be greater than zero.");
            }
            int fromIdx = _userName.IndexOf(fromUserName);
            int toIdx = _userName.IndexOf(toUserName);
            _amount[fromIdx] = amount;
            _amount[toIdx] += amount;
        }
        public static void setAmount(string userName, string password, double amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Amount cannot be negative.");
            }
            int idx = _userName.IndexOf(userName);
            _amount[idx] += amount;
        }
        public static void Withdraw(string userName, string password, double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Amount must be greater than zero.");
            }
            int idx = _userName.IndexOf(userName);
            if (_amount[idx] >= amount)
            {
                _amount[idx] -= amount;
            }
            else
            {
                throw new InvalidOperationException("Insufficient funds for withdrawal.");
            }
        }

        public static bool IsUser(string userName, string password)
        {
            for (int i = 0; i < _userName.Count; i++)
            {
                if (_userName[i] == userName && _password[i] == SimpleAesHelperBase.Encrypt(password, _key))
                {
                    return true;
                }
            }

            return false;
        }

        public static void LeakeData()
        {
            Console.WriteLine("All Users:");
            for (int i = 0; i < _userName.Count; i++)
            {
                Console.WriteLine($"User: {_userName[i]}, Amount: {_amount[i]}");
            }
        }
    }
}
