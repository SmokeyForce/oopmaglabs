using System;
using System.Collections.Generic;

namespace ParkingManagementSystem.Lab3
{
    // ========================================================
    // Strategy
    // ========================================================
    public interface IPricingStrategy
    {
        decimal CalculatePrice(int hours);
    }
    public class FixedRateStrategy : IPricingStrategy
    {
        private decimal ratePerHour;
        public FixedRateStrategy(decimal ratePerHour)
        {
            this.ratePerHour = ratePerHour;
        }
        public decimal CalculatePrice(int hours)
        {
            return hours * ratePerHour;
        }
    }
    public class DynamicRateStrategy : IPricingStrategy
    {
        public decimal CalculatePrice(int hours)
        {
            decimal rate = (DateTime.Now.Hour >= 8 && DateTime.Now.Hour <= 20) ? 12 : 6;
            return hours * rate;
        }
    }
    public class PricingContext
    {
        private IPricingStrategy pricingStrategy;
        public PricingContext(IPricingStrategy strategy)
        {
            pricingStrategy = strategy;
        }
        public void SetStrategy(IPricingStrategy strategy)
        {
            pricingStrategy = strategy;
        }
        public decimal GetPrice(int hours)
        {
            return pricingStrategy.CalculatePrice(hours);
        }
    }

    // ========================================================
    // Observer
    // ========================================================
    public interface IParkingObserver
    {
        void Update(decimal newRate);
    }
    public class RateNotifier
    {
        private List<IParkingObserver> observers = new List<IParkingObserver>();
        public decimal CurrentRate { get; private set; }
        public void Attach(IParkingObserver observer)
        {
            observers.Add(observer);
        }
        public void Detach(IParkingObserver observer)
        {
            observers.Remove(observer);
        }
        public void SetNewRate(decimal newRate)
        {
            CurrentRate = newRate;
            NotifyObservers();
        }
        private void NotifyObservers()
        {
            foreach (var observer in observers)
            {
                observer.Update(CurrentRate);
            }
        }
    }
    public class ParkingApp : IParkingObserver
    {
        public void Update(decimal newRate)
        {
            Console.WriteLine($"Parking App: New Rate – {newRate} grn/houre");
        }
    }
    public class BillingSystem : IParkingObserver
    {
        public void Update(decimal newRate)
        {
            Console.WriteLine($"Billing System: New Rate – {newRate} grn/houre");
        }
    }

    // ========================================================
    // Command
    // ========================================================
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
    public class ChangeRateCommand : ICommand
    {
        private RateNotifier rateNotifier;
        private decimal oldRate;
        private decimal newRate;

        public ChangeRateCommand(RateNotifier notifier, decimal newRate)
        {
            this.rateNotifier = notifier;
            this.newRate = newRate;
        }

        public void Execute()
        {
            oldRate = rateNotifier.CurrentRate;
            rateNotifier.SetNewRate(newRate);
            Console.WriteLine($"Command: new prices, from {oldRate} to {newRate}");
        }

        public void Undo()
        {
            rateNotifier.SetNewRate(oldRate);
            Console.WriteLine($"Command: prices back to {oldRate}");
        }
    }
    public class PricingInvoker
    {
        private ICommand command;
        public void SetCommand(ICommand command)
        {
            this.command = command;
        }
        public void ExecuteCommand()
        {
            command.Execute();
        }
        public void UndoCommand()
        {
            command.Undo();
        }
    }
    public static class Lab3Demo
    {
        public static void RunDemo()
        {
            Console.WriteLine("Lab3 Demo: Strategy, Observer та Command Patterns\n");
            Console.WriteLine("Strategy:");
            PricingContext pricingContext = new PricingContext(new FixedRateStrategy(10));
            Console.WriteLine($"Fixed rate (10 grn/houre) for 3 hours: {pricingContext.GetPrice(3)} grn");
            pricingContext.SetStrategy(new DynamicRateStrategy());
            Console.WriteLine($"Dynamic rate for 3 hours: {pricingContext.GetPrice(3)} grn");

            Console.WriteLine("\n-----------------------------\n");
            Console.WriteLine("Observer:");
            RateNotifier notifier = new RateNotifier();
            ParkingApp app = new ParkingApp();
            BillingSystem billing = new BillingSystem();
            notifier.Attach(app);
            notifier.Attach(billing);
            // Зміна тарифу – всі підписники отримають оновлення
            notifier.SetNewRate(15);
            Console.WriteLine("\n-----------------------------\n");
            Console.WriteLine("Command:");
            PricingInvoker invoker = new PricingInvoker();
            ICommand changeRateCommand = new ChangeRateCommand(notifier, 20);
            invoker.SetCommand(changeRateCommand);
            invoker.ExecuteCommand();
            invoker.UndoCommand();

            Console.WriteLine("\nLab3 Demo is over\n");
        }
    }
}
