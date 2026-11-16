// Завдання 5. Додати реалізацію класу «Трикутник» у прикладі до шаблону Prototype.
// Кожна фігура сама вміє створити свою копію (Clone), тому клієнт копіює фігуру,
// не знаючи її конкретного класу.

namespace Hw7.Patterns.Prototype;

public interface IFigure
{
	IFigure Clone();

	void GetInfo();
}

public class Rectangle : IFigure
{
	private readonly int _width;
	private readonly int _height;

	public Rectangle(int width, int height)
	{
		_width = width;
		_height = height;
	}

	public IFigure Clone() => new Rectangle(_width, _height);

	// У прикладі з посібника довжина й ширина були переплутані місцями — тут виправлено.
	public void GetInfo() => Console.WriteLine($"Прямокутник шириною {_width} і висотою {_height}");
}

public class Circle : IFigure
{
	private readonly int _radius;

	public Circle(int radius)
	{
		_radius = radius;
	}

	public IFigure Clone() => new Circle(_radius);

	public void GetInfo() => Console.WriteLine($"Круг радіусом {_radius}");
}

// Новий клас
public class Triangle : IFigure
{
	private readonly int _a;
	private readonly int _b;
	private readonly int _c;

	public Triangle(int a, int b, int c)
	{
		// Нерівність трикутника: кожна сторона менша за суму двох інших.
		if (a <= 0 || b <= 0 || c <= 0 || a >= b + c || b >= a + c || c >= a + b)
		{
			throw new ArgumentException($"Трикутника зі сторонами {a}, {b}, {c} не існує.");
		}

		_a = a;
		_b = b;
		_c = c;
	}

	public IFigure Clone() => new Triangle(_a, _b, _c);

	public void GetInfo() => Console.WriteLine($"Трикутник зі сторонами {_a}, {_b}, {_c}");
}

public static class PrototypeDemo
{
	public static void Run()
	{
		IFigure[] figures = { new Rectangle(10, 20), new Circle(15), new Triangle(3, 4, 5) };

		foreach (IFigure figure in figures)
		{
			IFigure clone = figure.Clone();
			figure.GetInfo();
			clone.GetInfo();
			Console.WriteLine($"  Це різні об'єкти: {!ReferenceEquals(figure, clone)}");
		}
	}
}
