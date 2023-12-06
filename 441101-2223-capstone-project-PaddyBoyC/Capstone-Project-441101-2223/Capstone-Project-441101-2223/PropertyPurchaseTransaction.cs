using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone_Project_441101_2223
{
    internal class PropertyPurchaseTransaction : Transaction
    {
        public PropertyPurchaseTransaction(decimal amount) :
            base(TransactionType.Purchase, amount)
        {

        }

        public override char GetCode()
        {
            return 'R';
        }
    }
}
