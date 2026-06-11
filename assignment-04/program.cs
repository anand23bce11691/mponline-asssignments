using System;

class Drone
{
    // Private fields (cannot be directly accessed)
    private int batteryLife;
    private int altitude;

    // Public property to check flying status
    public bool IsFlying { get; private set; }

    // Constructor
    public Drone(int battery)
    {
        batteryLife = battery;
        altitude = 0;
        IsFlying = false;
    }

    // Set battery life
    public void SetBatteryLife(int battery)
    {
        batteryLife = battery;
    }

    // Get battery life
    public int GetBatteryLife()
    {
        return batteryLife;
    }

    // Get altitude
    public int GetAltitude()
    {
        return altitude;
    }

    // Take Off
    public void TakeOff()
    {
        if (batteryLife > 20)
        {
            IsFlying = true;
            altitude = 10;
            batteryLife -= 10;

            Console.WriteLine("Drone has taken off.");
        }
        else
        {
            Console.WriteLine("Insufficient battery for takeoff.");
        }
    }

    // Move Drone
    public void Move(string direction)
    {
        if (!IsFlying)
        {
            Console.WriteLine("Drone is not flying. Cannot move.");
            return;
        }

        if (batteryLife <= 5)
        {
            Console.WriteLine("Battery too low. Please land.");
            return;
        }

        batteryLife -= 5;

        Console.WriteLine($"Drone moved towards {direction}");
    }

    // Land Drone
    public void Land()
    {
        if (IsFlying)
        {
            altitude = 0;
            IsFlying = false;

            Console.WriteLine("Drone landed successfully.");
        }
        else
        {
            Console.WriteLine("Drone is already on the ground.");
        }
    }
}

class Program
{
    static void Main()
    {
        Drone d1 = new Drone(100);

        Console.WriteLine("Battery: " + d1.GetBatteryLife() + "%");

        d1.TakeOff();

        d1.Move("North");
        d1.Move("East");

        Console.WriteLine("Altitude: " + d1.GetAltitude() + " meters");
        Console.WriteLine("Battery: " + d1.GetBatteryLife() + "%");

        d1.Land();

        Console.WriteLine("Flying Status: " + d1.IsFlying);
    }
}
