using System;

namespace magooplabs
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
            Console.WriteLine("IndoorSensor: Vehicle inside Indoor parking zone.");
        }
    }

    public class IndoorBarrier : IBarrier
    {
        public void Open()
        {
            Console.WriteLine("IndoorBarrier: Opened Barrier for indoor parking zone.");
        }

        public void Close()
        {
            Console.WriteLine("IndoorBarrier: Closed Barrier for indoor parking zone.");
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
            Console.WriteLine("OutdoorSensor: Vehicle inside outdoor parking zone.");
        }
    }

    public class OutdoorBarrier : IBarrier
    {
        public void Open()
        {
            Console.WriteLine("OutdoorBarrier: Opened Barrier for outdoor parking zone.");
        }

        public void Close()
        {
            Console.WriteLine("OutdoorBarrier: Closed Barrier for outdoor parking zone.");
        }
    }

    public class OutdoorDisplay : IDisplay
    {
        public void ShowStatus(string message)
        {
            Console.WriteLine("OutdoorDisplay: " + message);
        }
    }
    // abstract
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
    // test
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Test\n");
            IParkingZoneFactory indoorFactory = new IndoorParkingZoneFactory();
            ISensor indoorSensor = indoorFactory.CreateSensor();
            IBarrier indoorBarrier = indoorFactory.CreateBarrier();
            IDisplay indoorDisplay = indoorFactory.CreateDisplay();

            Console.WriteLine("Indoor devices:");
            indoorSensor.DetectVehicle();
            indoorBarrier.Open();
            indoorDisplay.ShowStatus("Indoor parking zone is active.");
            indoorBarrier.Close();

            Console.WriteLine("\n------------------------------\n");
            IParkingZoneFactory outdoorFactory = new OutdoorParkingZoneFactory();
            ISensor outdoorSensor = outdoorFactory.CreateSensor();
            IBarrier outdoorBarrier = outdoorFactory.CreateBarrier();
            IDisplay outdoorDisplay = outdoorFactory.CreateDisplay();

            Console.WriteLine("Outdoor devices:");
            outdoorSensor.DetectVehicle();
            outdoorBarrier.Open();
            outdoorDisplay.ShowStatus("Outdoor parking zone is active.");
            outdoorBarrier.Close(); 
        }
    }
}
