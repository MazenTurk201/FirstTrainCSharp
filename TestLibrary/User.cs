namespace TestLibrary
{
    public class User
    {
        public string userName, password;
        public double amount;

        public User(string userName, string password, double amount = 0.0)
        {
            this.userName = userName;
            this.password = password;
            this.amount = amount;

            DB.AddUser(userName, password, amount);
        }
    }
}
