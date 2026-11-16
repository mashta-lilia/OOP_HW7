// Завдання 4. Клас «Ялинка», який за необхідності можна декорувати ялинковими прикрасами (поля)
// та/або гірляндами (метод, що відповідає за те, що ялинка може світитися). Шаблон Decorator.
//
// Кожен декоратор сам є ялинкою (ChristmasTree) і містить ялинку всередині. Тому прикраси можна
// додавати в будь-якій кількості й порядку, не створюючи підкласів на кожну комбінацію.

namespace Hw7.Patterns.Decorator;

// "Component"
public abstract class ChristmasTree
{
	/// <summary>Опис ялинки разом з усіма прикрасами.</summary>
	public abstract string Describe();

	/// <summary>Чи може ялинка світитися (є хоча б одна гірлянда).</summary>
	public abstract bool CanGlow { get; }

	/// <summary>Увімкнути світло. Без гірлянди ялинка не світиться.</summary>
	public abstract void Glow();
}

// "ConcreteComponent" — звичайна ялинка без прикрас
public class FirTree : ChristmasTree
{
	private readonly double _height;

	public FirTree(double height)
	{
		_height = height;
	}

	public override string Describe() => $"Ялинка {_height} м";

	public override bool CanGlow => false;

	public override void Glow() => Console.WriteLine("  Ялинка не світиться: на ній немає гірлянди.");
}

// "Decorator" — за замовчуванням передає все ялинці, яку обгортає
public abstract class TreeDecorator : ChristmasTree
{
	private readonly ChristmasTree _tree;

	protected TreeDecorator(ChristmasTree tree)
	{
		_tree = tree;
	}

	public override string Describe() => _tree.Describe();

	public override bool CanGlow => _tree.CanGlow;

	public override void Glow() => _tree.Glow();
}

// "ConcreteDecoratorA" — прикраси: додають стан (поля)
public class Ornaments : TreeDecorator
{
	private readonly string _kind;
	private readonly int _count;
	private readonly string _color;

	public Ornaments(ChristmasTree tree, string kind, int count, string color)
		: base(tree)
	{
		_kind = kind;
		_count = count;
		_color = color;
	}

	public override string Describe() => $"{base.Describe()} + {_kind} ({_count} шт., {_color})";
}

// "ConcreteDecoratorB" — гірлянда: додає поведінку (метод Glow)
public class Garland : TreeDecorator
{
	private readonly string _color;

	public Garland(ChristmasTree tree, string color)
		: base(tree)
	{
		_color = color;
	}

	public override string Describe() => $"{base.Describe()} + гірлянда ({_color})";

	public override bool CanGlow => true;

	public override void Glow()
	{
		// Якщо всередині вже є гірлянда, вона теж світиться.
		if (base.CanGlow)
		{
			base.Glow();
		}

		Console.WriteLine($"  Світиться {_color} гірлянда ✨");
	}
}

public static class DecoratorDemo
{
	public static void Run()
	{
		ChristmasTree plain = new FirTree(1.8);

		ChristmasTree withToys = new Ornaments(plain, "кульки", 20, "червоні");

		ChristmasTree festive = new Garland(
			new Ornaments(
				new Ornaments(new FirTree(2.2), "кульки", 30, "золоті"),
				"шишки", 10, "срібні"),
			"різнокольорова");

		ChristmasTree twoGarlands = new Garland(new Garland(new FirTree(1.5), "біла"), "синя");

		foreach (ChristmasTree tree in new[] { plain, withToys, festive, twoGarlands })
		{
			Console.WriteLine(tree.Describe());
			tree.Glow();
		}
	}
}
