public class Food
{
    public Food(string name)
    {      
        Name = name;

        // Generate random prices
        Random random = new();
        PricesInPence = Enumerable.Range(1, 24).Select(x => random.Next(0,2000)).ToList();
    }
    public string Name { get; set; }
    public List<int> PricesInPence { get; set; }
    public decimal GetCurrentPriceInPounds()
    {

        return PricesInPence.Last() / 100;
    }

    public decimal GetAveragePriceInPounds()
    {
        return (decimal)PricesInPence.Average() / 100;
    }
    public decimal GetHighestPriceInPounds()
    {
        return (decimal)PricesInPence.Max() / 100;
    }
    public decimal GetLowestPriceInPounds()
    {
        return (decimal)PricesInPence.Min() / 100;
    }
    public decimal GetMedianPriceInPounds()
    {
        if (PricesInPence.Count % 2 == 0)
        {
            var middleIndex = PricesInPence.Count / 2;
            return (PricesInPence[middleIndex] + PricesInPence[middleIndex - 1]) / 2 / 100;
        }

        return PricesInPence.OrderBy(x => x).ElementAt(PricesInPence.Count / 2) / 100;
    }
    public decimal GetPriceRiseInPounds()
    { 
        var lastSixMonths = PricesInPence.Skip(PricesInPence.Count - 6);
        return (decimal)(lastSixMonths.Last() - lastSixMonths.First()) / 100;
    }
}