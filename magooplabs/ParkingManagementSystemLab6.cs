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

class Parser
{
    private Queue<string> tokens;

    public Parser(string expression)
    {
        tokens = new Queue<string>(Tokenize(expression));
    }

    private IEnumerable<string> Tokenize(string expr)
    {
        var tokens = new List<string>();
        var token = "";
        for (int i = 0; i < expr.Length; i++)
        {
            char c = expr[i];
            if (char.IsWhiteSpace(c))
            {
                if (token != "")
                {
                    tokens.Add(token);
                    token = "";
                }
            }
            else if (c == '(' || c == ')')
            {
                if (token != "")
                {
                    tokens.Add(token);
                    token = "";
                }
                tokens.Add(c.ToString());
            }
            else
            {
                token += c;
            }
        }
        if (token != "")
            tokens.Add(token);
        return tokens;
    }

    public IExpression ParseExpression()
    {
        return ParseOr();
    }

    private IExpression ParseOr()
    {
        IExpression left = ParseAnd();
        while (tokens.Count > 0 && tokens.Peek().ToUpper() == "OR")
        {
            tokens.Dequeue(); // consume OR
            IExpression right = ParseAnd();
            left = new OrExpression(left, right);
        }
        return left;
    }

    private IExpression ParseAnd()
    {
        IExpression left = ParseTerm();
        while (tokens.Count > 0 && tokens.Peek().ToUpper() == "AND")
        {
            tokens.Dequeue(); // consume AND
            IExpression right = ParseTerm();
            left = new AndExpression(left, right);
        }
        return left;
    }

    private IExpression ParseTerm()
    {
        if (tokens.Count == 0)
            throw new Exception("Unexpected end of expression");

        string token = tokens.Dequeue();

        if (token == "(")
        {
            IExpression expr = ParseExpression();
            if (tokens.Count == 0 || tokens.Dequeue() != ")")
                throw new Exception("Missing closing parenthesis");
            return expr;
        }
        else
        {
            return new TerminalExpression(token);
        }
    }
}

// LAB 6 DEMO 
public class Lab6Demo
{
    public static void RunDemo()
    {
        Console.WriteLine("\n Lab6: Interpreter + Mediator + Parser \n");

        // Інтерпретатор
        IExpression car = new TerminalExpression("car");
        IExpression bike = new TerminalExpression("bike");
        IExpression isVehicle = new OrExpression(car, bike);

        Console.WriteLine("\"car\" in context => " + isVehicle.Interpret("car enters"));
        Console.WriteLine("\"bus\" in context => " + isVehicle.Interpret("bus enters"));

        // Parser usage demo
        var parser = new Parser("car OR bike AND (bus OR train)");
        IExpression parsedExpression = parser.ParseExpression();

        Console.WriteLine("\"car\" in context => " + parsedExpression.Interpret("car enters"));
        Console.WriteLine("\"bus\" in context => " + parsedExpression.Interpret("bus enters"));
        Console.WriteLine("\"train\" in context => " + parsedExpression.Interpret("train arrives"));
        Console.WriteLine("\"bike\" in context => " + parsedExpression.Interpret("bike rides"));
        Console.WriteLine("\"plane\" in context => " + parsedExpression.Interpret("plane flies"));

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
