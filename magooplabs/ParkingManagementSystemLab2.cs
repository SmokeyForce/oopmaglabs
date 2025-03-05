using System;

namespace ParkingManagementSystem.Lab2
{
    // prototype
    public class ParkingZoneConfiguration : ICloneable
    {
        public string ZoneName { get; set; }
        public int TotalSpaces { get; set; }
        public string SensorType { get; set; }
        public string BarrierType { get; set; }
        public string DisplayType { get; set; }
        public bool HasDynamicPricing { get; set; }
        public string AdditionalSettings { get; set; }

        // поверхневе клонування
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public override string ToString()
        {
            return $"Zone: {ZoneName}\n" +
                   $"Number of Spaces: {TotalSpaces}\n" +
                   $"Sensor Type: {SensorType}\n" +
                   $"Barrier Type: {BarrierType}\n" +
                   $"Display Type: {DisplayType}\n" +
                   $"Dynamic Pricing: {HasDynamicPricing}\n" +
                   $"Additional Settings: {AdditionalSettings}\n";
        }
    }

    // parking zone configuration
    public class ParkingZoneBuilder
    {
        private ParkingZoneConfiguration _config = new ParkingZoneConfiguration();

        public ParkingZoneBuilder SetZoneName(string zoneName)
        {
            _config.ZoneName = zoneName;
            return this;
        }

        public ParkingZoneBuilder SetTotalSpaces(int spaces)
        {
            _config.TotalSpaces = spaces;
            return this;
        }

        public ParkingZoneBuilder SetSensorType(string sensorType)
        {
            _config.SensorType = sensorType;
            return this;
        }

        public ParkingZoneBuilder SetBarrierType(string barrierType)
        {
            _config.BarrierType = barrierType;
            return this;
        }

        public ParkingZoneBuilder SetDisplayType(string displayType)
        {
            _config.DisplayType = displayType;
            return this;
        }

        public ParkingZoneBuilder EnableDynamicPricing(bool enable)
        {
            _config.HasDynamicPricing = enable;
            return this;
        }

        public ParkingZoneBuilder SetAdditionalSettings(string settings)
        {
            _config.AdditionalSettings = settings;
            return this;
        }

        public ParkingZoneConfiguration Build()
        {
            return _config;
        }
    }

    public static class Lab2Demo
    {
        public static void RunDemo()
        {
            Console.WriteLine("Lab2 Demo: Prototype та Builder Patterns\n");

            // Створення конфігурації за допомогою Builder
            ParkingZoneBuilder builder = new ParkingZoneBuilder();
            ParkingZoneConfiguration originalConfig = builder
                .SetZoneName("Central Parking Zone")
                .SetTotalSpaces(200)
                .SetSensorType("Induction sensor")
                .SetBarrierType("Automatic Barrier")
                .SetDisplayType("LCD display")
                .EnableDynamicPricing(true)
                .SetAdditionalSettings("Settings for VIP")
                .Build();

            Console.WriteLine("Original configuration (Builder):");
            Console.WriteLine(originalConfig);

            // Клонування конфігурації за допомогою Прототипу та її модифікація
            ParkingZoneConfiguration clonedConfig = (ParkingZoneConfiguration)originalConfig.Clone();
            clonedConfig.ZoneName = "South Parking Zone";
            clonedConfig.TotalSpaces = 150;
            clonedConfig.HasDynamicPricing = false;

            Console.WriteLine("Cloned Configuration (Prototype):");
            Console.WriteLine(clonedConfig);

            Console.WriteLine("Lab2 Demo is over.\n");
        }
    }
}
