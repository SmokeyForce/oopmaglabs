using System;
using System.Collections.Generic;

// Інтерпритатор 
interface IExpression
{
    bool Interpret(string context);
}

class TerminalExpression : IExpression
{
    private string _data;

    public TerminalExpression(string data)
    {
        _data = data;
    }

    public bool Interpret(string context)
    {
        return context.Contains(_data);
    }
}

class OrExpression : IExpression
{
    private IExpression _expr1;
    private IExpression _expr2;

    public OrExpression(IExpression expr1, IExpression expr2)
    {
        _expr1 = expr1;
        _expr2 = expr2;
    }

    public bool Interpret(string context)
    {
        return _expr1.Interpret(context) || _expr2.Interpret(context);
    }
}

class AndExpression : IExpression
{
    private IExpression _expr1;
    private IExpression _expr2;

    public AndExpression(IExpression expr1, IExpression expr2)
    {
        _expr1 = expr1;
        _expr2 = expr2;
    }

    public bool Interpret(string context)
    {
        return _expr1.Interpret(context) && _expr2.Interpret(context);
    }
}

// Медіатор
interface IMediator
{
    void Notify(Component sender, string ev);
}

class ParkingMediator : IMediator
{
    public Gate Gate { get; set; }
    public PaymentTerminal Terminal { get; set; }

    public void Notify(Component sender, string ev)
    {
        if (ev == "PaymentMade")
        {
            Console.WriteLine("[Mediator] Payment success. Opening gate");
            Gate.OpenGate();
        }
        else if (ev == "CarEntered")
        {
            Console.WriteLine("[Mediator] Car entered. Closing gate");
            Gate.CloseGate();
        }
    }
}

abstract class Component
{
    protected IMediator _mediator;

    public Component(IMediator mediator)
    {
        _mediator = mediator;
    }
}

class Gate : Component
{
    public Gate(IMediator mediator) : base(mediator) { }

    public void OpenGate()
    {
        Console.WriteLine("[Gate] Gate opened.");
    }

    public void CloseGate()
    {
        Console.WriteLine("[Gate] Gate closed.");
    }

    public void CarEntered()
    {
        _mediator.Notify(this, "CarEntered");
    }
}

class PaymentTerminal : Component
{
    public PaymentTerminal(IMediator mediator) : base(mediator) { }

    public void MakePayment()
    {
        Console.WriteLine("[Terminal] Payment success.");
        _mediator.Notify(this, "PaymentMade");
    }
}

// LAB 6 DEMO 
public class Lab6Demo
{
    public static void RunDemo()
    {
        Console.WriteLine("\n Lab6: Interpreter + Mediator \n");

        // Інтерпретатор
        IExpression car = new TerminalExpression("car");
        IExpression bike = new TerminalExpression("bike");
        IExpression isVehicle = new OrExpression(car, bike);

        Console.WriteLine("\"car\" in context => " + isVehicle.Interpret("car enters"));
        Console.WriteLine("\"bus\" in context => " + isVehicle.Interpret("bus enters"));

        // Медіатор
        var mediator = new ParkingMediator();
        var gate = new Gate(mediator);
        var terminal = new PaymentTerminal(mediator);
        mediator.Gate = gate;
        mediator.Terminal = terminal;

        terminal.MakePayment();
        gate.CarEntered();
    }
}
