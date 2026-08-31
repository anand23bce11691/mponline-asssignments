using System;
using System.Text.RegularExpressions;

// Custom Exception for GPS Service Failure
public class GpsFailureException : Exception
{
    public GpsFailureException(string message) : base(message) { }
}

// Custom Exception for Invalid Location
public class InvalidLocationException : Exception
{
    public InvalidLocationException(string message) : base(message) { }
}

class GpsService
{
    private bool isGpsHardwareActive;

    public GpsService(bool isActive)
    {
        isGpsHardwareActive = isActive;
    }

    public string DetectCurrentLocation()
    {
        Console.WriteLine("\n[GPS Service] Attempting to acquire satellite location...");
        
        if (!isGpsHardwareActive)
        {
            throw new GpsFailureException("GPS Hardware Signal Error: Unable to locate device. Satellite signal lost or GPS is turned off.");
        }

        return "Downtown Central Station, Zone 4";
    }
}

class BookingManager
{
    public static void ValidateLocation(string location)
    {
        if (string.IsNullOrWhiteSpace(location))
        {
            throw new InvalidLocationException("Location cannot be empty or blank.");
        }

        if (location.Trim().Length < 3)
        {
            throw new InvalidLocationException($"Location '{location}' is too short to pinpoint on the map. Minimum 3 characters required.");
        }

        // Check if input contains only digits or special characters
        if (Regex.IsMatch(location, @"^[0-9]+$"))
        {
            throw new InvalidLocationException($"Location '{location}' contains only numbers without a street or landmark name.");
        }

        string lower = location.Trim().ToLower();
        if (lower == "invalid" || lower == "null" || lower == "unknown" || lower == "none")
        {
            throw new InvalidLocationException($"Location '{location}' is restricted or non-serviceable.");
        }
    }

    public static void ConfirmBooking(string pickupLocation)
    {
        Console.WriteLine("\n============================================");
        Console.WriteLine("          CAB BOOKING CONFIRMED!            ");
        Console.WriteLine("============================================");
        Console.WriteLine($"Pickup Location : {pickupLocation}");
        Console.WriteLine($"Destination     : Airport Terminal 2");
        Console.WriteLine($"Cab Type        : Prime Sedan (MP-04-AB-9876)");
        Console.WriteLine($"Driver Name     : Rajesh Kumar (Rating 4.9★)");
        Console.WriteLine($"Estimated ETA   : 5 Minutes");
        Console.WriteLine($"Estimated Fare  : ₹350");
        Console.WriteLine("============================================\n");
    }
}

class Program
{
    static void Main()
    {
        bool gpsEnabled = true; // Toggleable GPS status

        while (true)
        {
            Console.WriteLine("================ CAB BOOKING SYSTEM ================");
            Console.WriteLine($"GPS Service Status: {(gpsEnabled ? "ONLINE" : "OFFLINE / SIGNAL FAIL")}");
            Console.WriteLine("1. Book Cab using Auto-GPS Pickup Location");
            Console.WriteLine("2. Book Cab using Manual Location Entry");
            Console.WriteLine("3. Toggle GPS Service Failure Simulation");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice: ");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid choice! Please enter a number.\n");
                continue;
            }

            if (choice == 4)
            {
                Console.WriteLine("Thank you for using Cab Booking Application. Goodbye!");
                break;
            }

            switch (choice)
            {
                case 1:
                    try
                    {
                        GpsService gps = new GpsService(gpsEnabled);
                        string detectedLoc = gps.DetectCurrentLocation();
                        BookingManager.ValidateLocation(detectedLoc);
                        BookingManager.ConfirmBooking(detectedLoc);
                    }
                    catch (GpsFailureException ex)
                    {
                        Console.WriteLine($"\n[EXCEPTIONAL EVENT - GPS ERROR]: {ex.Message}");
                        Console.WriteLine("Fallback suggestion: Please switch to Manual Location Entry to book your cab.\n");
                    }
                    catch (InvalidLocationException ex)
                    {
                        Console.WriteLine($"\n[EXCEPTIONAL EVENT - LOCATION ERROR]: {ex.Message}\n");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\n[UNEXPECTED ERROR]: {ex.Message}\n");
                    }
                    break;

                case 2:
                    Console.Write("\nEnter Pickup Location: ");
                    string userLoc = Console.ReadLine() ?? "";

                    try
                    {
                        BookingManager.ValidateLocation(userLoc);
                        BookingManager.ConfirmBooking(userLoc);
                    }
                    catch (InvalidLocationException ex)
                    {
                        Console.WriteLine($"\n[EXCEPTIONAL EVENT - INVALID LOCATION]: {ex.Message}");
                        Console.WriteLine("Please try entering a valid street, landmark, or area name.\n");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\n[UNEXPECTED ERROR]: {ex.Message}\n");
                    }
                    break;

                case 3:
                    gpsEnabled = !gpsEnabled;
                    Console.WriteLine($"\nSimulated GPS Service is now: {(gpsEnabled ? "ONLINE (Working)" : "OFFLINE (Failing)")}\n");
                    break;

                default:
                    Console.WriteLine("Invalid option. Try again.\n");
                    break;
            }
        }
    }
}
