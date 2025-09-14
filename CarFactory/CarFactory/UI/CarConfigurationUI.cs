using CarFactory.Enums;
using CarFactory.Models;
using CarFactory.Services;

namespace CarFactory.UI;

public class CarConfigurationUI
{
    private readonly CarFactoryService _carFactoryService;

    public CarConfigurationUI( CarFactoryService carFactoryService )
    {
        _carFactoryService = carFactoryService;
    }

    public void Run()
    {
        Console.Clear();
        Console.WriteLine( "=== ФАБРИКА АВТОМОБИЛЕЙ ===\n" );

        while ( true )
        {
            ShowMainMenu();
            string? choice = Console.ReadLine();

            switch ( choice )
            {
                case "1":
                    CreateCustomCar();
                    break;
                case "2":
                    ShowAvailableConfigurations();
                    break;
                case "0":
                    Console.WriteLine( "До свидания!" );
                    return;
                default:
                    Console.WriteLine( "Неверный выбор. Попробуйте снова.\n" );
                    break;
            }

            Console.WriteLine( "\nНажмите любую клавишу для продолжения..." );
            Console.ReadKey();
            Console.Clear();
        }
    }

    private void ShowMainMenu()
    {
        Console.WriteLine( """
                           Выберите действие:
                           1. Создать кастомный автомобиль
                           2. Показать доступные компоненты
                           0. Выход
                           """ );
        Console.Write( "Ваш выбор: " );
    }

    private void CreateCustomCar()
    {
        Console.WriteLine( "\n=== СОЗДАНИЕ КАСТОМНОГО АВТОМОБИЛЯ ===\n" );

        Brand? brand = SelectBrand();
        if ( brand == null )
        {
            Console.WriteLine( "Неверный выбор брэнда." );
            return;
        }

        Engine? engine = SelectEngine();
        if ( engine == null )
        {
            Console.WriteLine( "Неверный выбор двигателя." );
            return;
        }

        Transmission? transmission = SelectTransmission();
        if ( transmission == null )
        {
            Console.WriteLine( "Неверный выбор коробки передач." );
            return;
        }

        Body? body = SelectBody();
        if ( body == null )
        {
            Console.WriteLine( "Неверный выбор кузова." );
            return;
        }

        CarColor? color = SelectColor();
        if ( color == null )
        {
            Console.WriteLine( "Неверный выбор цвета." );
            return;
        }

        Car car = _carFactoryService.CreateCustomCar( brand, engine, transmission, body, color );
        Console.WriteLine( $"\n{car.GetConfiguration()}" );
    }

    private Brand? SelectBrand()
    {
        Console.WriteLine( $$"""
                             Выберите брэнд:
                             {{( int )CarBrand.BMW}}. BMW
                             {{( int )CarBrand.Ford}}. Ford
                             {{( int )CarBrand.Tesla}}. Tesla
                             {{( int )CarBrand.Lada}}. Lada
                             """ );
        Console.Write( "Ваш выбор: " );

        string? choice = Console.ReadLine();
        Console.WriteLine();

        if ( int.TryParse( choice, out int brandType ) && Enum.IsDefined( typeof( CarBrand ), brandType ) )
        {
            return ( ( CarBrand )brandType ) switch
            {
                CarBrand.BMW => Brand.BMW,
                CarBrand.Ford => Brand.Ford,
                CarBrand.Tesla => Brand.Tesla,
                CarBrand.Lada => Brand.Lada,
                _ => null
            };
        }

        return null;
    }

    private Engine? SelectEngine()
    {
        Console.WriteLine( $$"""
                             Выберите двигатель:
                             {{( int )EngineType.Gasoline}}. Бензиновый V6 (250 л.с.)
                             {{( int )EngineType.Diesel}}. Дизельный V8 (300 л.с.)
                             {{( int )EngineType.Electric}}. Электрический (400 л.с.)
                             """ );
        Console.Write( "Ваш выбор: " );

        string? choice = Console.ReadLine();
        Console.WriteLine();

        if ( int.TryParse( choice, out int engineType ) && Enum.IsDefined( typeof( EngineType ), engineType ) )
        {
            return ( ( EngineType )engineType ) switch
            {
                EngineType.Gasoline => new GasolineEngine(),
                EngineType.Diesel => new DieselEngine(),
                EngineType.Electric => new ElectricEngine(),
                _ => null
            };
        }

        return null;
    }

    private Transmission? SelectTransmission()
    {
        Console.WriteLine( $$"""
                             Выберите коробку передач:
                             {{( int )TransmissionType.Manual}}. Механическая КПП (6 передач)
                             {{( int )TransmissionType.Automatic}}. Автоматическая КПП (8 передач)
                             {{( int )TransmissionType.Cvt}}. Вариатор CVT (1 передача)
                             """ );
        Console.Write( "Ваш выбор: " );

        string? choice = Console.ReadLine();
        Console.WriteLine();

        if ( int.TryParse( choice, out int transmissionType ) &&
             Enum.IsDefined( typeof( TransmissionType ), transmissionType ) )
        {
            return ( ( TransmissionType )transmissionType ) switch
            {
                TransmissionType.Manual => new ManualTransmission(),
                TransmissionType.Automatic => new AutomaticTransmission(),
                TransmissionType.Cvt => new CvtTransmission(),
                _ => null
            };
        }

        return null;
    }

    private Body? SelectBody()
    {
        Console.WriteLine( $$"""
                             Выберите тип кузова:
                             {{( int )BodyType.Sedan}}. Седан (4 двери)
                             {{( int )BodyType.Hatchback}}. Хэтчбек (5 дверей)
                             {{( int )BodyType.Crossover}}. Кроссовер (5 дверей)
                             {{( int )BodyType.Coupe}}. Купе (2 двери)
                             """ );
        Console.Write( "Ваш выбор: " );

        string? choice = Console.ReadLine();
        Console.WriteLine();

        if ( int.TryParse( choice, out int bodyType ) && Enum.IsDefined( typeof( BodyType ), bodyType ) )
        {
            return ( ( BodyType )bodyType ) switch
            {
                BodyType.Sedan => new SedanBody(),
                BodyType.Hatchback => new HatchbackBody(),
                BodyType.Crossover => new CrossoverBody(),
                BodyType.Coupe => new CoupeBody(),
                _ => null
            };
        }

        return null;
    }

    private CarColor? SelectColor()
    {
        Console.WriteLine( $$"""
                             Выберите цвет:
                             {{( int )CarColorType.Black}}. Черный
                             {{( int )CarColorType.White}}. Белый
                             {{( int )CarColorType.Red}}. Красный
                             {{( int )CarColorType.Blue}}. Синий
                             {{( int )CarColorType.Silver}}. Серебристый
                             {{( int )CarColorType.Green}}. Зеленый
                             """ );
        Console.Write( "Ваш выбор: " );

        string? choice = Console.ReadLine();
        Console.WriteLine();

        if ( int.TryParse( choice, out int colorType ) && Enum.IsDefined( typeof( CarColorType ), colorType ) )
        {
            return ( ( CarColorType )colorType ) switch
            {
                CarColorType.Black => CarColor.Black,
                CarColorType.White => CarColor.White,
                CarColorType.Red => CarColor.Red,
                CarColorType.Blue => CarColor.Blue,
                CarColorType.Silver => CarColor.Silver,
                CarColorType.Green => CarColor.Green,
                _ => null
            };
        }

        return null;
    }

    private void ShowAvailableConfigurations()
    {
        Console.WriteLine( "\n=== ДОСТУПНЫЕ КОМПОНЕНТЫ ===\n" );

        Console.WriteLine( $$"""
                             --- БРЭНДЫ ---
                             {{( int )CarBrand.BMW}}. BMW
                             {{( int )CarBrand.Ford}}. Ford
                             {{( int )CarBrand.Tesla}}. Tesla
                             {{( int )CarBrand.Lada}}. Lada

                             """ );

        Console.WriteLine( $$"""
                             --- ДВИГАТЕЛИ ---
                             {{( int )EngineType.Gasoline}}. Бензиновый V6 (250 л.с., макс. скорость 200 км/ч)
                             {{( int )EngineType.Diesel}}. Дизельный V8 (300 л.с., макс. скорость 180 км/ч)
                             {{( int )EngineType.Electric}}. Электрический (400 л.с., макс. скорость 250 км/ч)

                             """ );

        Console.WriteLine( $$"""
                             --- КОРОБКИ ПЕРЕДАЧ ---
                             {{( int )TransmissionType.Manual}}. Механическая КПП (6 передач)
                             {{( int )TransmissionType.Automatic}}. Автоматическая КПП (8 передач)
                             {{( int )TransmissionType.Cvt}}. Вариатор CVT (1 передача)

                             """ );

        Console.WriteLine( $$"""
                             --- ТИПЫ КУЗОВА ---
                             {{( int )BodyType.Sedan}}. Седан (4 двери, классический)
                             {{( int )BodyType.Hatchback}}. Хэтчбек (5 дверей, компактный)
                             {{( int )BodyType.Crossover}}. Кроссовер (5 дверей, высокий)
                             {{( int )BodyType.Coupe}}. Купе (2 двери, спортивный)

                             """ );

        Console.WriteLine( $$"""
                             --- ЦВЕТА ---
                             {{( int )CarColorType.Black}}. Черный
                             {{( int )CarColorType.White}}. Белый
                             {{( int )CarColorType.Red}}. Красный
                             {{( int )CarColorType.Blue}}. Синий
                             {{( int )CarColorType.Silver}}. Серебристый
                             {{( int )CarColorType.Green}}. Зеленый

                             """ );
    }
}