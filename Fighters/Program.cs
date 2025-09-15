using Fighters.Controllers;

namespace Fighters
{
    public class Program
    {
        public static void Main( string[] args )
        {
            GameController controller = new GameController();
            controller.ShowWelcome();

            while ( true )
            {
                Console.WriteLine();
                Console.Write( "App: Введите команду:" );
                string? command = Console.ReadLine()?.Trim();

                if ( string.IsNullOrEmpty( command ) )
                {
                    Console.WriteLine( "Пожалуйста, введите команду." );
                    continue;
                }

                if ( command.ToLower() == "exit" )
                {
                    Console.WriteLine( "До свидания!" );
                    break;
                }

                try
                {
                    controller.ProcessCommand( command );
                }
                catch ( Exception ex )
                {
                    Console.WriteLine( $"Произошла ошибка: {ex.Message}" );
                }
            }
        }
    }
}