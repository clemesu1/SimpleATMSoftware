using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary
{
    public class Transaction
    {
        public decimal Amount { get; }
        public DateTime Date { get; }
        public string Action { get; }
        public string Notes { get; }

        public Transaction(decimal amount, DateTime date, string action, string note)
        {
            Amount = amount;
            Date = date;
            Action = action;
            Notes = note;
        }
    }
}
