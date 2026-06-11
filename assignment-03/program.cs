using System;

class Product
{
    public int Id;
    public string Name;
    public double Price;
    public int Quantity;
}

class Program
{
    static void Main()
    {
        Console.Write("Enter number of products: ");
        int n = Convert.ToInt32(Console.ReadLine());

        Product[] cart = new Product[n];
        double totalCost = 0;

        for (int i = 0; i < n; i++)
        {
            cart[i] = new Product();

            Console.WriteLine("\nEnter Details for Product " + (i + 1));

            Console.Write("Product ID: ");
            cart[i].Id = Convert.ToInt32(Console.ReadLine());

            Console.Write("Product Name: ");
            cart[i].Name = Console.ReadLine();

            Console.Write("Product Price: ");
            cart[i].Price = Convert.ToDouble(Console.ReadLine());

            Console.Write("Product Quantity: ");
            cart[i].Quantity = Convert.ToInt32(Console.ReadLine());

            totalCost += cart[i].Price * cart[i].Quantity;
        }

        Console.WriteLine("\n========== SHOPPING CART ==========");
        Console.WriteLine("ID\tName\tPrice\tQuantity\tAmount");

        for (int i = 0; i < n; i++)
        {
            double amount = cart[i].Price * cart[i].Quantity;

            Console.WriteLine(
                cart[i].Id + "\t" +
                cart[i].Name + "\t" +
                cart[i].Price + "\t" +
                cart[i].Quantity + "\t\t" +
                amount
            );
        }

        Console.WriteLine("\nTotal Cost = ₹" + totalCost);
    }
}
