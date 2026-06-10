using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter a character: ");
        char ch = Convert.ToChar(Console.ReadLine());

        if (char.IsLetter(ch))
        {
            Console.WriteLine("The entered character is an Alphabet.");
        }
        else if (char.IsDigit(ch))
        {
            Console.WriteLine("The entered character is a Digit.");
        }
        else
        {
            Console.WriteLine("The entered character is a Special Symbol.");
        }
    }
}
