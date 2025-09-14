using CarFactory.Services;
using CarFactory.UI;

namespace CarFactory;

public class Program
{
    public static void Main( string[] args )
    {
        CarFactoryService carFactoryService = new CarFactoryService();
        CarConfigurationUI ui = new CarConfigurationUI( carFactoryService );

        ui.Run();
    }
}