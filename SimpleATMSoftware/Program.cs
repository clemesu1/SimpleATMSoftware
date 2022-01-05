//This simple project will essentially create a simulation of an ATM within a Windows program. Just like an ATM, the program should have at least the following features:

//Checking whether an input – such as an ATM card (a debit/credit card number) – is recorded correctly
//Verifying the user by asking for a PIN
//In case of negative verification, logging out the user
//In case of positive verification, showing multiple options, including cash availability, the previous five transactions, and cash withdrawal
//Giving the user the ability to withdraw up to $1,000 worth of cash in one transaction, with total transactions limited to ten per day.


// options: create account (name, card number, pin), login, once logged in: amount of cash in account, previous 5 transactions, withdrawal, deposit
// can not withdraw more than $1000 a day, and no more than 10 transactions a day.


// Create account: create account and add to list of accounts.
// Login: search list of accounts to see if information entered matches any accounts in list.

using BankLibrary;

Console.WriteLine("Welcome to our Simple ATM Software!\n");

bool run = true;
int transactionCount = 0;
decimal withdrawTotal = 0;
List<BankAccount> allBankAccounts = new List<BankAccount>();

allBankAccounts.Add(new BankAccount("1234567898765432", 1234, "Test", "pass"));

while (run)
{
    Console.WriteLine("Please select an option! (Enter 1, 2, or 3)");
    Console.WriteLine("1. Login to bank account");
    Console.WriteLine("2. Create a bank account");
    Console.WriteLine("3. Quit");
    Console.Write("Enter your option: ");

    string input = Console.ReadLine();
    int selection = 0;
    try
    {
        selection = int.Parse(input);
    }
    catch (FormatException e)
    {
        Console.WriteLine(e.Message);
        return;
    }

    switch (selection)
    {
        case 1:
            Login();
            break;
        case 2:
            CreateAccount();
            break;
        case 3:
            run = false;
            break;
        default:
            Console.WriteLine("Incorrect entry. Please try again.");
            break;
    }

}

void CreateAccount()
{
    Console.WriteLine("Bank Account Creation\n");
    Console.Write("Enter credit/debit card number: ");
    string cardNumber = Console.ReadLine();
    Console.Write("Enter pin number: ");
    int pin = int.Parse(Console.ReadLine());
    Console.Write("Enter your name: ");
    string name = Console.ReadLine();
    Console.Write("Enter a password: ");
    string password = Console.ReadLine();
    BankAccount account = new BankAccount(cardNumber, pin, name, password);
    allBankAccounts.Add(account);
    Console.WriteLine($"Bank Account has been created. Account #{account.Number}");
}

void Login()
{
    Console.WriteLine("Login to Bank Account\n");
    Console.Write("Enter credit/debit card number: ");
    string cardNumber = Console.ReadLine();
    Console.Write("Enter account password: ");
    string password = Console.ReadLine();
    foreach (var account in allBankAccounts)
    {
        if (!(cardNumber.Equals(account.CardNumber)) || !(password.Equals(account.Password)))
        {
            Console.WriteLine("Information entered does not match.");
            return;
        }
        Console.WriteLine("Login successful");
        GetATMOptions(account);
        break;
    }
}

void GetATMOptions(BankAccount account)
{
    // amount of cash in account, previous 5 transactions, withdrawal, deposit
    while (true)
    {
        Console.WriteLine("ATM Options\n1. Show Balance\n2. View Previous Transactions\n3. Withdraw\n4. Deposit\n5. Log out (Enter 1 to 5)");
        Console.Write("Enter your option: ");
        string input = Console.ReadLine();
        int option;
        try
        {
            option = int.Parse(input);
        }
        catch (FormatException e)
        {
            Console.WriteLine(e.Message);
            return;
        }

        switch (option)
        {
            case 1:
                Console.WriteLine($"Balance: ${account.Balance}");
                break;
            case 2:
                Console.WriteLine(account.GetAccountHistory());
                break;
            case 3:
                if (transactionCount <= 10)
                {
                    withdraw(account);
                    transactionCount++;
                } 
                else
                {
                    Console.WriteLine("ERROR: Cannot make more than 10 transactions a day. Try again tomorrow.");
                }
                break;
            case 4:
                if (transactionCount <= 10)
                {
                    deposit(account);
                    transactionCount++;
                }
                else
                {
                    Console.WriteLine("ERROR: Cannot make more than 10 transactions a day. Try again tomorrow.");
                }
                break;
            case 5:
                return;
            default:
                Console.WriteLine("Incorrect entry. Please try again.");
                break;
        }
    }
}

void withdraw(BankAccount account)
{

    if (withdrawTotal >= 1000)
    {
        Console.WriteLine("Cannot withdraw more than $1000 a day. Try again tomorrow.");
        return;
    }
    confirmPin(account);

    Console.Write("Enter amount to withdraw: ");
    decimal amount = decimal.Parse(Console.ReadLine());
    Console.Write("Enter note: ");
    string note = Console.ReadLine();
    try
    {
        account.MakeWithdrawl(amount, DateTime.Now, note);
    }
    catch (InvalidOperationException e)
    {
        Console.WriteLine(e.Message);
    }
    withdrawTotal += amount;
}

void deposit(BankAccount account)
{
    confirmPin(account);

    Console.Write("Enter amount to deposit: ");
    decimal amount = decimal.Parse(Console.ReadLine());
    Console.Write("Enter note: ");
    string note = Console.ReadLine();
    try
    {
        account.MakeDeposit(amount, DateTime.Now, note);
    }
    catch (ArgumentOutOfRangeException e)
    {
        Console.WriteLine(e.Message);
    }
}

void confirmPin(BankAccount account)
{
    int pin = 0;

    while (true)
    {
        Console.Write("Enter pin: ");
        pin = int.Parse(Console.ReadLine());

        if (pin == account.Pin)
        {
            return;
        }
        Console.WriteLine("Incorrect pin entered.");
    }
}
