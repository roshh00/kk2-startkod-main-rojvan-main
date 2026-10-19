ShoppingList list = new ShoppingList("items.txt", 500);
list.Load();

while (true)
{
    Console.WriteLine();
    list.Print();
    Console.WriteLine();
    Console.WriteLine("1. Lägg till vara");
    Console.WriteLine("2. Ta bort vara");
    Console.WriteLine("3. Spara");
    Console.WriteLine("4. Sök vara");
    Console.WriteLine("5. Avsluta");
    int choice = ReadInt("Välj: ");

    if (choice == 1)
    {
        string name = ReadName("Namn: ");
        
// loopar tills användaren anger ett giltigt pris 
// och fångar ArgumentOutOfRangeException om priset är negativt. 

        while (true)
        {
            int price = ReadInt("Pris:");
            try
            {
                list.Add(new Item(name, price));
                break;
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Varans pris får inte vara negativt.");
            }
            catch (InvalidOperationException exception)
            {
                Console.WriteLine(exception.Message);
                break;
            }
            // har lagt till en till catch för att den ska inte krascha när man överskrider budgeten.
        }
    }
    else if (choice == 2)
    {
        int number = ReadInt("Nummer: ");
        list.RemoveAt(number);
    }
    else if (choice == 3)
    {
        list.Save();
    }
    else if (choice == 4)
    {
        Console.Write("Namn att söka efter: ");
        string wanted = Console.ReadLine();
        Item found = list.Find(wanted);

        if (found == null)
        {
            Console.WriteLine("Varan finns inte i listan.");
        }
        else
        {
            Console.WriteLine($"Hittade: {found}");
        }
    }
    else if (choice == 5)
    {
        break;
    }
}

int ReadInt(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        if (int.TryParse(Console.ReadLine(), out int value))
        {
            return value;
        }

        Console.WriteLine("Ogiltig inmatning. Ange ett heltal.");
    }
}

string ReadName(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        string name = Console.ReadLine();
        
// kastar in ett undantag om namnet är ogiltigt, vilket fångas i catch-blocket
        try
        {
            Item.ValidateName(name);
            return name;
        }
        catch (ArgumentException exception)
        {
            Console.WriteLine(exception.Message);
        }
    }
}
