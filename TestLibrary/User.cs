namespace TestLibrary
{
    public class User
    {
        public string userName { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public double amount { get; set; } = 0.0;

        public User(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
        }
        public User(string userName) => this.userName = userName;

        public static void CreateAccount(string userName, string password)
        {
            DB.AddUser(new User(userName, password));
        }

        public override string ToString()
        {
            return $"User:{userName},Amount:{amount},Password:{password}";
        }

        public static List<User> GetUsers()
        {
            return DB.GetUsers();
        }
    }
}
