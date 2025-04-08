using System;
using System.Collections.Generic;

namespace ParkingManagementSystem.Lab4
{
    // Macro Command
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
    public class OpenBarrierCommand : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("Barrier opened");
        }

        public void Undo()
        {
            Console.WriteLine("Barrier closed");
        }
    }
    public class TurnOnTrafficLightCommand : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("Traffic Light is turned on");
        }

        public void Undo()
        {
            Console.WriteLine("Traffic Light is turned off");
        }
    }
    public class MacroCommand : ICommand
    {
        private List<ICommand> commands = new List<ICommand>();

        public void AddCommand(ICommand command)
        {
            commands.Add(command);
        }

        public void Execute()
        {
            Console.WriteLine("Macrocommand execute:");
            foreach (var command in commands)
            {
                command.Execute();
            }
        }

        public void Undo()
        {
            Console.WriteLine("Undo Macrocommand:");
            for (int i = commands.Count - 1; i >= 0; i--)
            {
                commands[i].Undo();
            }
        }
    }
    // Template Method
    public abstract class ParkingPaymentProcessor
    {
        public void ProcessPayment()
        {
            SelectPaymentMethod();
            EnterPaymentDetails();
            ConfirmPayment();
            PrintReceipt();
        }

        protected abstract void SelectPaymentMethod();
        protected abstract void EnterPaymentDetails();

        protected virtual void ConfirmPayment()
        {
            Console.WriteLine("Payment confirmed");
        }

        protected virtual void PrintReceipt()
        {
            Console.WriteLine("Receipt printed");
        }
    }
    public class CardPaymentProcessor : ParkingPaymentProcessor
    {
        protected override void SelectPaymentMethod()
        {
            Console.WriteLine("Selected card payment");
        }

        protected override void EnterPaymentDetails()
        {
            Console.WriteLine("Card details entered");
        }
    }
    public class MobileAppPaymentProcessor : ParkingPaymentProcessor
    {
        protected override void SelectPaymentMethod()
        {
            Console.WriteLine("Payment through ");
        }

        protected override void EnterPaymentDetails()
        {
            Console.WriteLine("Authorisation via mobile app");
        }

        protected override void ConfirmPayment()
        {
            Console.WriteLine("Confirming via sms");
            base.ConfirmPayment();
        }
    }

    public static class Lab4Demo
    {
        public static void RunDemo()
        {
            Console.WriteLine("Lab4 Demo: Macro Command and Template Method Patterns\n");
            Console.WriteLine(" Macro Command:");
            MacroCommand macroCommand = new MacroCommand();
            macroCommand.AddCommand(new OpenBarrierCommand());
            macroCommand.AddCommand(new TurnOnTrafficLightCommand());

            macroCommand.Execute();
            macroCommand.Undo();

            Console.WriteLine("\n-----------------------------\n");

            Console.WriteLine("Template Method:");
            ParkingPaymentProcessor cardPayment = new CardPaymentProcessor();
            ParkingPaymentProcessor mobilePayment = new MobileAppPaymentProcessor();

            Console.WriteLine("\nCard payment:");
            cardPayment.ProcessPayment();

            Console.WriteLine("\nMobile app payment:");
            mobilePayment.ProcessPayment();

            Console.WriteLine("\nLab4 is over.\n");
        }
    }
}
