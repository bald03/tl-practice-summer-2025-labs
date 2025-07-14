using Casino;

const string GameName = @"
████─████─███─███─█──█─████
█──█─█──█─█────█──██─█─█──█
█────████─███──█──█─██─█──█
█──█─█──█───█──█──█──█─█──█
████─█──█─███─███─█──█─████
";

static void PrintGameName(string gameName)
{
    Console.WriteLine(gameName);
}

static uint ValidatePositiveNumberInput(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        string numberStr = Console.ReadLine();
        if (uint.TryParse(numberStr, out uint number) && number > 0)
        {
            return number;
        }
        Console.WriteLine($"Please enter a valid positive number. Your entered {numberStr}");
    }
}

static void PrintMainMenu()
{
    Console.WriteLine("\ud83c\udfb0 Menu:");
    Console.WriteLine("1. Play");
    Console.WriteLine("2. Exit");
}

static void PrintGameMenu()
{
    Console.WriteLine("1. Spin");
    Console.WriteLine("2. Check balance");
    Console.WriteLine("3. Exit to main menu");
}

static MainMenuOperation? ReadMainMenuOperation()
{
    Console.Write("Enter your choice: ");
    string MainMenuOperationStr = Console.ReadLine();
    if (!Enum.TryParse(MainMenuOperationStr, out MainMenuOperation operation) ||
        !Enum.IsDefined(typeof(MainMenuOperation), operation))
    {
        return null;
    }
    return operation;
}

static GameMenuOperation? ReadGameMenuOperation()
{
    Console.Write("Enter your choice: ");
    string GameMenuOperationStr = Console.ReadLine();
    if (!Enum.TryParse(GameMenuOperationStr, out GameMenuOperation operation) ||
        !Enum.IsDefined(typeof(GameMenuOperation), operation))
    {
        return null;
    }
    return operation;
}

static uint StartGame()
{
    uint balance = GetBalance();

    while (true)
    {
        Console.WriteLine();
        PrintGameMenu();
        GameMenuOperation? operation = ReadGameMenuOperation();

        if (operation == null)
        {
            Console.WriteLine("\u274c Invalid input. Please try again.\n");
            continue;
        }

        balance = HandleGameMenuOperation(operation.Value, balance);
        if (operation == GameMenuOperation.Exit)
        {
            return balance;
        }
    }
}

static uint HandleGameMenuOperation(GameMenuOperation operation, uint currentBalance)
{
    switch (operation)
    {
        case GameMenuOperation.Spin:
            return Spin(currentBalance);
        case GameMenuOperation.CheckBalance:
            CheckBalance(currentBalance);
            return currentBalance;
        case GameMenuOperation.Exit:
            return currentBalance;
        default:
            throw new Exception("\u274c Invalid game operation");
    }
}

static uint Spin(uint balance)
{
    uint bet = GetBet();
    
    if (bet > balance)
    {
        Console.WriteLine("\u274c Bet cannot be greater than balance!");
        return balance;
    }

    int randomNumber = GetRandomNumber();
    Console.WriteLine($"\n=== Spin result: {randomNumber} ===");
    
    if (randomNumber >= 18)
    {
        decimal winAmount = CalculatePayout(bet, randomNumber);
        balance += (uint)winAmount;
        Console.WriteLine($"🎉 You win! Payout: {winAmount}");
    }
    else
    {
        balance -= bet;
        Console.WriteLine("😢 You lose!");
    }
    
    Console.WriteLine($"💵 Current balance: {balance}\n");
    return balance;
}

static void CheckBalance(uint balance)
{
    Console.WriteLine($"Your current balance: {balance}");
}

static uint GetBalance()
{
    return ValidatePositiveNumberInput("Enter your starting balance: ");
}

static uint GetBet()
{
    return ValidatePositiveNumberInput("Enter your bet: ");
}

static int GetRandomNumber()
{
    Random random = new Random();
    return random.Next(1, 21);
}

static decimal CalculatePayout(uint bet, int randomNumber)
{
    const decimal multiplicator = 0.1m;
    decimal winAmount = bet * (1 + (multiplicator * randomNumber % 17));
    return Math.Round(winAmount, 2);
}

static void HandleMainMenuOperation(MainMenuOperation operation)
{
    switch (operation)
    {
        case MainMenuOperation.StartGame:
            StartGame();
            break;
        case MainMenuOperation.Exit:
            break;
        default:
            throw new Exception("\u274c Invalid operation.");
    }
}

PrintGameName(GameName);

while (true)
{
    PrintMainMenu();
    MainMenuOperation? operation = ReadMainMenuOperation();
    if (operation == null)
    {
        Console.WriteLine("\u274c Invalid input. Please try again.\n");
        continue;
    }

    if (operation == MainMenuOperation.Exit)
    {
        Console.WriteLine("Goodbye!");
        break;
    }

    HandleMainMenuOperation(operation.Value);
}