using TestLibrary;

namespace TestConsole
{
    public static class BankSystem
    {
        static async Task Main(string[] args)
        {
            //string userName = "user1", Password = "password1";

            //ATM.login(new User(userName: userName, password: Password));
            //ATM.ShowAmount();
            //ATM.Deposit(400);
            //ATM.Transfer(new User("user2"), 50);
            //ATM.Withdraw(amount: 100);
            //ATM.ShowAmount();
            //ATM.logout();
            //ATM.login(new User("user2", "password2"));
            //ATM.ShowAmount();
            //ATM.logout();
            //Console.WriteLine("\n\n\n");
            //DB.LeakeData();
            while (true)
            {
                Console.Clear();
                Banner();
                switch (Console.ReadLine())
                {
                    case "1":
                        if (ATM.isLoggedIn()) {
                            ATM.logout(); 
                        } else {
                            Console.Write("Enter UserName: ");
                            string userName = Console.ReadLine() ?? "";
                            Console.Write("Enter Password: ");
                            string password = Console.ReadLine() ?? "";
                            ATM.login(new User(userName, password));
                        }
                        break;
                    case "2":
                        Console.Write("Enter UserName: ");
                        string newUserName = Console.ReadLine() ?? "";
                        Console.Write("Enter Password: ");
                        string newPassword = Console.ReadLine() ?? "";
                        User.CreateAccount(newUserName, newPassword);
                        break;
                    case "3":
                        ATM.ShowAmount();
                        break;
                    case "4":
                        Console.Write("Enter Amount to Deposit: ");
                        double depositAmount = Convert.ToDouble(Console.ReadLine());
                        ATM.Deposit(depositAmount);
                        break;
                    case "5":
                        Console.Write("Enter Amount to Withdraw: ");
                        double withdrawAmount = Convert.ToDouble(Console.ReadLine());
                        ATM.Withdraw(withdrawAmount);
                        break;
                    case "6":
                        Console.Write("Enter Recipient UserName: ");
                        string recipientUserName = Console.ReadLine() ?? "";
                        Console.Write("Enter Amount to Transfer: ");
                        double transferAmount = Convert.ToDouble(Console.ReadLine());
                        ATM.Transfer(new User(recipientUserName), transferAmount);
                        break;
                    case "99":
                        DB.LeakeData();
                        await Task.Delay(3000);
                        break;
                    case "0":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        await Task.Delay(1000);
                        break;
                    }
                }
        }
        public static void Banner()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("""
                    ,---,.                               ,-.           .--.--.                         
                  ,'  .'  \                          ,--/ /|          /  /    '.                       
                ,---.' .' |                  ,---, ,--. :/ |         |  :  /`. /                       
                |   |  |: |              ,-+-. /  |:  : ' /          ;  |  |--`             .--.--.    
                :   :  :  /  ,--.--.    ,--.'|'   ||  '  /           |  :  ;_         .--, /  /    '   
                :   |    ;  /       \  |   |  ,"' |'  |  :            \  \    `.    /_ ./||  :  /`./   
                |   :     \.--.  .-. | |   | /  | ||  |   \            `----.   \, ' , ' :|  :  ;_     
                |   |   . | \__\/: . . |   | |  | |'  : |. \           __ \  \  /___/ \: | \  \    `.  
                '   :  '; | ," .--.; | |   | |  |/ |  | ' \ \         /  /`--'  /.  \  ' |  `----.   \ 
                |   |  | ; /  /  ,.  | |   | |--'  '  : |--'         '--'.     /  \  ;   : /  /`--'  / 
                |   :   / ;  :   .'   \|   |/      ;  |,'              `--'---'    \  \  ;'--'.     /  
                |   | ,'  |  ,     .-./'---'       '--'                             :  \  \ `--'---'   
                `----'     `--`---'                                                  \  ' ;            
                                                                                      `--`             
            """);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"""

                Welcome to the Bank System!
                {(ATM.isLoggedIn() ? $"Welcome back {ATM.CurrentUser!.userName.ToUpper()}" : "Hello Guest!")}
                1) {(ATM.isLoggedIn() ? "Logout" : "Login")}
                2) Create Account
                3) Show Amount
                4) Deposit
                5) Withdraw
                6) Transfer
                99) Leak Data (For Testing Purposes)
                0) Exit
                """);
            Console.Write("\nPlease select an option: ");
        }
    }
}
