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

    // Adds up the price of every item on the list.
    public int Total()
    {
        int sum = 0;

        for (int i = 0; i < items.Count; i++)
        {
            sum += items[i].Price;
        }

        return sum;
    }

    // Looks up an item by its name. Returns null if there is no such item.
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

    // Writes one item per line, as "price;name".
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

    // Reads the file back into the list.
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
