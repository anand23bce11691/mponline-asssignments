using System;
using System.Collections.Generic;

interface INotificationService
{
    string ServiceName { get; }
    bool SendMessage(string recipient, string message);
}

class EmailNotificationService : INotificationService
{
    public string ServiceName => "Email Notification Service";

    public bool SendMessage(string recipient, string message)
    {
        if (string.IsNullOrWhiteSpace(recipient) || !recipient.Contains("@") || !recipient.Contains("."))
        {
            Console.WriteLine($"[Error] Invalid email address: '{recipient}'");
            return false;
        }

        Console.WriteLine($"\n[EMAIL SENT] To: {recipient}");
        Console.WriteLine($"Subject: Notification Alert");
        Console.WriteLine($"Body: {message}");
        Console.WriteLine("Status: Delivered successfully via SMTP.\n");
        return true;
    }
}

class SmsNotificationService : INotificationService
{
    public string ServiceName => "SMS Notification Service";

    public bool SendMessage(string recipient, string message)
    {
        if (string.IsNullOrWhiteSpace(recipient) || recipient.Length < 10)
        {
            Console.WriteLine($"[Error] Invalid phone number for SMS: '{recipient}'");
            return false;
        }

        int segments = (message.Length / 160) + 1;
        Console.WriteLine($"\n[SMS SENT] To: {recipient}");
        Console.WriteLine($"Message ({message.Length} chars, {segments} segment(s)): \"{message}\"");
        Console.WriteLine("Status: Dispatched via Cellular Gateway.\n");
        return true;
    }
}

class WhatsAppNotificationService : INotificationService
{
    public string ServiceName => "WhatsApp Notification Service";

    public bool SendMessage(string recipient, string message)
    {
        if (string.IsNullOrWhiteSpace(recipient))
        {
            Console.WriteLine($"[Error] Recipient WhatsApp contact cannot be empty.");
            return false;
        }

        Console.WriteLine($"\n[WHATSAPP SENT] To: {recipient}");
        Console.WriteLine($"Payload: {message}");
        Console.WriteLine("Status: Delivered with Double Blue Ticks via WhatsApp Business API.\n");
        return true;
    }
}

class Program
{
    static void Main()
    {
        List<INotificationService> services = new List<INotificationService>()
        {
            new EmailNotificationService(),
            new SmsNotificationService(),
            new WhatsAppNotificationService()
        };

        while (true)
        {
            Console.WriteLine("================ NOTIFICATION SYSTEM ================");
            Console.WriteLine("1. Send Email Notification");
            Console.WriteLine("2. Send SMS Notification");
            Console.WriteLine("3. Send WhatsApp Notification");
            Console.WriteLine("4. Broadcast Message to ALL Channels");
            Console.WriteLine("5. Exit");
            Console.Write("Select an option (1-5): ");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid input. Please enter a number.\n");
                continue;
            }

            if (choice == 5)
            {
                Console.WriteLine("Exiting Notification System. Goodbye!");
                break;
            }

            if (choice >= 1 && choice <= 3)
            {
                var service = services[choice - 1];
                Console.WriteLine($"\n--- {service.ServiceName} ---");
                Console.Write("Enter Recipient (Email/Phone/Handle): ");
                string recipient = Console.ReadLine() ?? "";

                Console.Write("Enter Message: ");
                string message = Console.ReadLine() ?? "";

                service.SendMessage(recipient, message);
            }
            else if (choice == 4)
            {
                Console.WriteLine("\n--- Broadcast Notification ---");
                Console.Write("Enter Email Address: ");
                string email = Console.ReadLine() ?? "";

                Console.Write("Enter Phone Number for SMS: ");
                string phone = Console.ReadLine() ?? "";

                Console.Write("Enter WhatsApp Number: ");
                string waPhone = Console.ReadLine() ?? "";

                Console.Write("Enter Broadcast Message: ");
                string msg = Console.ReadLine() ?? "";

                Console.WriteLine("\nBroadcasting to all channels...");
                services[0].SendMessage(email, msg);
                services[1].SendMessage(phone, msg);
                services[2].SendMessage(waPhone, msg);
            }
            else
            {
                Console.WriteLine("Invalid selection. Try again.\n");
            }
        }
    }
}
