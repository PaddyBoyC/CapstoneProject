using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone_Project_441101_2223
{
    internal abstract class Project
    {
        private static int _nextID = 100;

        public int ID { get; private set; }

        protected List<Transaction> _transactions = new List<Transaction>();

        public Project(int? id = null)
        {
            ID = id ?? _nextID++;
        }

        public void AddTransaction(Transaction transaction)
        {
            _transactions.Add(transaction);
        }

        public decimal GetTotalSales()
        {
            decimal total = 0;
            foreach (Transaction transaction in _transactions)
            {
                if (transaction.Type == Transaction.TransactionType.Sale)
                {
                    total += transaction.Value;
                }
            }
            return total;
        }

        public decimal GetTotalPurchases()
        {
            decimal total = 0;
            foreach (Transaction transaction in _transactions)
            {
                if (transaction.Type == Transaction.TransactionType.Purchase)
                {
                    total += transaction.Value;
                }
            }
            return total;
        }

        public abstract decimal GetTotalRefund();

        public IEnumerable<Transaction> GetTransactionsOfType(Transaction.TransactionType type)
        {
            return _transactions.Where(transaction => transaction.Type == type);
        }
    }
}
