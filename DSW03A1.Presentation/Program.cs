using System;
using DSW03A1.BusinessLogic;

namespace DSW03A1.Presentation
{
    class Program
    {
        static AuthService authService = new AuthService();
        static CaseService caseService = new CaseService();

        static void Main(string[] args)
        {
            RunApplication();
        }

        static void RunApplication()
        {
            bool running = true;
            while (running)
            {
                if (!authService.IsLoggedIn())
                    running = HandlePreLoginMenu();
                else if (authService.GetCurrentRole().Equals("Student", StringComparison.OrdinalIgnoreCase))
                    HandleStudentMenu();
                else if (authService.GetCurrentRole().Equals("Administrator", StringComparison.OrdinalIgnoreCase))
                    HandleAdminMenu();
            }
        }

        static bool HandlePreLoginMenu()
        {
            Console.WriteLine("\n====== UJ Student Wellness Case Management System ======");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Exit");
            Console.Write("Select an option: ");
            string choice = Console.ReadLine()?.Trim();

            if (choice == "1") PerformLogin();
            else if (choice == "2") return false;
            else Console.WriteLine("Invalid option. Please try again.");
            return true;
        }

        static void PerformLogin()
        {
            Console.Write("Enter email: ");
            string email = Console.ReadLine()?.Trim();
            Console.Write("Enter password: ");
            string password = Console.ReadLine()?.Trim();

            bool success = authService.Login(email, password);
            if (success)
                Console.WriteLine($"\nWelcome, {authService.GetCurrentName()}! Role: {authService.GetCurrentRole()}");
            else
                Console.WriteLine("Invalid credentials. Please try again.");
        }

        static void HandleStudentMenu()
        {
            Console.WriteLine($"\n====== Student Menu | {authService.GetCurrentName()} ======");
            Console.WriteLine("1. View My Cases");
            Console.WriteLine("2. Create New Case");
            Console.WriteLine("3. Logout");
            Console.Write("Select an option: ");
            string choice = Console.ReadLine()?.Trim();

            if (choice == "1") DisplayStudentCases();
            else if (choice == "2") CreateNewCase();
            else if (choice == "3") PerformLogout();
            else Console.WriteLine("Invalid option. Please try again.");
        }

        static void DisplayStudentCases()
        {
            var studentCases = caseService.GetCasesForStudent(authService.GetCurrentEmail());
            Console.WriteLine("\n--- Your Cases ---");
            if (studentCases.Count == 0)
            {
                Console.WriteLine("No cases found.");
                return;
            }
            PrintCaseHeader();
            foreach (var c in studentCases)
                PrintCaseRow(c);
        }

        static void CreateNewCase()
        {
            Console.WriteLine("\nAvailable Issue Types: Academic, Financial, Accommodation, Mental Health, Wellness");
            Console.Write("Enter issue type: ");
            string issueType = Console.ReadLine()?.Trim();
            string result = caseService.CreateCase(authService.GetCurrentEmail(), issueType);
            Console.WriteLine(result);
        }

        static void HandleAdminMenu()
        {
            Console.WriteLine($"\n====== Admin Menu | {authService.GetCurrentName()} ======");
            Console.WriteLine("1. View All Cases");
            Console.WriteLine("2. Close a Case");
            Console.WriteLine("3. Delete a Case");
            Console.WriteLine("4. Logout");
            Console.Write("Select an option: ");
            string choice = Console.ReadLine()?.Trim();

            if (choice == "1") DisplayAllCases();
            else if (choice == "2") CloseCaseFlow();
            else if (choice == "3") DeleteCaseFlow();
            else if (choice == "4") PerformLogout();
            else Console.WriteLine("Invalid option. Please try again.");
        }

        static void DisplayAllCases()
        {
            var allCases = caseService.GetAllCases();
            Console.WriteLine("\n--- All Cases ---");
            if (allCases.Count == 0)
            {
                Console.WriteLine("No cases found.");
                return;
            }
            PrintCaseHeader();
            foreach (var c in allCases)
                PrintCaseRow(c);
        }

        static void CloseCaseFlow()
        {
            Console.Write("\nEnter Case ID to close: ");
            string caseId = Console.ReadLine()?.Trim();
            string result = caseService.CloseCase(caseId);
            Console.WriteLine(result);
        }

        static void DeleteCaseFlow()
        {
            Console.Write("\nEnter Case ID to delete: ");
            string caseId = Console.ReadLine()?.Trim();
            string result = caseService.DeleteCase(caseId);
            Console.WriteLine(result);
        }

        static void PerformLogout()
        {
            string name = authService.GetCurrentName();
            authService.Logout();
            Console.WriteLine($"\nGoodbye, {name}. You have been logged out.");
        }

        static void PrintCaseHeader()
        {
            Console.WriteLine($"{"CaseID",-8} {"Student Email",-30} {"Issue Type",-20} {"Status",-15}");
            Console.WriteLine(new string('-', 77));
        }

        static void PrintCaseRow(string[] c)
        {
            Console.WriteLine($"{c[0].Trim(),-8} {c[1].Trim(),-30} {c[2].Trim(),-20} {c[3].Trim(),-15}");
        }
    }
}
