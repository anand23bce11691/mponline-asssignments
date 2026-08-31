using System;
using System.Collections.Generic;

class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }

    public Product(int id, string name, double price)
    {
        Id = id;
        Name = name;
        Price = price;
    }
}

class Order
{
    private static int counter = 101;
    public int OrderId { get; private set; }
    public string CustomerName { get; set; }
    public List<Product> Items { get; set; }
    public double TotalAmount { get; set; }
    public string Status { get; set; }
    public string PaymentMethod { get; set; }

    public Order(string customerName, List<Product> items, double totalAmount, string paymentMethod)
    {
        OrderId = counter++;
        CustomerName = customerName;
        Items = items;
        TotalAmount = totalAmount;
        Status = "Placed";
        PaymentMethod = paymentMethod;
    }

    public void DisplayOrderDetails()
    {
        Console.WriteLine($"\n--- Order ID: #{OrderId} ---");
        Console.WriteLine($"Customer: {CustomerName}");
        Console.WriteLine($"Payment Method: {PaymentMethod}");
        Console.WriteLine("Items Ordered:");
        foreach (var item in Items)
        {
            Console.WriteLine($"  - {item.Name} : ₹{item.Price}");
        }
        Console.WriteLine($"Total Amount Paid: ₹{TotalAmount}");
        Console.WriteLine($"Current Status: {Status}");
    }
}

abstract class User
{
    public string Name { get; set; }

    public User(string name)
    {
        Name = name;
    }

    public abstract void ShowExperience(List<Product> catalog, List<Order> orders);
}

class Customer : User
{
    public Customer(string name) : base(name) { }

    public override void ShowExperience(List<Product> catalog, List<Order> orders)
    {
        Console.WriteLine($"\n=== Welcome Customer: {Name} ===");
        List<Product> cart = new List<Product>();
        double cartTotal = 0;

        while (true)
        {
            Console.WriteLine("\n--- Catalog ---");
            foreach (var prod in catalog)
            {
                Console.WriteLine($"{prod.Id}. {prod.Name} - ₹{prod.Price}");
            }
            Console.WriteLine("0. Proceed to Checkout / View Cart");

            Console.Write("Enter Product ID to add to cart (or 0 to checkout): ");
            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid input!");
                continue;
            }

            if (choice == 0)
            {
                break;
            }

            Product? selected = catalog.Find(p => p.Id == choice);
            if (selected != null)
            {
                cart.Add(selected);
                cartTotal += selected.Price;
                Console.WriteLine($"Added '{selected.Name}' to cart. Current Total: ₹{cartTotal}");
            }
            else
            {
                Console.WriteLine("Product not found!");
            }
        }

        if (cart.Count == 0)
        {
            Console.WriteLine("Your cart is empty. Returning to main menu.");
            return;
        }

        Console.WriteLine($"\nCart Summary: {cart.Count} items | Total Amount: ₹{cartTotal}");
        Console.WriteLine("Select Payment Method:");
        Console.WriteLine("1. UPI");
        Console.WriteLine("2. Credit / Debit Card");
        Console.WriteLine("3. Cash on Delivery");
        Console.Write("Choice: ");
        int payChoice = Convert.ToInt32(Console.ReadLine());

        string paymentMethod = payChoice switch
        {
            1 => "UPI",
            2 => "Credit/Debit Card",
            3 => "Cash on Delivery",
            _ => "UPI"
        };

        Console.WriteLine($"Processing payment of ₹{cartTotal} via {paymentMethod}...");
        Console.WriteLine("Payment Successful! Order placed successfully.");

        Order newOrder = new Order(Name, cart, cartTotal, paymentMethod);
        orders.Add(newOrder);
        newOrder.DisplayOrderDetails();
    }
}

class DeliveryAgent : User
{
    public DeliveryAgent(string name) : base(name) { }

    public override void ShowExperience(List<Product> catalog, List<Order> orders)
    {
        Console.WriteLine($"\n=== Welcome Delivery Agent: {Name} ===");
        if (orders.Count == 0)
        {
            Console.WriteLine("No orders available for delivery right now.");
            return;
        }

        Console.WriteLine("\nAvailable Orders:");
        foreach (var order in orders)
        {
            Console.WriteLine($"Order #{order.OrderId} | Customer: {order.CustomerName} | Amount: ₹{order.TotalAmount} | Status: {order.Status}");
        }

        Console.Write("\nEnter Order ID to update status (or 0 to exit): ");
        int orderId = Convert.ToInt32(Console.ReadLine());
        if (orderId == 0) return;

        Order? targetOrder = orders.Find(o => o.OrderId == orderId);
        if (targetOrder != null)
        {
            Console.WriteLine("Select New Status:");
            Console.WriteLine("1. Out for Delivery");
            Console.WriteLine("2. Delivered");
            Console.Write("Choice: ");
            int stChoice = Convert.ToInt32(Console.ReadLine());

            if (stChoice == 1)
            {
                targetOrder.Status = "Out for Delivery";
                Console.WriteLine($"Order #{orderId} updated to Out for Delivery.");
            }
            else if (stChoice == 2)
            {
                targetOrder.Status = "Delivered";
                Console.WriteLine($"Order #{orderId} updated to Delivered.");
            }
            else
            {
                Console.WriteLine("Invalid status selection.");
            }
        }
        else
        {
            Console.WriteLine("Order ID not found.");
        }
    }
}

class Program
{
    static void Main()
    {
        List<Product> catalog = new List<Product>()
        {
            new Product(1, "Laptop", 55000),
            new Product(2, "Wireless Mouse", 800),
            new Product(3, "Keyboard", 1500),
            new Product(4, "Headphones", 2500)
        };

        List<Order> orders = new List<Order>();

        while (true)
        {
            Console.WriteLine("\n================ SHOPPING SYSTEM ================");
            Console.WriteLine("1. Customer Experience (Place Order & Pay)");
            Console.WriteLine("2. Delivery Agent Experience (View & Update Deliveries)");
            Console.WriteLine("3. Exit");
            Console.Write("Enter your choice: ");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                continue;
            }

            if (choice == 3)
            {
                Console.WriteLine("Thank you for using the Shopping System. Goodbye!");
                break;
            }

            switch (choice)
            {
                case 1:
                    Console.Write("Enter Customer Name: ");
                    string custName = Console.ReadLine() ?? "Guest Customer";
                    User customer = new Customer(custName);
                    customer.ShowExperience(catalog, orders);
                    break;

                case 2:
                    Console.Write("Enter Delivery Agent Name: ");
                    string agentName = Console.ReadLine() ?? "Agent";
                    User agent = new DeliveryAgent(agentName);
                    agent.ShowExperience(catalog, orders);
                    break;

                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
    }
}
