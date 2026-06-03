using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Flat_Files
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FileManager.CreateStructure();

            while (true)
            {
                bool access = Login();

                if (access)
                {
                    MainMenu();
                }
                else
                {
                    Console.WriteLine(
                        "\nMaximum number of attempts reached.\nThe application will close.");

                    Console.ReadKey();
                    break;
                }
            }
        }

        static bool Login()
        {
            int attempts = 0;
            int maxAttempts = GetMaxAttempts();

            while (attempts < maxAttempts)
            {
                Console.Clear();

                Console.WriteLine("===== LOGIN =====\n");

                Console.Write("USER: ");
                string username = Console.ReadLine();

                Console.Write("PASSWORD: ");
                string password = Console.ReadLine();

                string[] lines =
                    File.ReadAllLines(
                        FileManager.UsersFile);

                foreach (string line in lines)
                {
                    string[] data = line.Split(',');

                    if (data.Length != 4)
                        continue;

                    string user = data[1];
                    string pass = data[2];

                    bool active =
                        bool.Parse(data[3]);

                    if (user == username &&
                        pass == password)
                    {
                        if (!active)
                        {
                            Console.WriteLine(
                                "\nInactive User");

                            Console.ReadKey();
                            return false;
                        }

                        Console.WriteLine(
                            "\nActive User");

                        Console.ReadKey();

                        return true;
                    }
                }

                attempts++;

                Console.WriteLine(
                    "\nInvalid username or password.");

                Console.WriteLine(
                    $"Attempt {attempts} of {maxAttempts}");

                Console.ReadKey();
            }

            return false;
        }

        static int GetMaxAttempts()
        {
            string[] lines =
                File.ReadAllLines(
                    FileManager.ConfigFile);

            foreach (string line in lines)
            {
                if (line.StartsWith("MAX_ATTEMPTS"))
                {
                    return int.Parse(
                        line.Split('=')[1]);
                }
            }

            return 3;
        }

        static bool LettersOnly(string text)
        {
            return text.All(c =>
                char.IsLetter(c) ||
                char.IsWhiteSpace(c));
        }

        static bool NumbersOnly(string text)
        {
            return text.All(char.IsDigit);
        }

        static bool CustomerExists(string id)
        {
            string[] lines =
                File.ReadAllLines(
                    FileManager.CustomersFile);

            foreach (string line in lines.Skip(1))
            {
                string[] data = line.Split(',');

                if (data[0] == id)
                    return true;
            }

            return false;
        }

        static string[] FindCustomerById(string id)
        {
            string[] lines =
                File.ReadAllLines(
                    FileManager.CustomersFile);

            foreach (string line in lines.Skip(1))
            {
                string[] data = line.Split(',');

                if (data[0] == id)
                    return data;
            }

            return null;
        }

        
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////

        static void AddCustomer()
        {
            Console.Clear();

            Console.WriteLine("===== NEW CUSTOMER =====\n");

            string id;

            while (true)
            {
                Console.Write("ID: ");
                id = Console.ReadLine();

                if (!NumbersOnly(id))
                {
                    Console.WriteLine("ID must contain numbers only.");
                    continue;
                }

                if (CustomerExists(id))
                {
                    Console.WriteLine("The ID already exists.");
                    continue;
                }

                break;
            }

            string firstName;

            while (true)
            {
                Console.Write("First Name: ");
                firstName = Console.ReadLine();

                if (LettersOnly(firstName))
                    break;

                Console.WriteLine("Letters only.");
            }

            string lastName;

            while (true)
            {
                Console.Write("Last Name: ");
                lastName = Console.ReadLine();

                if (LettersOnly(lastName))
                    break;

                Console.WriteLine("Letters only.");
            }

            string phone;

            while (true)
            {
                Console.Write("Phone: ");
                phone = Console.ReadLine();

                if (NumbersOnly(phone))
                    break;

                Console.WriteLine("Numbers only.");
            }

            Console.Write("City: ");
            string city = Console.ReadLine();

            decimal balance;

            while (true)
            {
                Console.Write("Balance: ");

                if (decimal.TryParse(Console.ReadLine(), out balance)
                    && balance >= 0)
                    break;

                Console.WriteLine(
                    "Please enter a valid balance.");
            }

            string record =
                $"{id},{firstName},{lastName},{phone},{city},{balance}";

            File.AppendAllText(
                FileManager.CustomersFile,
                record + Environment.NewLine);

            Console.WriteLine(
                "\nCustomer saved successfully.");

            Console.ReadKey();
        }

        static void ViewCustomers()
        {
            Console.Clear();

            Console.WriteLine("===== REGISTERED CUSTOMERS =====\n");

            string[] lines =
                File.ReadAllLines(
                    FileManager.CustomersFile);

            if (lines.Length <= 1)
            {
                Console.WriteLine(
                    "No customers registered.");

                Console.ReadKey();
                return;
            }

            Console.WriteLine(
                "{0,-8}{1,-15}{2,-15}{3,-15}{4,-15}{5,-15}",
                "ID",
                "First Name",
                "Last Name",
                "Phone",
                "City",
                "Balance");

            Console.WriteLine(
                new string('-', 80));

            foreach (string line in lines.Skip(1))
            {
                string[] data = line.Split(',');

                Console.WriteLine(
                    "{0,-8}{1,-15}{2,-15}{3,-10}{4,-15}{5,-15}",
                    data[0],
                    data[1],
                    data[2],
                    data[3],
                    data[4],
                    decimal.Parse(data[5]).ToString("N2"));
            }

            Console.ReadKey();
        }

        static void ModifyCustomer()
        {
            Console.Clear();

            Console.WriteLine("===== MODIFY CUSTOMER =====\n");

            Console.Write("Enter ID: ");
            string searchId = Console.ReadLine();

            string[] customer =
                FindCustomerById(searchId);

            if (customer == null)
            {
                Console.WriteLine(
                    "\nCustomer does not exist.");

                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nCurrent Data:");

            Console.WriteLine($"ID: {customer[0]}");
            Console.WriteLine($"First Name: {customer[1]}");
            Console.WriteLine($"Last Name: {customer[2]}");
            Console.WriteLine($"Phone: {customer[3]}");
            Console.WriteLine($"City: {customer[4]}");
            Console.WriteLine($"Balance: {customer[5]}");

            Console.WriteLine("\nDo you want to modify this customer?");

            Console.WriteLine("1. YES");
            Console.WriteLine("2. NO");

            string option = Console.ReadLine();

            if (option != "1")
                return;

            Console.Clear();

            Console.WriteLine("1. ID");
            Console.WriteLine("2. First Name");
            Console.WriteLine("3. Last Name");
            Console.WriteLine("4. Phone");
            Console.WriteLine("5. City");
            Console.WriteLine("6. Balance");
            Console.WriteLine("7. Main Menu");

            Console.Write("\nSelect: ");

            string field =
                Console.ReadLine();

            string newValue = "";

            switch (field)
            {
                case "1":

                    Console.Write(
                        "New ID: ");

                    newValue =
                        Console.ReadLine();

                    if (!NumbersOnly(newValue))
                    {
                        Console.WriteLine(
                            "Invalid ID");

                        Console.ReadKey();
                        return;
                    }

                    customer[0] = newValue;
                    break;

                case "2":

                    Console.Write(
                        "New First Name: ");

                    newValue =
                        Console.ReadLine();

                    customer[1] = newValue;
                    break;

                case "3":

                    Console.Write(
                        "New Last Name: ");

                    newValue =
                        Console.ReadLine();

                    customer[2] = newValue;
                    break;

                case "4":

                    Console.Write(
                        "New Phone: ");

                    newValue =
                        Console.ReadLine();

                    customer[3] = newValue;
                    break;

                case "5":

                    Console.Write(
                        "New City: ");

                    newValue =
                        Console.ReadLine();

                    customer[4] = newValue;
                    break;

                case "6":

                    Console.Write(
                        "New Balance: ");

                    newValue =
                        Console.ReadLine();

                    customer[5] = newValue;
                    break;

                default:
                    return;
            }

            Console.WriteLine(
                "\nAre you sure?");

            Console.WriteLine("1. YES");
            Console.WriteLine("2. NO");

            string confirm =
                Console.ReadLine();

            if (confirm != "1")
                return;

            UpdateCustomer(customer);

            Console.WriteLine(
                "\nModification Successful");

            Console.ReadKey();
        }

       
        /// //////////////////////////////////////////////////////////////////////////////////////////////


        static void UpdateCustomer(
    string[] updatedCustomer)
        {
            List<string> newLines =
                new List<string>();

            string[] lines =
                File.ReadAllLines(
                    FileManager.CustomersFile);

            newLines.Add(lines[0]);

            foreach (string line in lines.Skip(1))
            {
                string[] data =
                    line.Split(',');

                if (data[0] ==
                    updatedCustomer[0])
                {
                    newLines.Add(
                        string.Join(",",
                        updatedCustomer));
                }
                else
                {
                    newLines.Add(line);
                }
            }

            File.WriteAllLines(
                FileManager.CustomersFile,
                newLines);
        }

        /// /////////////////////////////////////////////////////////////////////////////////////////////////////

        static void DeleteCustomer()
        {
            Console.Clear();

            Console.WriteLine(
                "===== DELETE CUSTOMER =====\n");

            Console.Write("Enter ID: ");

            string id =
                Console.ReadLine();

            string[] customer =
                FindCustomerById(id);

            if (customer == null)
            {
                Console.WriteLine(
                    "\nCustomer does not exist.");

                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nCustomer Found:\n");

            Console.WriteLine($"ID: {customer[0]}");
            Console.WriteLine($"First Name: {customer[1]}");
            Console.WriteLine($"Last Name: {customer[2]}");
            Console.WriteLine($"Phone: {customer[3]}");
            Console.WriteLine($"City: {customer[4]}");
            Console.WriteLine($"Balance: {customer[5]}");

            Console.Write("\nDelete Customer (Y/N): ");

            string answer =
                Console.ReadLine().ToUpper();

            if (answer != "Y")
                return;

            List<string> newLines =
                new List<string>();

            string[] lines =
                File.ReadAllLines(
                    FileManager.CustomersFile);

            newLines.Add(lines[0]);

            foreach (string line in lines.Skip(1))
            {
                string[] data =
                    line.Split(',');

                if (data[0] != id)
                {
                    newLines.Add(line);
                }
            }

            File.WriteAllLines(
                FileManager.CustomersFile,
                newLines);

            Console.WriteLine(
                "\nCustomer deleted successfully.");

            Console.ReadKey();
        }

        static void CityReport()
        {
            Console.Clear();

            Console.WriteLine(
                "===== CITY REPORT =====\n");

            decimal grandTotal = 0;

            bool continueSearch = true;

            while (continueSearch)
            {
                Console.Write("Enter City: ");

                string city =
                    Console.ReadLine();

                string[] lines =
                    File.ReadAllLines(
                        FileManager.CustomersFile);

                decimal citySubtotal = 0;

                bool found = false;

                Console.WriteLine();

                Console.WriteLine(
                    $"CITY: {city}\n");

                Console.WriteLine(
                    "{0,-8}{1,-15}{2,-15}{3,15}",
                    "ID",
                    "First Name",
                    "Last Name",
                    "Balance");

                Console.WriteLine(
                    new string('-', 60));

                foreach (string line in lines.Skip(1))
                {
                    string[] data =
                        line.Split(',');

                    if (data[4].Equals(
                        city,
                        StringComparison.OrdinalIgnoreCase))
                    {
                        found = true;

                        decimal balance =
                            decimal.Parse(data[5]);

                        citySubtotal += balance;

                        Console.WriteLine(
                            "{0,-8}{1,-15}{2,-15}{3,15:N2}",
                            data[0],
                            data[1],
                            data[2],
                            balance);
                    }
                }

                if (!found)
                {
                    Console.WriteLine(
                        "\nNo records found for this city.");
                }
                else
                {
                    Console.WriteLine(
                        new string('=', 60));

                    Console.WriteLine(
                        $"Total {city}: {citySubtotal:N2}");

                    grandTotal += citySubtotal;
                }

                Console.WriteLine();

                Console.Write(
                   "Y = Another City\nN = Show Grand Total: ");

                string answer =
                    Console.ReadLine()
                    .ToUpper();

                if (answer != "Y")
                {
                    continueSearch = false;
                }

                Console.WriteLine();
            }

            Console.WriteLine(
                new string('=', 60));

            Console.WriteLine(
                $"GRAND TOTAL: {grandTotal:N2}");

            Console.ReadKey();
        }

        static void MainMenu()
        {
            int option;

            do
            {
                Console.Clear();

                Console.WriteLine(
                    "===== MAIN MENU =====");

                Console.WriteLine("1. VIEW CUSTOMERS");
                Console.WriteLine("2. ADD CUSTOMER");
                Console.WriteLine("3. MODIFY CUSTOMER");
                Console.WriteLine("4. DELETE CUSTOMER");
                Console.WriteLine("5. REPORT");
                Console.WriteLine("6. EXIT");

                Console.Write("\nSelect: ");

                int.TryParse(
                    Console.ReadLine(),
                    out option);

                switch (option)
                {
                    case 1:
                        ViewCustomers();
                        break;

                    case 2:
                        AddCustomer();
                        break;

                    case 3:
                        ModifyCustomer();
                        break;

                    case 4:
                        DeleteCustomer();
                        break;

                    case 5:
                        CityReport();
                        break;
                }

            } while (option != 6);
        }
    }
}