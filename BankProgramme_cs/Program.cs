using System;
using System.Collections.Generic;

namespace BankApp
{
    
    class Program
    {
        
        static void Main(string[] args)
        {
            
            var Bank = new Bank();
            
            bool isRunning = true;
            
            int selectedOne = 0;

           
            string[] menuOptions = {
                "|Create Account",
                "|Deposit Money",
                "|Withdraw Money",
                "|Check Balance",
                "|View Account Details",
                "|Delete Account",
                "|Exit"
            };

            
            while (isRunning)
            {
               
                Console.Clear();
                Console.WriteLine("**Welcome to the Banking Application!**");
                Console.WriteLine("       (Press Enter to select.)\n");

                 //  Display the menu
                
                for (int i = 0; i < menuOptions.Length; i++)
                {
                    if (i == selectedOne)
                    {
                       
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(" * " + menuOptions[i]);
                        Console.ResetColor();

                    }
                    else
                    {
                        
                        Console.WriteLine("   " + menuOptions[i]);

                    }
                }

                // menu navigation
                var key = Console.ReadKey(true).Key;
               
                if (key == ConsoleKey.UpArrow)
                {
                   
                    selectedOne = (selectedOne == 0) ? menuOptions.Length - 1 : selectedOne - 1;

                }
                else if (key == ConsoleKey.DownArrow)
                {
                   
                    selectedOne = (selectedOne + 1) % menuOptions.Length;

                }
                else if (key == ConsoleKey.Enter)
                {
                   
                    Console.Clear(); // Clear menu before showing the selected option
                    
                    
                   
                    switch (selectedOne)
                    {
                        case 0:
                            Bank.CreateAccount();
                            break;
                        case 1:
                            Bank.Deposit();
                            break;
                        case 2:
                            Bank.Withdraw();
                            break;
                        case 3:
                            Bank.CheckBalance();
                            break;
                        case 4:
                            Bank.ViewAccountDetails();
                            break;
                        case 5:
                            Bank.DeleteAccount();
                            break;
                        case 6:
                            isRunning = false;
                            Console.WriteLine("Thank you for using the Banking Application!");
                            break;


                    }

                    
                    if (isRunning) // Step before showing the menu again
                    {
                        
                        Console.WriteLine("\nPress any key to return to the main menu...");
                        Console.ReadKey();


                   
                    }

                }
            }
        }
    }

   
    public class Bank
    {
        
        
        private readonly Dictionary<int, Account> accounts = new Dictionary<int, Account>();
        
        private int newAccountNum = 1;

       
        
          
        public void CreateAccount()
        {
           
            Console.Write("Enter account holder's name: ");
            string holderName = Console.ReadLine();

            Console.Write("Enter a password for this account: ");
            string password = Console.ReadLine();

            Account account = new Account(newAccountNum++, holderName, password);
            accounts.Add(account.AccountNumber, account);

            Console.WriteLine($"Account created successfully! Account Number: {account.AccountNumber}");


        }

        
        
        public void Deposit()
        {
           
            var account = GetWithPassword();
            if (account == null) return;





            Console.Write("Enter amount to deposit: ");
           
            if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
            {
               
                account.Deposit(amount);
                Console.WriteLine($"Deposit successful! New Balance: {account.Balance}");

            }
            else
            {
               
                Console.WriteLine("Invalid amount.");

            }
        }

         public void Withdraw()
        {
           
            var account = GetWithPassword();
            if (account == null) return;





            Console.Write("Enter amount to withdraw: ");
            
            if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
            {
                if (account.Withdraw(amount))
                {
                    
                    Console.WriteLine($"Withdrawal successful! New Balance: {account.Balance}");


                }
                else
                {
                    
                    Console.WriteLine("Insufficient balance.");

                }
            }
            else
            {

                Console.WriteLine("Invalid amount.");

            }
        }

         public void CheckBalance()
        {
           
            var account = GetWithPassword();
            if (account == null) return;

            Console.WriteLine($"Account Balance: {account.Balance}");

        }

        
        public void ViewAccountDetails()
        {
           
            var account = GetWithPassword();
            if (account == null) return;

            Console.WriteLine($"Account Number: {account.AccountNumber}");
            Console.WriteLine($"Holder Name: {account.HolderName}");
            Console.WriteLine($"Balance: {account.Balance}");

        }

         
        public void DeleteAccount()
        {
           
            var account = GetWithPassword();
            if (account == null) return;

           
            Console.Write("Are you sure you want to delete this account? (y/n): ");
            
            if (Console.ReadLine()?.ToLower() == "y")
            {
                
                accounts.Remove(account.AccountNumber);
                Console.WriteLine("Account deleted successfully.");

            }
            else
            {
                
                Console.WriteLine("Account deletion canceled!");

            }
        }

         private Account GetAccount()
        {
           
            Console.Write("Enter account number: ");
            
            if (int.TryParse(Console.ReadLine(), out int accountNumber) && accounts.TryGetValue(accountNumber, out Account account))
            {
                return account;
            }
            else
            {
                Console.WriteLine("Account not found.");
                return null;


            }
        }

         private Account GetWithPassword()
        {
          
            Account account = GetAccount();
           
            if (account == null) return null;

            Console.Write("Enter account password: ");
            string password = Console.ReadLine();


           
            if (account.VerifyPassword(password))
            {
               
                return account;

            }
            else
            {
                
                Console.WriteLine("Incorrect password!");
                return null;


            }
        }
    }

   
    
   
  
    public class Account
    {
        
       
        public int AccountNumber { get; }
       
        public string HolderName { get; }
        
        public decimal Balance { get; private set; }
        
        private string Password { get; }

       
        public Account(int accountNumber, string holderName, string password)
        {

            AccountNumber = accountNumber;
            HolderName = holderName;
            Password = password;
            Balance = 0;


        }

        
        public void Deposit(decimal amount)
        {

            Balance += amount;

        }

        
        public bool Withdraw(decimal amount)
        {
            
            if (amount > Balance)
            {
                return false;
            }
            Balance -= amount;
            return true;


        }

        
        public bool VerifyPassword(string password)
        {

            return Password == password;
        
        }
    }
}
