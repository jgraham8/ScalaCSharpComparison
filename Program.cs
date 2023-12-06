using ScalaCSharpComparison;

MainMenu();

void MainMenu()
{
    var names = new string[] { "Apple", "Banana", "Orange", "Pear", "Strawberry", "Oil", "Bread" };
    var foods = new List<Food>();

    foreach (var name in names)
    {
        foods.Add(new Food(name));
    }

    string[] options =
    [
        "Get current price for each food",
        "Get highest and lowest prices for each food",
        "Get median price for each food",
        "Get the symbol for the food which has risen most over the last 6 months",
        "Compare average values of two foods",
        "Calculate basket total",
        "Quit"
    ];

    while (true)
    {
        for (int i = 0; i < options.Length; i++)
        {
            Console.WriteLine($"{i}. {options[i]}");
        }

        Console.WriteLine("Please select an option: ");

        string? input = Console.ReadLine();

        Console.WriteLine("\r\n--------\r\n");

        switch (input)
        {
            case "0":
                PrintCurrentPrices(foods);
                break;
            case "1":
                PrintMinMaxPrices(foods);
                break;
            case "2":
                PrintMedianPrices(foods);
                break;
            case "3":
                PrintMostRisenFood(foods);            
                break;
            case "4":
                PrintAveragePrices(foods);
                break;
            case "5":
                PrintBasketTotal(foods);
                break;
            case "6":
                Console.WriteLine("Exiting Application...");
                Environment.Exit(0);
                return;
            default:
                Console.WriteLine("Incorrect Selection");
                break;
        }

        Console.WriteLine("\r\n========\r\n");
    } 
}

void PrintCurrentPrices(List<Food> foods)
{
    foreach (var food in foods)
    {
        Console.WriteLine($"{food.Name}: £{food.GetCurrentPriceInPounds():#.##}");
    }
}

void PrintMinMaxPrices(List<Food> foods)
{
    foreach (var food in foods)
    {
        Console.WriteLine($"{food.Name}: Max = £{food.GetHighestPriceInPounds():#.##} - Min = £{food.GetLowestPriceInPounds():#.##}");
    }
}

void PrintMedianPrices(List<Food> foods)
{
    foreach (var food in foods)
    {
        Console.WriteLine($"{food.Name}: £{food.GetMedianPriceInPounds():#.##}");
    }
}

void PrintMostRisenFood(List<Food> foods)
{
    var mostRisenFood = foods.OrderByDescending(x => x.GetPriceRiseInPounds()).First();
    if (mostRisenFood.GetPriceRiseInPounds() <= 0)
    {
        Console.WriteLine($"no rise");
    }

    Console.WriteLine($"{mostRisenFood.Name} has risen the most over the last 6 months by £{mostRisenFood.GetPriceRiseInPounds():#.##}");
}

void PrintAveragePrices(List<Food> foods)
{
    var food1 = GetFoodUserInput(foods);
    var food2 = GetFoodUserInput(foods);

    Console.WriteLine($"{food1.Name} Avg = £{food1.GetAveragePriceInPounds()} | {food2.Name} Avg = £{food2.GetAveragePriceInPounds():#.##} | Difference = £{Math.Abs(food1.GetAveragePriceInPounds() - food2.GetAveragePriceInPounds()):#.##}");
}

void PrintBasketTotal(List<Food> foods)
{
    var basket = new Basket();

    while (true)
    {
        Console.WriteLine("Please enter the food you would like to add to the basket: ");
        var food = GetFoodUserInput(foods);

        Console.WriteLine("Please enter the quantity you would like to add to the basket: ");
        int quantity = GetQuantityUserInput();

        basket.AddFood(food, quantity);

        Console.WriteLine("Would you like to add another food to the basket? (y/n)");
        var input = Console.ReadLine();

        if (input == "n")
        {
            break;
        }
    }

    basket.PrintBasket();
}

int GetQuantityUserInput()
{
    int quantity = 0;
    while (quantity <= 0)
    {
        var input = Console.ReadLine();
        if (!int.TryParse(input, out quantity))
        {
            Console.WriteLine("Please enter a valid quantity");
        }
    }

    return quantity;
}

Food GetFoodUserInput(List<Food> foods)
{
    string? foodName = "";
    while(foodName == "")
    {
        Console.WriteLine("Please enter the food: ");
        foodName = Console.ReadLine();

        Food? food = foods.FirstOrDefault(x => x.Name == foodName);

        if (food == null || !foods.Contains(food))
        {
            Console.WriteLine("The food you entered does not exist");
            foodName = "";
        }
    }

    return foods.FirstOrDefault(x => x.Name == foodName);
}