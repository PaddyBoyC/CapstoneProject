using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone_Project_441101_2223
{
    internal class LandPurchaseTransaction : Transaction
    {
        public LandPurchaseTransaction(decimal amount) :
            base(TransactionType.Purchase, amount)
        {

        }

        public override char GetCode()
        {
            return 'L';
        }
    }
}
