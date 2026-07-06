using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;

namespace TestLibrary
{
    public static class DB
    {
        private static readonly List<User> _users = [
            new User("user1", "password1"),
            new User("user2", "password2"),
            new User("user3", "password3"),
            ];

        public static void AddUser(User user)
        {
            _users.Add(user);
        }

        public static double getAmount(User user)
        {
            User fromUser = FindUserInList(user.userName);
            return fromUser.amount;
        }
        public static void setAmount(User user, User toUserName, double amount)
        {
            User fromUser = FindUserInList(user.userName);
            User toUser = FindUserInList(toUserName.userName);
            if (amount <= 0 && fromUser.amount < amount)
            {
                throw new ArgumentException("Amount must be greater than zero.");
            }
            fromUser.amount = amount;
            toUser.amount += amount;
        }
        public static void setAmount(User user, double amount)
        {
            User fromUser = FindUserInList(user.userName);
            if (amount < 0)
            {
                throw new ArgumentException("Amount cannot be negative.");
            }
            fromUser.amount += amount;
        }
        public static void Withdraw(User user, double amount)
        {
            User fromUser = FindUserInList(user.userName);
            if (amount <= 0)
            {
                throw new ArgumentException("Amount must be greater than zero.");
            }
            if (fromUser.amount >= amount)
            {
                fromUser.amount -= amount;
            }
            else
            {
                throw new InvalidOperationException("Insufficient funds for withdrawal.");
            }
        }

        private static User FindUserInList(string userName)
        {
            User findUser = _users.Find(u => u.userName == userName)!;
            if (findUser != null)
            {
                return findUser;
            }
            else
            {
                throw new ArgumentException($"User with username '{userName}' not found.");
            }
        }
        public static User FindUserAndPasswordInList(string userName, string password)
        {
            User findUser = _users.Find(u => u.userName == userName && u.password == password)!;
            if (findUser != null)
            {
                return findUser;
            }
            else
            {
                throw new ArgumentException($"Invalid username or password for user '{userName}'.");
            }
        }
        public static void LeakeData()
        {
            Console.WriteLine("All Users:");
            var users = from user in User.GetUsers()
                        //where user.amount == 0
                        select new
                        {
                            AccountUser = user.userName,
                            AccountAmount = user.amount,
                            AccountPassword = user.password
                        };
            foreach (var item in users)
            {
                Console.WriteLine(item.ToString());
            }
        }

        public static List<User> GetUsers()
        {
            return _users;
        }
    }
}
