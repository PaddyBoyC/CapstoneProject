using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone_Project_441101_2223
{
    internal class Transaction
    {
        public enum TransactionType
        {
            Sale,
            Purchase,
        }

        public TransactionType Type { get; private set; }
        public decimal Value { get; private set; }

        public Transaction(TransactionType type, decimal value)
        {
            Type = type;
            Value = value;
        }

        public virtual char GetCode()
        {
            return Type switch
            {
                TransactionType.Purchase => 'P',
                TransactionType.Sale => 'S',
                _ => '?',
            };
        }
    }
}
