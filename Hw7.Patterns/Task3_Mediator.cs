// Завдання 3. В прикладі до шаблону «Mediator» додати клас ConcreteColleague3 і повідомлення від нього «Hello!».
// Колеги не знають один про одного: кожен надсилає повідомлення посереднику,
// а посередник вирішує, кому його доставити. Тепер учасників троє, тому посередник
// пересилає повідомлення всім, крім відправника.

namespace Hw7.Patterns.Mediator;

// "Mediator"
public abstract class Mediator
{
	public abstract void Send(string message, Colleague sender);
}

// "Colleague"
public abstract class Colleague
{
	private readonly Mediator _mediator;

	protected Colleague(Mediator mediator, string name)
	{
		_mediator = mediator;
		Name = name;
	}

	public string Name { get; }

	public void Send(string message)
	{
		Console.WriteLine($"{Name} sends: {message}");
		_mediator.Send(message, this);
	}

	public void Notify(string message) => Console.WriteLine($"  {Name} gets message: {message}");
}

// "ConcreteColleague1"
public class ConcreteColleague1 : Colleague
{
	public ConcreteColleague1(Mediator mediator)
		: base(mediator, "Colleague1")
	{
	}
}

// "ConcreteColleague2"
public class ConcreteColleague2 : Colleague
{
	public ConcreteColleague2(Mediator mediator)
		: base(mediator, "Colleague2")
	{
	}
}

// "ConcreteColleague3" — новий учасник
public class ConcreteColleague3 : Colleague
{
	public ConcreteColleague3(Mediator mediator)
		: base(mediator, "Colleague3")
	{
	}
}

// "ConcreteMediator"
public class ConcreteMediator : Mediator
{
	public ConcreteColleague1? Colleague1 { get; set; }

	public ConcreteColleague2? Colleague2 { get; set; }

	public ConcreteColleague3? Colleague3 { get; set; }

	public override void Send(string message, Colleague sender)
	{
		// Повідомлення отримують усі учасники, крім відправника.
		foreach (Colleague? colleague in new Colleague?[] { Colleague1, Colleague2, Colleague3 })
		{
			if (colleague is not null && colleague != sender)
			{
				colleague.Notify(message);
			}
		}
	}
}

public static class MediatorDemo
{
	public static void Run()
	{
		var mediator = new ConcreteMediator();
		var c1 = new ConcreteColleague1(mediator);
		var c2 = new ConcreteColleague2(mediator);
		var c3 = new ConcreteColleague3(mediator);

		mediator.Colleague1 = c1;
		mediator.Colleague2 = c2;
		mediator.Colleague3 = c3;

		c1.Send("How are you?");
		c2.Send("Fine, thanks");
		c3.Send("Hello!");
	}
}
