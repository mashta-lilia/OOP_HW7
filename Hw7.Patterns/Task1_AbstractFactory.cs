// Завдання 1. В прикладі до шаблону «Абстрактна фабрика» додати конкретну фабрику Mercedes.
// Нове сімейство = новий автомобіль + новий двигун + нова фабрика. Наявні класи не змінюються.

namespace Hw7.Patterns.AbstractFactory;

// AbstractProductA
public abstract class Car
{
	public abstract void Info();
}

// ConcreteProductA1
public class Ford : Car
{
	public override void Info() => Console.WriteLine("Ford");
}

// ConcreteProductA2
public class Toyota : Car
{
	public override void Info() => Console.WriteLine("Toyota");
}

// ConcreteProductA3 — новий продукт
public class Mercedes : Car
{
	public override void Info() => Console.WriteLine("Mercedes");
}

// AbstractProductB
public abstract class Engine
{
	public abstract void GetPower();
}

// ConcreteProductB1
public class FordEngine : Engine
{
	public override void GetPower() => Console.WriteLine("Ford Engine 4.4");
}

// ConcreteProductB2
public class ToyotaEngine : Engine
{
	public override void GetPower() => Console.WriteLine("Toyota Engine 3.2");
}

// ConcreteProductB3 — новий продукт
public class MercedesEngine : Engine
{
	public override void GetPower() => Console.WriteLine("Mercedes Engine 5.0");
}

// AbstractFactory
public interface ICarFactory
{
	Car CreateCar();

	Engine CreateEngine();
}

// ConcreteFactory1
public class FordFactory : ICarFactory
{
	public Car CreateCar() => new Ford();

	public Engine CreateEngine() => new FordEngine();
}

// ConcreteFactory2
public class ToyotaFactory : ICarFactory
{
	public Car CreateCar() => new Toyota();

	public Engine CreateEngine() => new ToyotaEngine();
}

// ConcreteFactory3 — нова фабрика: створює машину і двигун одного сімейства
public class MercedesFactory : ICarFactory
{
	public Car CreateCar() => new Mercedes();

	public Engine CreateEngine() => new MercedesEngine();
}

public static class AbstractFactoryDemo
{
	public static void Run()
	{
		// Клієнт працює лише з ICarFactory, Car і Engine — конкретні класи йому не відомі.
		ICarFactory[] factories = { new ToyotaFactory(), new FordFactory(), new MercedesFactory() };

		foreach (ICarFactory factory in factories)
		{
			Car car = factory.CreateCar();
			Engine engine = factory.CreateEngine();
			car.Info();
			engine.GetPower();
		}
	}
}
