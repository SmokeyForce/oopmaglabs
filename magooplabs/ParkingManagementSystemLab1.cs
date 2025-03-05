using System;

namespace ParkingManagementSystem.Lab1
{
    public interface ISensor
    {
        void DetectVehicle();
    }

    public interface IBarrier
    {
        void Open();
        void Close();
    }

    public interface IDisplay
    {
        void ShowStatus(string message);
    }
    public class IndoorSensor : ISensor
    {
        public void DetectVehicle()
        {
            Console.WriteLine("IndoorSensor: Vehicle detected in Indor parking zone");
        }
    }

    public class IndoorBarrier : IBarrier
    {
        public void Open()
        {
            Console.WriteLine("IndoorBarrier: Indoor Barrier opened for Indoor parking zone");
        }

        public void Close()
        {
            Console.WriteLine("IndoorBarrier: Indoor Barrier closed for Indoor parking zone");
        }
    }

    public class IndoorDisplay : IDisplay
    {
        public void ShowStatus(string message)
        {
            Console.WriteLine("IndoorDisplay: " + message);
        }
    }

    public class OutdoorSensor : ISensor
    {
        public void DetectVehicle()
        {
            Console.WriteLine("OutdoorSensor: Vehicle detected in Outdoor parking zone");
        }
    }

    public class OutdoorBarrier : IBarrier
    {
        public void Open()
        {
            Console.WriteLine("OutdoorBarrier: Outdoor Barrier opened for Outdoor parking zone");
        }

        public void Close()
        {
            Console.WriteLine("OutdoorBarrier: Outdoor Barrier closed for Outdoor parking zone");
        }
    }

    public class OutdoorDisplay : IDisplay
    {
        public void ShowStatus(string message)
        {
            Console.WriteLine("OutdoorDisplay: " + message);
        }
    }

    // Абстрактна фабрика для створення пристроїв
    public interface IParkingZoneFactory
    {
        ISensor CreateSensor();
        IBarrier CreateBarrier();
        IDisplay CreateDisplay();
    }

    public class IndoorParkingZoneFactory : IParkingZoneFactory
    {
        public ISensor CreateSensor()
        {
            return new IndoorSensor();
        }

        public IBarrier CreateBarrier()
        {
            return new IndoorBarrier();
        }

        public IDisplay CreateDisplay()
        {
            return new IndoorDisplay();
        }
    }

    public class OutdoorParkingZoneFactory : IParkingZoneFactory
    {
        public ISensor CreateSensor()
        {
            return new OutdoorSensor();
        }

        public IBarrier CreateBarrier()
        {
            return new OutdoorBarrier();
        }

        public IDisplay CreateDisplay()
        {
            return new OutdoorDisplay();
        }
    }
    public static class Lab1Demo
    {
        public static void RunDemo()
        {
            Console.WriteLine("Lab1 Demo: Factory Method та Abstract Factory\n");

            IParkingZoneFactory indoorFactory = new IndoorParkingZoneFactory();
            ISensor indoorSensor = indoorFactory.CreateSensor();
            IBarrier indoorBarrier = indoorFactory.CreateBarrier();
            IDisplay indoorDisplay = indoorFactory.CreateDisplay();

            Console.WriteLine("Indoor parking zone devices:");
            indoorSensor.DetectVehicle();
            indoorBarrier.Open();
            indoorDisplay.ShowStatus("Indoor parking zone is active");
            indoorBarrier.Close();

            Console.WriteLine("\n------------------------------\n");

            IParkingZoneFactory outdoorFactory = new OutdoorParkingZoneFactory();
            ISensor outdoorSensor = outdoorFactory.CreateSensor();
            IBarrier outdoorBarrier = outdoorFactory.CreateBarrier();
            IDisplay outdoorDisplay = outdoorFactory.CreateDisplay();

            Console.WriteLine("Outdoor parking zone devices:");
            outdoorSensor.DetectVehicle();
            outdoorBarrier.Open();
            outdoorDisplay.ShowStatus("Outdoor parking zone is active");
            outdoorBarrier.Close();
        }
    }
}
