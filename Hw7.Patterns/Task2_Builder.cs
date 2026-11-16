// Завдання 2. В прикладі до шаблону «Builder» додати клас MargaritaPizzaBuilder.
// Новий будівельник визначає власні кроки; «директор» Waiter і клас Pizza не змінюються.

namespace Hw7.Patterns.Builder;

// Product
public class Pizza
{
	private string _dough = string.Empty;
	private string _sauce = string.Empty;
	private string _topping = string.Empty;

	public void SetDough(string dough) => _dough = dough;

	public void SetSauce(string sauce) => _sauce = sauce;

	public void SetTopping(string topping) => _topping = topping;

	public void Info() => Console.WriteLine($"Dough: {_dough}\nSauce: {_sauce}\nTopping: {_topping}");
}

// Abstract Builder
public abstract class PizzaBuilder
{
	protected Pizza Pizza { get; private set; } = new();

	public Pizza GetPizza() => Pizza;

	public void CreateNewPizza() => Pizza = new Pizza();

	public abstract void BuildDough();

	public abstract void BuildSauce();

	public abstract void BuildTopping();
}

// Concrete Builder
public class HawaiianPizzaBuilder : PizzaBuilder
{
	public override void BuildDough() => Pizza.SetDough("cross");

	public override void BuildSauce() => Pizza.SetSauce("mild");

	public override void BuildTopping() => Pizza.SetTopping("ham+pineapple");
}

// Concrete Builder
public class SpicyPizzaBuilder : PizzaBuilder
{
	public override void BuildDough() => Pizza.SetDough("pan baked");

	public override void BuildSauce() => Pizza.SetSauce("hot");

	public override void BuildTopping() => Pizza.SetTopping("pepperoni+salami");
}

// Concrete Builder — новий
public class MargaritaPizzaBuilder : PizzaBuilder
{
	public override void BuildDough() => Pizza.SetDough("thin");

	public override void BuildSauce() => Pizza.SetSauce("tomato");

	public override void BuildTopping() => Pizza.SetTopping("mozzarella+basil");
}

// Director: знає порядок кроків, але не знає, яку саме піцу будує
public class Waiter
{
	private PizzaBuilder? _pizzaBuilder;

	public void SetPizzaBuilder(PizzaBuilder builder) => _pizzaBuilder = builder;

	public Pizza GetPizza() => Builder.GetPizza();

	public void ConstructPizza()
	{
		Builder.CreateNewPizza();
		Builder.BuildDough();
		Builder.BuildSauce();
		Builder.BuildTopping();
	}

	private PizzaBuilder Builder =>
		_pizzaBuilder ?? throw new InvalidOperationException("Спочатку виберіть будівельника піци.");
}

public static class BuilderDemo
{
	public static void Run()
	{
		var waiter = new Waiter();
		PizzaBuilder[] builders = { new HawaiianPizzaBuilder(), new SpicyPizzaBuilder(), new MargaritaPizzaBuilder() };

		foreach (PizzaBuilder builder in builders)
		{
			Console.WriteLine($"--- {builder.GetType().Name} ---");
			waiter.SetPizzaBuilder(builder);
			waiter.ConstructPizza();
			waiter.GetPizza().Info();
		}
	}
}
