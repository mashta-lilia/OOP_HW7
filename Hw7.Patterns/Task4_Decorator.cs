// Завдання 4. Клас «Ялинка», який за необхідності можна декорувати ялинковими прикрасами (поля)
// та/або гірляндами (метод, що відповідає за те, що ялинка може світитися). Шаблон Decorator.
//
// Кожен декоратор сам є ялинкою (ChristmasTree) і містить ялинку всередині. Тому прикраси можна
// додавати в будь-якій кількості й порядку, не створюючи підкласів на кожну комбінацію.

namespace Hw7.Patterns.Decorator;

// "Component"
public abstract class ChristmasTree
{
	public abstract string Describe();

	public abstract bool CanGlow { get; }

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
		if (base.CanGlow)
		{
			base.Glow();
		}

		Console.WriteLine($"  Світиться {_color} гірлянда ✨");
	}
}

public static class RandomDecorator
{
	private static readonly Random Rnd = new();

	private static readonly string[] Kinds = { "кульки", "шишки", "зірочки", "бантики", "сніжинки" };
	private static readonly string[] Colors = { "червоні", "золоті", "срібні", "сині", "білі" };
	private static readonly string[] GarlandColors = { "біла", "синя", "різнокольорова", "жовта" };

	public static ChristmasTree Decorate(ChristmasTree tree)
	{
		int layers = Rnd.Next(0, 5);

		for (int i = 0; i < layers; i++)
		{
			if (Rnd.Next(2) == 0)
			{
				tree = new Ornaments(
					tree,
					Kinds[Rnd.Next(Kinds.Length)],
					Rnd.Next(5, 31),
					Colors[Rnd.Next(Colors.Length)]);
			}
			else
			{
				tree = new Garland(tree, GarlandColors[Rnd.Next(GarlandColors.Length)]);
			}
		}

		return tree;
	}
}

public static class DecoratorDemo
{
	public static void Run()
	{
		for (int i = 1; i <= 3; i++)
		{
			ChristmasTree tree = RandomDecorator.Decorate(new FirTree(1.5 + i * 0.3));
			Console.WriteLine($"Ялинка №{i}: {tree.Describe()}");
			tree.Glow();
			Console.WriteLine();
		}
	}
}
