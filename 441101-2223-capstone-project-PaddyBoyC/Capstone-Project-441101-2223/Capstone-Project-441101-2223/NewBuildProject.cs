using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone_Project_441101_2223
{
    internal class NewBuildProject : Project
    {

        public NewBuildProject(decimal landPurchaseAmount, int? id = null):
            base(id)
        {
            AddTransaction(new LandPurchaseTransaction(landPurchaseAmount));
        }

        /// <summary>
        /// Calculate the refund for all purchase transactions based on a VAT rate of 20%.
        /// </summary>
        /// <returns>The total refund</returns>
        public override decimal GetTotalRefund()
        {
            decimal totalRefund = 0;
            foreach (Transaction transactions in _transactions)
            {
                if (transactions.Type == Transaction.TransactionType.Purchase)
                {
                    decimal vatRefund = transactions.Value / 1.2m;
                    totalRefund += transactions.Value - vatRefund; 
                }
            }
            return totalRefund;
        }

        public override string ToString()
        {
            return $"New build, ID {ID}";
        }
    }
}
