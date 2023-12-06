using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone_Project_441101_2223
{
    internal class Menu
    {
        Portfolio _portfolio;

        public Menu()
        {
            _portfolio = new Portfolio();
        }

        public void Run()
        {
            Project selectedProject = null;
            string choice;
            do
            {
                Console.WriteLine("\nPortfolio Menu");
                Console.WriteLine(" 1: Add new project");
                Console.WriteLine(" 2: Display all projects");
                Console.WriteLine(" 3: Select a project");
                Console.WriteLine(" 4: Add a new transaction");
                Console.WriteLine(" 5: Remove selected project");
                Console.WriteLine(" 6: Display sales transactions for selected project");
                Console.WriteLine(" 7: Display purchase transactions for selected project");
                Console.WriteLine(" 8: Display summary for selected project");
                Console.WriteLine(" 9: Display the total summary of all projects");
                Console.WriteLine("10: Load Beige report file");
                Console.WriteLine("11: Exit");
                Console.WriteLine("Please enter a number between 1 and 11 inclusive");

                choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        {
                            AddProject();
                            break;
                        }
                    case "2":
                        {
                            _portfolio.ShowAllProjects();
                            WaitForKeyPress();
                            break;
                        }
                    case "3":
                        {
                            selectedProject = SelectProject();
                            break;
                        }
                    case "4":
                        {
                            AddTransaction(selectedProject);
                            break;
                        }
                    case "5":
                        {
                            RemoveProject(selectedProject);
                            break;
                        }
                    case "6":
                        {
                            DisplayTransactions(selectedProject, Transaction.TransactionType.Sale);
                            break;
                        }
                    case "7":
                        {
                            DisplayTransactions(selectedProject, Transaction.TransactionType.Purchase);
                            break;
                        }
                    case "8":
                        {
                            DisplaySummary(selectedProject);
                            break;
                        }

                    case "9":
                        {
                            _portfolio.ShowAllProjectsSummary();
                            WaitForKeyPress();
                            break;
                        }
                    case "10":
                        {
                            LoadReportFile();
                            break;
                        }
                }
            } while (choice != "11");
        }

        /// <summary>
        /// Prompts user to select a specific project from the list by inputting an ID.
        /// </summary>
        /// <returns>Selected project or null if there is no project ID input</returns>
        private Project SelectProject()
        {
            _portfolio.ShowAllProjects();
            Console.WriteLine("Please enter project ID to select");
            string id = Console.ReadLine();
            int intid;
            if (int.TryParse(id, out intid))
            {
                Project chosen = _portfolio.GetProject(intid);
                if (chosen == null)
                {
                    Console.WriteLine($"No project with ID {intid}");
                }
                else
                {
                    Console.WriteLine($"Selected: {chosen}");
                }
                WaitForKeyPress();
                return chosen;
            }
            else
            {
                Console.WriteLine("Invalid input");
            }
            WaitForKeyPress();
            return null;
        }


        /// <summary>
        /// Creates a new project determined by user input.
        /// </summary>
        void AddProject()
        {
            Console.WriteLine("Please choose the project type");
            Console.WriteLine("1. New build");
            Console.WriteLine("2. Renovation");
            Project project = null;
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    {
                        Console.WriteLine("Please enter an amount for the land purchase");
                        decimal landPurchaseAmount = decimal.Parse(Console.ReadLine());
                        project = _portfolio.AddNewBuildProject(landPurchaseAmount);
                        break;
                    }
                case "2":
                    {
                        Console.WriteLine("Please enter an amount for the property purchase");
                        decimal propertyPurchaseAmount = decimal.Parse(Console.ReadLine());
                        project = _portfolio.AddRenovationProject(propertyPurchaseAmount);
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Invalid project type");
                        WaitForKeyPress();
                        return;
                    }
            }
            if (project != null)
            {
                Console.WriteLine($"New project created with ID {project.ID}");
            }
        }


        /// <summary>
        /// Add a type of transaction and its amount determined by the user input to a selected project.
        /// </summary>
        /// <returns>A new transaction type to the selected project. If neither of the possible types of transactions are input returns 'Invalid input'.
        /// If there is no project selected returns null.</returns>
        /// <param name="project">Selected project</param>
        void AddTransaction(Project project)
        {
            if (project == null)
            {
                Console.WriteLine("You must first select a project");
                WaitForKeyPress();
                return;
            }

            Console.WriteLine("Enter the type of transaction");
            Console.WriteLine(" P: Purchase");
            Console.WriteLine(" S: Sale");
            string type = Console.ReadLine().ToUpper();
            if (type.Length == 0 || (type[0] != 'S' && type[0] != 'P'))
            {
                Console.WriteLine("Invalid input");
                WaitForKeyPress();
                return;
            }

            Console.WriteLine("Enter the amount of the transaction");
            string amount = Console.ReadLine();
            decimal amountDecimal;
            if (decimal.TryParse(amount, out amountDecimal))
            {
                if (amountDecimal < 0)
                {
                    Console.WriteLine("Amount must not be negative");
                    WaitForKeyPress();
                    return;
                }
                switch (type[0])
                {
                    case 'P':
                        {
                            project.AddTransaction(new Transaction(Transaction.TransactionType.Purchase, -amountDecimal));
                            break;
                        }
                    case 'S':
                        {
                            project.AddTransaction(new Transaction(Transaction.TransactionType.Sale, amountDecimal));
                            break;
                        }
                }
            }
            else
            {
                Console.WriteLine("Invalid input");
            }
            WaitForKeyPress();
        }

        /// <summary>
        /// Removes selected project.
        /// </summary>
        /// <param name="project">Selected project</param>
        void RemoveProject(Project project)
        {
            if (project == null)
            {
                Console.WriteLine("You must first select a project");
                WaitForKeyPress();
                return;
            }

            _portfolio.RemoveProject(project.ID);

            Console.WriteLine("Project Deleted");
            WaitForKeyPress();
        }


        /// <summary>
        /// Displays the transaction of the selected project.
        /// </summary>
        /// <returns> The transactions of the project selected. If there is no project selected returns null.</returns>
        /// <param name="project">Selected project</param>
        /// <param name="type">Type of transaction</param>
        void DisplayTransactions(Project project, Transaction.TransactionType type)
        {
            if (project == null)
            {
                Console.WriteLine("You must first select a project");
                return;
            }

            Console.WriteLine($"{"Type",-5}{"Amount",10}");
            foreach (Transaction transaction in project.GetTransactionsOfType(type))
            {
                Console.WriteLine($"{transaction.GetCode(),-5}{transaction.Value,10:0.00}");
            }
        }

        /// <summary>
        /// Prompt the user to enter a file name, and then load the specified report file.
        /// The file can be in one of two formats.
        /// </summary>
        void LoadReportFile() 
        {
            Console.WriteLine("Enter name of file to load");
            string fileName = Console.ReadLine();
            if (!File.Exists(fileName))
            {
                Console.WriteLine("File does not exist");
                WaitForKeyPress();
                return;
            }

            _portfolio.Clear();
            bool success = false;
            try
            {
                string[] lines = File.ReadAllLines(fileName);
                foreach (string line in lines)
                {
                    char transactionType;
                    int id;
                    decimal value;

                    if (line.Contains(','))
                    {
                        string[] columns = line.Split(',');
                        value = decimal.Parse(columns[2]);
                        id = int.Parse(columns[0]);
                        transactionType = columns[1][0];
                    }
                    else
                    {
                        transactionType = line[0];
                        id = int.Parse(line.Split('(', ')')[1]);
                        var valueSplit = line.Split(' ', '=');
                        string valueString = line.Split(' ', '=', ';')[3];
                        value = decimal.Parse(valueString);
                    }
                    switch (transactionType)
                    {
                        case 'R':
                            {
                                _portfolio.AddRenovationProject(value, id);
                                break;
                            }
                        case 'L':
                            {
                                _portfolio.AddNewBuildProject(value, id);
                                break;
                            }
                        case 'P':
                        case 'S':
                            {
                                Project project = _portfolio.GetProject(id);
                                if (project == null)
                                {
                                    Console.WriteLine("Invalid project ID while loading");
                                }
                                else
                                {
                                    Transaction.TransactionType type = transactionType switch
                                    {
                                        'P' => Transaction.TransactionType.Purchase,
                                        _ => Transaction.TransactionType.Sale,
                                    };
                                    project.AddTransaction(new Transaction(type, value));
                                }
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Invalid transaction type");
                                break;
                            }
                    }
                }
                success = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured while loading the file");
                Console.WriteLine("File may only be partially loaded");
            }
            if (success)
            {
                Console.WriteLine($"Successfully loaded {fileName}");
            }
            WaitForKeyPress();
        }

        /// <summary>
        /// Displays the ID, sales, purchases, VAT refund and profit made from the selected project.
        /// </summary>
        /// <param name="project">Selected project</param>
        void DisplaySummary(Project project)
        {
            if (project == null)
            {
                Console.WriteLine("You must first select a project");
                WaitForKeyPress();
                return;
            }

            Console.WriteLine($"{"ID",-5}{"Sales",10}{"Purchases",10}{"Refund",10}{"Profit",10}");
            DisplaySummaryLine(project);
        }

        /// <summary>
        /// Stores the respected sales, purchases, VAT refund and profit into variables appropriately.
        /// </summary>
        /// <param name="project">Selected project</param>
        private static void DisplaySummaryLine(Project project)
        {
            decimal sales = project.GetTotalSales();
            decimal purchases = project.GetTotalPurchases();
            decimal refund = project.GetTotalRefund();
            decimal profit = sales - purchases + refund;
            Console.WriteLine($"{project.ID,-5}{sales,10:0.00}{purchases,10:0.00}{refund,10:0.00}{profit,10:0.00}");
        }

        /// <summary>
        /// Prompts the user to press the 'Enter' key before making another selection.
        /// </summary>
        void WaitForKeyPress()
        {
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
        }
    }
}
