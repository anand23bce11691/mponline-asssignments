using System;

class Program
{
    static void Main()
    {
        int choice;
        double num1, num2, result;

        do
        {
            Console.WriteLine("\n===== ARITHMETIC MENU =====");
            Console.WriteLine("1. Addition");
            Console.WriteLine("2. Subtraction");
            Console.WriteLine("3. Multiplication");
            Console.WriteLine("4. Division");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice: ");

            choice = Convert.ToInt32(Console.ReadLine());

            if (choice >= 1 && choice <= 4)
            {
                Console.Write("Enter First Number: ");
                num1 = Convert.ToDouble(Console.ReadLine());

                Console.Write("Enter Second Number: ");
                num2 = Convert.ToDouble(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        result = num1 + num2;
                        Console.WriteLine("Addition = " + result);
                        break;

                    case 2:
                        result = num1 - num2;
                        Console.WriteLine("Subtraction = " + result);
                        break;

                    case 3:
                        result = num1 * num2;
                        Console.WriteLine("Multiplication = " + result);
                        break;

                    case 4:
                        if (num2 != 0)
                        {
                            result = num1 / num2;
                            Console.WriteLine("Division = " + result);
                        }
                        else
                        {
                            Console.WriteLine("Division by zero is not possible.");
                        }
                        break;
                }
            }
            else if (choice != 5)
            {
                Console.WriteLine("Invalid Choice!");
            }

        } while (choice != 5);

        Console.WriteLine("Program Terminated.");
    }
}
