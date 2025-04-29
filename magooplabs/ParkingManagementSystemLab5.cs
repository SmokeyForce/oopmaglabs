using System;
using System.Collections;
using System.Collections.Generic;

namespace ParkingManagementSystem.Lab5
{
    // Iterator

    public class ParkingSpot
    {
        public int SpotNumber { get; }
        public bool IsOccupied { get; }

        public ParkingSpot(int number, bool isOccupied)
        {
            SpotNumber = number;
            IsOccupied = isOccupied;
        }

        public override string ToString()
        {
            return $"Spot {SpotNumber}: {(IsOccupied ? "Occupied" : "Free")}";
        }
    }

    public class ParkingLot : IEnumerable<ParkingSpot>
    {
        private List<ParkingSpot> spots = new List<ParkingSpot>();

        public void AddSpot(ParkingSpot spot) => spots.Add(spot);

        public IEnumerator<ParkingSpot> GetEnumerator() => spots.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    // State

    public interface IGateState
    {
        void Enter(ParkingGate gate);
        void Exit(ParkingGate gate);
    }

    public class OpenGateState : IGateState
    {
        public void Enter(ParkingGate gate)
        {
            Console.WriteLine("Entry is allowed");
            gate.SetState(new ClosedGateState());
        }

        public void Exit(ParkingGate gate)
        {
            Console.WriteLine("Exit is allowed");
            gate.SetState(new ClosedGateState());
        }
    }

    public class ClosedGateState : IGateState
    {
        public void Enter(ParkingGate gate)
        {
            Console.WriteLine("Entry is prohibited. Open the gate");
        }

        public void Exit(ParkingGate gate)
        {
            Console.WriteLine("Exit is prohibited. Open the gate");
        }
    }

    public class ParkingGate
    {
        private IGateState _state;

        public ParkingGate()
        {
            _state = new ClosedGateState();
        }

        public void SetState(IGateState state)
        {
            _state = state;
        }

        public void OpenGate()
        {
            Console.WriteLine(" Opening gate...");
            SetState(new OpenGateState());
        }

        public void Enter() => _state.Enter(this);
        public void Exit() => _state.Exit(this);
    }

    // Chain of Responsibility


    public abstract class ParkingRequestHandler
    {
        protected ParkingRequestHandler NextHandler;

        public void SetNext(ParkingRequestHandler handler)
        {
            NextHandler = handler;
        }

        public abstract void Handle(string request);
    }

    public class AuthenticationHandler : ParkingRequestHandler
    {
        public override void Handle(string request)
        {
            if (request == "auth")
            {
                Console.WriteLine("Auth success.");
            }
            else if (NextHandler != null)
            {
                NextHandler.Handle(request);
            }
        }
    }

    public class PaymentHandler : ParkingRequestHandler
    {
        public override void Handle(string request)
        {
            if (request == "payment")
            {
                Console.WriteLine("Payment success.");
            }
            else if (NextHandler != null)
            {
                NextHandler.Handle(request);
            }
        }
    }

    public class AccessHandler : ParkingRequestHandler
    {
        public override void Handle(string request)
        {
            if (request == "access")
            {
                Console.WriteLine("Access allowed");
            }
            else
            {
                Console.WriteLine("Unknown operation");
            }
        }
    }

    // DEMO


    public static class Lab5Demo
    {
        public static void RunDemo()
        {
            Console.WriteLine("Lab5 Demo: Iterator, State, Chain of Responsibility\n");

            // iterator
            Console.WriteLine("Iterator:");
            ParkingLot lot = new ParkingLot();
            lot.AddSpot(new ParkingSpot(1, false));
            lot.AddSpot(new ParkingSpot(2, true));
            lot.AddSpot(new ParkingSpot(3, false));

            foreach (var spot in lot)
            {
                Console.WriteLine(spot);
            }

            Console.WriteLine("\nState:");
            ParkingGate gate = new ParkingGate();
            gate.Enter();         // Спроба в'їзду з закритим шлагбаумом
            gate.OpenGate();      // Відкриття шлагбауму
            gate.Enter();         // Тепер в'їзд дозволено

            Console.WriteLine("\nChain of Responsibility:");
            var auth = new AuthenticationHandler();
            var pay = new PaymentHandler();
            var access = new AccessHandler();

            auth.SetNext(pay);
            pay.SetNext(access);

            auth.Handle("auth");
            auth.Handle("payment");
            auth.Handle("access");
            auth.Handle("unknown");

            Console.WriteLine("\nLab5 demo end.\n");
        }
    }
}
