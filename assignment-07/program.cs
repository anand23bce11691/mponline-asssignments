using System;
using System.Collections.Generic;

abstract class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }

    public Employee(int id, string name, string role)
    {
        Id = id;
        Name = name;
        Role = role;
    }

    public abstract void GenerateReport();
}

class Developer : Employee
{
    public string ProjectName { get; set; }
    public int FeaturesCompleted { get; set; }
    public int SprintProgressPercentage { get; set; }

    public Developer(int id, string name, string projectName, int featuresCompleted, int sprintProgressPercentage)
        : base(id, name, "Software Developer")
    {
        ProjectName = projectName;
        FeaturesCompleted = featuresCompleted;
        SprintProgressPercentage = sprintProgressPercentage;
    }

    public override void GenerateReport()
    {
        Console.WriteLine("\n============================================");
        Console.WriteLine($"      DEVELOPER REPORT - {Name} (ID: {Id})");
        Console.WriteLine("============================================");
        Console.WriteLine($"Role: {Role}");
        Console.WriteLine($"Project Assigned: {ProjectName}");
        Console.WriteLine($"Features Completed This Sprint: {FeaturesCompleted}");
        Console.WriteLine($"Project Progression: {SprintProgressPercentage}% Completed");
        Console.WriteLine("Summary: Codebase architecture is stable, feature development progressing on schedule.");
    }
}

class Tester : Employee
{
    public string ProjectName { get; set; }
    public int TestCasesExecuted { get; set; }
    public int BugsIdentified { get; set; }
    public string PerksAndHighlights { get; set; }

    public Tester(int id, string name, string projectName, int testCasesExecuted, int bugsIdentified, string perksAndHighlights)
        : base(id, name, "QA Tester")
    {
        ProjectName = projectName;
        TestCasesExecuted = testCasesExecuted;
        BugsIdentified = bugsIdentified;
        PerksAndHighlights = perksAndHighlights;
    }

    public override void GenerateReport()
    {
        Console.WriteLine("\n============================================");
        Console.WriteLine($"        TESTER REPORT - {Name} (ID: {Id})");
        Console.WriteLine("============================================");
        Console.WriteLine($"Role: {Role}");
        Console.WriteLine($"Project Tested: {ProjectName}");
        Console.WriteLine($"Total Test Cases Executed: {TestCasesExecuted}");
        Console.WriteLine($"Bugs Logged & Verified: {BugsIdentified}");
        Console.WriteLine($"Project Quality Perks: {PerksAndHighlights}");
        Console.WriteLine("Summary: Overall software stability is optimal; critical perks & edge cases verified.");
    }
}

class Manager : Employee
{
    public List<Developer> TeamDevelopers { get; set; }
    public List<Tester> TeamTesters { get; set; }

    public Manager(int id, string name)
        : base(id, name, "Engineering Manager")
    {
        TeamDevelopers = new List<Developer>();
        TeamTesters = new List<Tester>();
    }

    public void AddDeveloper(Developer dev)
    {
        TeamDevelopers.Add(dev);
    }

    public void AddTester(Tester tester)
    {
        TeamTesters.Add(tester);
    }

    public override void GenerateReport()
    {
        Console.WriteLine("\n========================================================");
        Console.WriteLine($"    CONSOLIDATED MANAGER REPORT - {Name} (ID: {Id})");
        Console.WriteLine("========================================================");
        Console.WriteLine($"Role: {Role}");
        Console.WriteLine($"Total Team Members Managed: {TeamDevelopers.Count + TeamTesters.Count}");
        
        Console.WriteLine("\n--- SECTION 1: DEVELOPER PROJECT PROGRESSION ---");
        int totalProgress = 0;
        foreach (var dev in TeamDevelopers)
        {
            Console.WriteLine($" • [{dev.Name}] Project: {dev.ProjectName} | Features Done: {dev.FeaturesCompleted} | Progress: {dev.SprintProgressPercentage}%");
            totalProgress += dev.SprintProgressPercentage;
        }
        double avgDevProgress = TeamDevelopers.Count > 0 ? (double)totalProgress / TeamDevelopers.Count : 0;
        Console.WriteLine($"Average Developer Progress: {avgDevProgress:F1}%");

        Console.WriteLine("\n--- SECTION 2: TESTER QUALITY & PERKS REPORT ---");
        int totalBugs = 0;
        foreach (var t in TeamTesters)
        {
            Console.WriteLine($" • [{t.Name}] Project: {t.ProjectName} | Tests Run: {t.TestCasesExecuted} | Bugs Found: {t.BugsIdentified} | Perks: {t.PerksAndHighlights}");
            totalBugs += t.BugsIdentified;
        }
        Console.WriteLine($"Total Quality Bugs Identified: {totalBugs}");

        Console.WriteLine("\n--- MANAGER'S EXECUTIVE CONCLUSION ---");
        Console.WriteLine("Project velocity is healthy with excellent synergy between development progression and quality assurance testing perks.");
        Console.WriteLine("========================================================\n");
    }
}

class Program
{
    static void Main()
    {
        // Setup Manager and Team
        Manager manager = new Manager(101, "Alice Johnson");

        Developer dev1 = new Developer(201, "Bob Smith", "E-Commerce Suite", 12, 85);
        Developer dev2 = new Developer(202, "Charlie Brown", "Payment Gateway", 8, 70);

        Tester test1 = new Tester(301, "Diana Prince", "E-Commerce Suite", 150, 14, "Zero critical security defects, 99.8% uptime perk.");
        Tester test2 = new Tester(302, "Evan Wright", "Payment Gateway", 95, 6, "Seamless integration perk across multiple gateways.");

        manager.AddDeveloper(dev1);
        manager.AddDeveloper(dev2);
        manager.AddTester(test1);
        manager.AddTester(test2);

        List<Employee> allEmployees = new List<Employee>() { dev1, dev2, test1, test2, manager };

        while (true)
        {
            Console.WriteLine("================ EMPLOYEE REPORTING SYSTEM ================");
            Console.WriteLine("1. Generate Developer Report (Project Progression)");
            Console.WriteLine("2. Generate Tester Report (Project Perks & Quality)");
            Console.WriteLine("3. Generate Manager Report (Consolidated Team Overview)");
            Console.WriteLine("4. Generate All Employee Reports");
            Console.WriteLine("5. Exit");
            Console.Write("Enter choice (1-5): ");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid choice. Please enter a valid number.\n");
                continue;
            }

            if (choice == 5)
            {
                Console.WriteLine("Exiting Reporting System. Have a great day!");
                break;
            }

            switch (choice)
            {
                case 1:
                    Console.WriteLine("\nSelect Developer:");
                    Console.WriteLine("1. Bob Smith");
                    Console.WriteLine("2. Charlie Brown");
                    Console.Write("Choice: ");
                    int dChoice = Convert.ToInt32(Console.ReadLine());
                    if (dChoice == 1) dev1.GenerateReport();
                    else if (dChoice == 2) dev2.GenerateReport();
                    else Console.WriteLine("Invalid selection.");
                    break;

                case 2:
                    Console.WriteLine("\nSelect Tester:");
                    Console.WriteLine("1. Diana Prince");
                    Console.WriteLine("2. Evan Wright");
                    Console.Write("Choice: ");
                    int tChoice = Convert.ToInt32(Console.ReadLine());
                    if (tChoice == 1) test1.GenerateReport();
                    else if (tChoice == 2) test2.GenerateReport();
                    else Console.WriteLine("Invalid selection.");
                    break;

                case 3:
                    manager.GenerateReport();
                    break;

                case 4:
                    foreach (var emp in allEmployees)
                    {
                        emp.GenerateReport();
                    }
                    break;

                default:
                    Console.WriteLine("Invalid option. Try again.\n");
                    break;
            }
        }
    }
}
