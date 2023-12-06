using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone_Project_441101_2223
{
    internal class RenovationProject : Project
    {
        public RenovationProject(decimal propertyPurchaseAmount, int? id = null) :
            base(id)
        {
            AddTransaction(new PropertyPurchaseTransaction(propertyPurchaseAmount));
        }

        public override decimal GetTotalRefund()
        {
            return 0;
        }

        public override string ToString()
        {
            return $"Renovation, ID {ID}";
        }
    }
}
