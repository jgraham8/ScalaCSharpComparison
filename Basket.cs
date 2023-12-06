namespace ScalaCSharpComparison;
public class Basket
{
    public Basket()
    {
        BasketLines = new();
    }

    public List<BasketLine> BasketLines { get; set; }

    public void AddFood(Food food, int quantity)
    {
        BasketLines.Add(new BasketLine(food, quantity));
    }
    public void RemoveFood(Food food, int quantity)
    {
        var basketLine = BasketLines.FirstOrDefault(x => x.Food.Name == food.Name);
        if (basketLine != null)
        {
            basketLine.Quantity -= quantity;
            if (basketLine.Quantity <= 0)
            {
                BasketLines.Remove(basketLine);
            }
        }
    }

    public decimal GetTotalPriceInPounds()
    {
        return BasketLines.Sum(x => x.getTotalPriceinPounds());
    }

    public void PrintBasket()
    {
        foreach (var basketLine in BasketLines)
        {
            Console.WriteLine($"{basketLine.Food.Name} x {basketLine.Quantity} = {basketLine.getTotalPriceinPounds():#.##}");
        }
        Console.WriteLine($"Total: {GetTotalPriceInPounds():#.##}");
    }
}

public class BasketLine
{
    public BasketLine(Food food, int quantity)
    {
        Food = food;
        Quantity = quantity;
    }

    public Food Food { get; set; }
    public int Quantity { get; set; }

    public decimal getTotalPriceinPounds()
    {
        return Food.GetCurrentPriceInPounds() * Quantity;
    }
}