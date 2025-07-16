using OrderManager;

static string ValidateStringInput(string prompt, string fieldName)
{
    while (true)
    {
        Console.Write(prompt);
        string input = Console.ReadLine();
        if (input == null || input == "" || input == " ")
        {
            Console.WriteLine($"Please enter a valid {fieldName}. You entered \"{input}\".\n");
        }
        else
        {
            return input;
        }
    }
}

static uint ValidatePositiveNumberInput(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        string input = Console.ReadLine();
        if (uint.TryParse(input, out uint number) && number > 0)
        {
            return number;
        }
        Console.WriteLine("Please enter a valid positive number.\n");
    }
}

static void PrintMenu()
{
    Console.WriteLine("Welcome to Order manager!");
    Console.WriteLine("Menu:");
    Console.WriteLine("1. Place an order.");
    Console.WriteLine("2. Exit.");
}

static Operation? ReadOperation()
{
    Console.Write("Enter operation: ");
    string operationStr = Console.ReadLine();
    
    if (!Enum.TryParse(operationStr, out Operation operation) || 
        !Enum.IsDefined(typeof(Operation), operation))
    {
        return null;
    }
    
    return operation;
}

while (true)
{
    PrintMenu();
    Operation? operation = ReadOperation();

    if (operation == null)
    {
        Console.WriteLine("Invalid input. Please try again.\n");
        continue;
    }

    if (operation == Operation.Exit)
    {
        Console.WriteLine("Goodbye!");
        break;
    }

    HandleOperation(operation.Value);
}

static string GetProductName()
{
    return ValidateStringInput("Enter product name: ", "product name");
}

static uint GetProductAmount(string productName)
{
    return ValidatePositiveNumberInput($"Enter how much \"{productName}\" you need: ");
}

static string GetUserName()
{
    return ValidateStringInput("Enter user name: ", "user name");
}

static string GetDeliveryAddress()
{
    return ValidateStringInput("Enter the delivery address: ", "delivery address");
}

static bool IsCorrectOrder(string productName, uint productAmount, string userName, string deliveryAddress)
{
    Console.WriteLine($"\nHi, {userName}! You ordered {productAmount} unit(s) of \"{productName}\" for delivery to {deliveryAddress}.");
    Console.Write("Is it correct? (Enter y/n): ");
    string flag = Console.ReadLine();
    return flag == "y" || flag == "Y";
}

static DateTime GetDeliveryDate()
{
    const int deliveryDays = 3;
    return DateTime.Today.AddDays(deliveryDays);
}

static void WriteOrder(string productName, uint productAmount, string userName, string deliveryAddress, DateTime deliveryDate)
{
    Console.WriteLine($"\n{userName}, your order for {productAmount} unit(s) of \"{productName}\" has been placed!");
    Console.WriteLine($"Expect delivery to: {deliveryAddress} by {deliveryDate:dd.MM.yyyy}");
    Console.WriteLine();
}

static void StartPlaceAnOrder()
{
    Console.WriteLine();
    string productName = GetProductName();
    uint productAmount = GetProductAmount(productName);
    string userName = GetUserName();
    string deliveryAddress = GetDeliveryAddress();

    if (IsCorrectOrder(productName, productAmount, userName, deliveryAddress))
    {
        DateTime deliveryDate = GetDeliveryDate();
        WriteOrder(productName, productAmount, userName, deliveryAddress, deliveryDate);
    }
    else
    {
        Console.WriteLine("\nOrder canceled. Returning to the main menu.\n");
    }
}

static void HandleOperation(Operation operation)
{
    switch (operation)
    {
        case Operation.PlaceAnOrder:
            StartPlaceAnOrder();
            break;
        case Operation.Exit:
            break;
        default:
            throw new Exception($"Invalid operation. Entered: {operation}.");
    }
}