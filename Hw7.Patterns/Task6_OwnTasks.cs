// Завдання 6. Придумати і виконати завдання до інших шаблонів (два власні завдання).
//
// 6а. Factory Method. У прикладі з посібника ConcreteCreator вибирає продукт через switch,
//     тому щоб додати новий продукт, треба змінювати вже написаний код.
//     Завдання: додати продукт C так, щоб наявні класи не змінювались
//     (кожен продукт має свого творця — принцип відкритості/закритості).
//
// 6б. Adapter. У прикладі адаптується лише стара радянська розетка.
//     Завдання: додати ще одну несумісну систему (американська розетка 110 В)
//     і свій адаптер для неї, не змінюючи клієнта ElectricityConsumer.

namespace Hw7.Patterns.OwnTasks;

// ===================== 6а. Factory Method =====================

// Product
public abstract class Document
{
	public abstract string Name { get; }
}

public class ReportDocument : Document
{
	public override string Name => "Звіт";
}

public class InvoiceDocument : Document
{
	public override string Name => "Рахунок";
}

// Новий продукт — додано без змін у класах вище
public class ContractDocument : Document
{
	public override string Name => "Договір";
}

// Creator: містить спільну логіку, а що саме створювати, вирішують підкласи
public abstract class DocumentCreator
{
	// Фабричний метод
	protected abstract Document CreateDocument();

	public void Print()
	{
		Document document = CreateDocument();
		Console.WriteLine($"  Створено документ «{document.Name}» ({document.GetType().Name})");
	}
}

public class ReportCreator : DocumentCreator
{
	protected override Document CreateDocument() => new ReportDocument();
}

public class InvoiceCreator : DocumentCreator
{
	protected override Document CreateDocument() => new InvoiceDocument();
}

// Новий творець
public class ContractCreator : DocumentCreator
{
	protected override Document CreateDocument() => new ContractDocument();
}

// ===================== 6б. Adapter =====================

// Adaptee 1 (з посібника)
public class OldElectricitySystem
{
	public string MatchThinSocket() => "220 В, стара вузька розетка";
}

// Adaptee 2 — нова несумісна система
public class AmericanElectricitySystem
{
	public int Voltage => 110;

	public string MatchFlatPins() => $"{Voltage} В, американська розетка з пласкими контактами";
}

// Target — інтерфейс, який очікує клієнт
public interface INewElectricitySystem
{
	string MatchWideSocket();
}

public class NewElectricitySystem : INewElectricitySystem
{
	public string MatchWideSocket() => "220 В, сучасна євророзетка";
}

// Adapter 1
public class OldSystemAdapter : INewElectricitySystem
{
	private readonly OldElectricitySystem _adaptee;

	public OldSystemAdapter(OldElectricitySystem adaptee)
	{
		_adaptee = adaptee;
	}

	public string MatchWideSocket() => $"{_adaptee.MatchThinSocket()} → через перехідник";
}

// Adapter 2 — новий: окрім форми вилки, ще й підвищує напругу
public class AmericanSystemAdapter : INewElectricitySystem
{
	private readonly AmericanElectricitySystem _adaptee;

	public AmericanSystemAdapter(AmericanElectricitySystem adaptee)
	{
		_adaptee = adaptee;
	}

	public string MatchWideSocket() =>
		$"{_adaptee.MatchFlatPins()} → трансформатор {_adaptee.Voltage}→220 В";
}

// Client — не змінювався
public static class ElectricityConsumer
{
	public static void ChargeNotebook(INewElectricitySystem socket) =>
		Console.WriteLine($"  Ноутбук заряджається: {socket.MatchWideSocket()}");
}

public static class OwnTasksDemo
{
	public static void Run()
	{
		Console.WriteLine("6а. Factory Method:");
		DocumentCreator[] creators = { new ReportCreator(), new InvoiceCreator(), new ContractCreator() };
		foreach (DocumentCreator creator in creators)
		{
			creator.Print();
		}

		Console.WriteLine();
		Console.WriteLine("6б. Adapter:");
		ElectricityConsumer.ChargeNotebook(new NewElectricitySystem());
		ElectricityConsumer.ChargeNotebook(new OldSystemAdapter(new OldElectricitySystem()));
		ElectricityConsumer.ChargeNotebook(new AmericanSystemAdapter(new AmericanElectricitySystem()));
	}
}
