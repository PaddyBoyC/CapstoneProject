using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone_Project_441101_2223
{
    internal class Portfolio
    {
        private Dictionary<int, Project> _projects = new();

        public Project AddNewBuildProject(decimal landPurchaseAmount, int? id = null)
        {
            if (landPurchaseAmount < 0)
            {
                Console.WriteLine("Land purchase amount must not be negative");
                return null;
            }

            NewBuildProject project = new NewBuildProject(landPurchaseAmount, id);
            _projects[project.ID] = project;
            return project;
        }

        /// <summary>
        /// Creates and returns a new renovation project with an initial property purchase transaction of the specified amount. If an ID is not speicifed then a new ID will be generated.
        /// </summary>
        /// <returns>New renovation project or null if the property purchase is negative.</returns>
        public Project AddRenovationProject(decimal propertyPurchaseAmount, int? id = null)
        {
            if (propertyPurchaseAmount < 0)
            {
                Console.WriteLine("Property purchase amount must not be negative");
                return null;
            }

            RenovationProject project = new RenovationProject(propertyPurchaseAmount, id);
            _projects[project.ID] = project;
            return project;
        }


        /// <summary>
        /// Show all current projects in the portfolio.
        /// </summary>
        public void ShowAllProjects()
        {
            foreach (var project in _projects.Values)
            {
                Console.WriteLine(project);
            }
            if (_projects.Count == 0)
            {
                Console.WriteLine("There are no projects");
            }
        }


        /// <summary>
        /// Displays the ID, sales, purchases, VAT refund and profit made from all current projects in the portfolio.
        /// </summary>
        public void ShowAllProjectsSummary()
        {
            Console.WriteLine($"{"ID",-5}{"Sales",10}{"Purchases",10}{"Refund",10}{"Profit",10}");
            decimal totalSales = 0;
            decimal totalPurchases = 0;
            decimal totalRefund = 0;
            decimal totalProfit = 0;
            foreach (var project in _projects.Values)
            {
                decimal sales = project.GetTotalSales();
                totalSales += sales;
                decimal purchases = project.GetTotalPurchases();
                totalPurchases += purchases;
                decimal refund = project.GetTotalRefund();
                totalRefund += refund;
                decimal profit = sales - purchases + refund;
                totalProfit += profit;
                Console.WriteLine($"{project.ID,-5}{sales,10:0.00}{purchases,10:0.00}{refund,10:0.00}{profit,10:0.00}");
            }
            Console.WriteLine($"{"All",-5}{totalSales,10:0.00}{totalPurchases,10:0.00}{totalRefund,10:0.00}{totalProfit,10:0.00}");
            if (_projects.Count == 0)
            {
                Console.WriteLine("There are no projects");
            }
        }

        public Project GetProject(int id)
        {
            if (_projects.ContainsKey(id))
            {
                return _projects[id];
            }
            return null;
        }

        public void RemoveProject(int id)
        {
            if (_projects.ContainsKey(id))
            {
                _projects.Remove(id);
            }
        }

        public void Clear()
        {
            _projects.Clear();
        }
    }
}
