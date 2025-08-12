using Casino;

class Program
{
    const string GameName = @"
████─████─███─███─█──█─████
█──█─█──█─█────█──██─█─█──█
█────████─███──█──█─██─█──█
█──█─█──█───█──█──█──█─█──█
████─█──█─███─███─█──█─████
";

    const decimal MULTIPLICATOR = 0.1m;
    static readonly Random random = new();

    public static void Main()
    {
        PrintGameName( GameName );

        while ( true )
        {
            PrintMainMenu();
            MainMenuOperation? operation = ReadMainMenuOperation();
            if ( operation == null )
            {
                Console.WriteLine( "❌ Invalid input. Please try again.\n" );
                continue;
            }

            if ( operation == MainMenuOperation.Exit )
            {
                Console.WriteLine( "Goodbye!" );
                break;
            }

            HandleMainMenuOperation( operation.Value );
        }
    }

    private static void PrintGameName( string gameName )
    {
        Console.WriteLine( gameName );
    }

    static uint ValidatePositiveNumberInput( string prompt )
    {
        while ( true )
        {
            Console.Write( prompt );
            string? numberStr = Console.ReadLine();
            if ( uint.TryParse( numberStr, out uint number ) && number > 0 )
            {
                return number;
            }

            Console.WriteLine( $"Please enter a valid positive number. Your entered {numberStr}" );
        }
    }

    private static void PrintMainMenu()
    {
        Console.WriteLine( """
                           🎰 Menu:
                           1. Play
                           2. Exit
                           """ );
    }

    private static void PrintGameMenu()
    {
        Console.WriteLine( """
                           Menu:
                           1. Spin
                           2. Check balance
                           3. Exit to main menu
                           """ );
    }

    private static MainMenuOperation? ReadMainMenuOperation()
    {
        Console.Write( "Enter your choice: " );
        string? input = Console.ReadLine();
        if ( string.IsNullOrWhiteSpace( input ) ||
             !Enum.TryParse( input, out MainMenuOperation operation ))
        {
            return null;
        }

        return operation;
    }

    private static GameMenuOperation? ReadGameMenuOperation()
    {
        Console.Write( "Enter your choice: " );
        string? input = Console.ReadLine();
        if ( string.IsNullOrWhiteSpace( input ) ||
             !Enum.TryParse( input, out GameMenuOperation operation ))
        {
            return null;
        }

        return operation;
    }

    private static uint StartGame()
    {
        uint balance = GetBalance();

        while ( true )
        {
            Console.WriteLine();
            PrintGameMenu();
            GameMenuOperation? operation = ReadGameMenuOperation();

            if ( operation == null )
            {
                Console.WriteLine( "❌ Invalid input. Please try again.\n" );
                continue;
            }

            balance = HandleGameOperationAndUpdateBalance( operation.Value, balance );
            if ( operation == GameMenuOperation.Exit )
            {
                return balance;
            }
        }
    }

    private static uint HandleGameOperationAndUpdateBalance( GameMenuOperation operation, uint currentBalance )
    {
        switch ( operation )
        {
            case GameMenuOperation.Spin:
                return Spin( currentBalance );
            case GameMenuOperation.PrintBalance:
                PrintBalance( currentBalance );
                return currentBalance;
            case GameMenuOperation.Exit:
                return currentBalance;
            default:
                throw new Exception( "❌ Invalid game operation" );
        }
    }

    private static uint Spin( uint balance )
    {
        uint bet = GetBet();

        if ( bet > balance )
        {
            Console.WriteLine( "❌ Bet cannot be greater than balance!" );
            return balance;
        }

        int randomNumber = GetRandomNumber();
        Console.WriteLine( $"\n=== Spin result: {randomNumber} ===" );

        if ( randomNumber >= 18 )
        {
            decimal winAmount = CalculatePayout( bet, randomNumber );
            balance += ( uint )winAmount;
            Console.WriteLine( $"🎉 You win! Payout: {winAmount}" );
        }
        else
        {
            balance -= bet;
            Console.WriteLine( "😢 You lose!" );
        }

        Console.WriteLine( $"💵 Current balance: {balance}" );
        return balance;
    }

    private static void PrintBalance( uint balance )
    {
        Console.WriteLine( $"Your current balance: {balance}" );
    }

    private static uint GetBalance()
    {
        return ValidatePositiveNumberInput( "Enter your starting balance: " );
    }

    private static uint GetBet()
    {
        return ValidatePositiveNumberInput( "Enter your bet: " );
    }

    private static int GetRandomNumber()
    {
        return random.Next( 1, 21 );
    }

    private static decimal CalculatePayout( uint bet, int randomNumber )
    {
        decimal winAmount = bet * ( 1 + ( MULTIPLICATOR * randomNumber % 17 ) );
        return Math.Round( winAmount, 2 );
    }

    private static void HandleMainMenuOperation( MainMenuOperation operation )
    {
        switch ( operation )
        {
            case MainMenuOperation.StartGame:
                StartGame();
                break;
            case MainMenuOperation.Exit:
                break;
            default:
                throw new Exception( "❌ Invalid operation." );
        }
    }
}