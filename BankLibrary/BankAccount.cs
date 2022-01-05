using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary
{
    public class BankAccount
    {
        public string CardNumber { get; set; }
        public int Pin { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Number { get; }
        public decimal Balance
        {
            get
            {
                decimal balance = 0;
                foreach (var item in allTransactions)
                {
                    balance += item.Amount;
                }
                return balance;
            }
        }

        private static int accountNumberSeed = 1234567890;
       
        private List<Transaction> allTransactions = new List<Transaction>();

        public BankAccount(string cardNumber, int pin, string name, string password)
        {
            this.CardNumber = cardNumber;
            this.Pin = pin;
            this.Name = name;
            this.Password = password;
            Number = accountNumberSeed.ToString();
            accountNumberSeed++;
        }

        public void MakeDeposit(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");
            }
            var deposit = new Transaction(amount, date, "Deposit", note);
            allTransactions.Add(deposit);
        }

        public void MakeWithdrawl(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of withdrawl must be positive");
            }
            if (Balance - amount < 0)
            {
                throw new InvalidOperationException("Not sufficient funds for this withdrawl");
            }
            var withdrawl = new Transaction(-amount, date, "Withdrawl", note);
            allTransactions.Add(withdrawl);

        }
        public string GetAccountHistory()
        {
            var report = new StringBuilder();

            // HEADER
            report.AppendLine("Date\t\tAmount\tAction\t\tNote");
            foreach (var item in allTransactions)
            {
                // ROWS
                report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{item.Action}\t{item.Notes}");
            }
            return report.ToString();
        }


        // to withdraw and deposit: must enter pin

    }
}
