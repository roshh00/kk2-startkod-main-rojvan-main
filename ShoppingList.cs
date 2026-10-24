class ShoppingList
{
    private List<Item> items = new List<Item>();
    private string path;

// lägger till en budgettak 
    private readonly int budget;
    public ShoppingList(string path, int budget)
    {
        if (budget < 0)
        { 
            throw new ArgumentOutOfRangeException(nameof(budget));
        }

        this.path = path;
        this.budget = budget;
    }


    public void Add(Item item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item));
        }
        if (item.Price > budget - Total())
        {
            throw new InvalidOperationException("Varans pris överstiger budgeten.");
        }
        items.Add(item);
    }

    // tar bort ett item från listan, men tar bort det som är på platsen number-1 i listan 
public void RemoveAt(int number)
{
    if (number < 1 || number > items.Count)
    {
        throw new ArgumentOutOfRangeException(
            nameof(number),
            number,
            "Numret finns inte i listan.");
    }

    items.RemoveAt(number - 1);
}

    // Summerar alla priser i listan och returnerar summan.
    public int Total()
    {
        int sum = 0;

        for (int i = 0; i < items.Count; i++)
        {
            sum += items[i].Price;
        }

        return sum;
    }

    // Kollar upp om en vara finns i listan och returnerar den om den finns, annars null
    public Item Find(string name)
    {
        foreach (Item item in items)
        {
            if (item.Name == name)
            {
                return item;
            }
        }

        return null;
    }

    public void Print()
    {
        for (int i = 0; i < items.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {items[i]}");
        }

        Console.WriteLine($"Totalt: {Total()} kr");
    }

    // Skriver ett pris och namn på varje item i listan till en fil.
    public void Save()
    {
        List<string> lines = new List<string>();

        foreach (Item item in items)
        {
            lines.Add($"{item.Price};{item.Name}");
        }

        try
        {
            File.WriteAllText(path, string.Join("\r\n", lines) + "\r\n");
            Console.WriteLine("Listan är sparad.");
        }
        catch (IOException)
        {
            Console.WriteLine("Fel: Listan kunde inte sparas.");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Fel: Listan kunde inte sparas.");
        }
    }

    // Läser tillbaka listan från filen och lägger till varje item i listan.
    public void Load()
    {
        // Sparar listan utan items.txt.
        if (!File.Exists(path))
        {
            return;
        }

        string[] lines = File.ReadAllLines(path);

        foreach (string line in lines)
        
        // Har lagt till en if sats så den inte kraschar när man har en ogiltig rad i filen.

        {
            string[] parts = line.Split(';');
            if (parts.Length < 2 ||
                !int.TryParse(parts[0], out int price) ||
                price < 0 ||
                string.IsNullOrWhiteSpace(parts[1]))
            {
                continue;
            }

            items.Add(new Item(parts[1], price));
        }
    }
}
