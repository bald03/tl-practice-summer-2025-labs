using OrderManager;

const int DELIVERY_DAYS = 3;

Main();

static string ValidateStringInput( string prompt, string fieldName )
{
    while ( true )
    {
        Console.Write( prompt );
        string input = Console.ReadLine()?.Trim() ?? string.Empty;

        if ( string.IsNullOrEmpty( input ) )
        {
            Console.WriteLine( $"Please enter a valid {fieldName}. You entered \"{input}\".\n" );
            continue;
        }

        return input;
    }
}

static uint ValidatePositiveNumberInput( string prompt )
{
    while ( true )
    {
        Console.Write( prompt );
        string input = Console.ReadLine();
        if ( uint.TryParse( input, out uint number ) && number > 0 )
        {
            return number;
        }

        Console.WriteLine( "Please enter a valid positive number.\n" );
    }
}

static void PrintMenu()
{
    Console.WriteLine( """
                       Welcome to Order manager!
                       Menu:
                       1. Place an order.
                       2. Exit.
                       """ );
}

static Operation? ReadOperation()
{
    Console.Write( "Enter operation: " );
    string? operationStr = Console.ReadLine();

    if ( !Enum.TryParse( operationStr, out Operation operation ) )
    {
        return null;
    }

    return operation;
}

static string GetProductName()
{
    return ValidateStringInput( "Enter product name: ", "product name" );
}

static uint GetProductAmount( string productName )
{
    return ValidatePositiveNumberInput( $"Enter how much \"{productName}\" you need: " );
}

static string GetUserName()
{
    return ValidateStringInput( "Enter user name: ", "user name" );
}

static string GetDeliveryAddress()
{
    return ValidateStringInput( "Enter the delivery address: ", "delivery address" );
}

static bool IsCorrectOrder( string productName, uint productAmount, string userName, string deliveryAddress )
{
    Console.WriteLine(
        $"\nHi, {userName}! You ordered {productAmount} unit(s) of \"{productName}\" for delivery to {deliveryAddress}." );
    Console.Write( "Is it correct? (Enter y/n): " );
    string flag = Console.ReadLine()?.ToLower()?.Trim() ?? string.Empty;
    return flag == "y";
}

static DateTime GetDeliveryDate()
{
    return DateTime.Today.AddDays( DELIVERY_DAYS );
}

static void WriteOrder( string productName, uint productAmount, string userName, string deliveryAddress,
    DateTime deliveryDate )
{
    Console.WriteLine( $"""
                        {userName}, your order for {productAmount} unit(s) of "{productName}" has been placed!
                        Expect delivery to: {deliveryAddress} by {deliveryDate:dd.MM.yyyy}

                        """ );
}

static void StartPlaceAnOrder()
{
    Console.WriteLine();
    string productName = GetProductName();
    uint productAmount = GetProductAmount( productName );
    string userName = GetUserName();
    string deliveryAddress = GetDeliveryAddress();

    if ( IsCorrectOrder( productName, productAmount, userName, deliveryAddress ) )
    {
        DateTime deliveryDate = GetDeliveryDate();
        WriteOrder( productName, productAmount, userName, deliveryAddress, deliveryDate );
    }
    else
    {
        Console.WriteLine( "\nOrder canceled. Returning to the main menu.\n" );
    }
}

static void Main()
{
    Operation? operation;

    do
    {
        try
        {
            PrintMenu();
            operation = ReadOperation();

            switch ( operation )
            {
                case Operation.PlaceAnOrder:
                    StartPlaceAnOrder();
                    break;

                case Operation.Exit:
                    Console.WriteLine( "Goodbye!" );
                    break;

                default:
                    Console.WriteLine( "Invalid input. Please try again.\n" );
                    break;
            }
        }
        catch ( Exception ex )
        {
            Console.WriteLine( $"\nError: {ex.Message}" );
            Console.WriteLine( "Please try again.\n" );
            operation = null;
        }
    } while ( operation != Operation.Exit );
}