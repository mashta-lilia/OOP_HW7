// ДЗ 7. Породжувальні та структурні шаблони проєктування (посібник, с. 307–317, завдання 1–6).
// Запуск усіх завдань:            dotnet run --project Hw7.Patterns -- all
// Запуск одного завдання (1..6):  dotnet run --project Hw7.Patterns -- 4
// Без аргументів відкривається меню.

using System.Text;

namespace Hw7.Patterns;

public static class Program
{
	private static readonly (string Title, Action Run)[] Tasks =
	{
		("Абстрактна фабрика: фабрика Mercedes", AbstractFactory.AbstractFactoryDemo.Run),
		("Будівельник: MargaritaPizzaBuilder", Builder.BuilderDemo.Run),
		("Посередник: ConcreteColleague3 і «Hello!»", Mediator.MediatorDemo.Run),
		("Декоратор: ялинка з прикрасами та гірляндами", Decorator.DecoratorDemo.Run),
		("Прототип: трикутник", Prototype.PrototypeDemo.Run),
		("Власні завдання: Factory Method і Adapter", OwnTasks.OwnTasksDemo.Run),
	};

	public static void Main(string[] args)
	{
		Console.OutputEncoding = Encoding.UTF8;

		if (args.Length > 0)
		{
			RunFromArgument(args[0]);
			return;
		}

		RunMenu();
	}

	private static void RunFromArgument(string argument)
	{
		if (argument.Equals("all", StringComparison.OrdinalIgnoreCase))
		{
			for (int i = 0; i < Tasks.Length; i++)
			{
				RunTask(i);
			}

			return;
		}

		if (int.TryParse(argument, out int number) && number >= 1 && number <= Tasks.Length)
		{
			RunTask(number - 1);
			return;
		}

		Console.WriteLine($"Невідоме завдання «{argument}». Вкажіть 1–{Tasks.Length} або all.");
	}

	private static void RunMenu()
	{
		while (true)
		{
			Console.WriteLine();
			Console.WriteLine("ДЗ 7 — шаблони проєктування");
			for (int i = 0; i < Tasks.Length; i++)
			{
				Console.WriteLine($"  {i + 1}. {Tasks[i].Title}");
			}

			Console.WriteLine("  0. Вихід");
			Console.Write("Оберіть завдання: ");

			string? input = Console.ReadLine();
			if (input is null || input.Trim() == "0")
			{
				return;
			}

			if (int.TryParse(input, out int number) && number >= 1 && number <= Tasks.Length)
			{
				RunTask(number - 1);
			}
			else
			{
				Console.WriteLine("Введіть номер зі списку.");
			}
		}
	}

	private static void RunTask(int index)
	{
		Console.WriteLine();
		Console.WriteLine($"===== Завдання {index + 1}. {Tasks[index].Title} =====");
		Tasks[index].Run();
	}
}
