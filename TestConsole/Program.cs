using TestLibrary;

namespace TestConsole
{
    public static class BankSystem
    {
        static void Main(string[] args)
        {
            initSomeUsers();
            string userName = "user1", Password = "password1";

            ATM.login(userName, Password);
            ATM.ShowAmount();
            ATM.Deposit(400);
            ATM.Transfer("user2", 50);
            ATM.Withdraw(amount: 100);
            ATM.ShowAmount();
            ATM.logout();
            ATM.login("user2", "password2");
            ATM.ShowAmount();
            ATM.logout();
            Console.WriteLine("\n\n\n");
            DB.LeakeData();
        }

        static void initSomeUsers()
        {
            // Initialize an array of User objects
            User[] users = new User[3];
            string[] userNames = { "user1", "user2", "user3" };

            for (int i = 0; i < users.Length; i++)
            {
                users[i] = new User(userNames[i], "password" + (i + 1));
            }
        }
    }
}
