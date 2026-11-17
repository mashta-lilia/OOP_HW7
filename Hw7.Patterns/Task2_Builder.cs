// Завдання 2. В прикладі до шаблону «Builder» додати клас MargaritaPizzaBuilder.
// Другий топінг — необов'язковий крок (virtual з порожньою реалізацією).

namespace Hw7.Patterns.Builder;

// Product
public class Pizza
{
	private string _dough = string.Empty;
	private string _sauce = string.Empty;
	private string _topping = string.Empty;
	private string _topping2 = string.Empty;

	public void SetDough(string dough) => _dough = dough;

	public void SetSauce(string sauce) => _sauce = sauce;

	public void SetTopping(string topping) => _topping = topping;

	public void SetTopping2(string topping) => _topping2 = topping;

	public void Info()
	{
		Console.WriteLine($"Dough: {_dough}\nSauce: {_sauce}\nTopping: {_topping}");
		if (_topping2 != string.Empty)
		{
			Console.WriteLine($"Topping2: {_topping2}");
		}
	}
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

	// Необов'язковий крок: за замовчуванням нічого не робить
	public virtual void BuildTopping2() { }
}

// Concrete Builder — без другого топінгу
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

	public override void BuildTopping2() => Pizza.SetTopping2("chili");
}

// Concrete Builder — новий
public class MargaritaPizzaBuilder : PizzaBuilder
{
	public override void BuildDough() => Pizza.SetDough("thin");

	public override void BuildSauce() => Pizza.SetSauce("tomato");

	public override void BuildTopping() => Pizza.SetTopping("doubled cheese");

	public override void BuildTopping2() => Pizza.SetTopping2("basil");
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
		Builder.BuildTopping2();
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
