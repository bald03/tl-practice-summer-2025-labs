using CarFactory.Enums;

namespace CarFactory.Models;

public abstract class Engine
{
    public string Name { get; protected set; } = string.Empty;
    public int HorsePower { get; protected set; }
    public double MaxSpeed { get; protected set; }
    public EngineType Type { get; protected set; }

    protected Engine( string name, int horsePower, double maxSpeed, EngineType type )
    {
        Name = name;
        HorsePower = horsePower;
        MaxSpeed = maxSpeed;
        Type = type;
    }

    public override string ToString()
    {
        return $"{Name} ({HorsePower} л.с.)";
    }
}

public class GasolineEngine : Engine
{
    public GasolineEngine() : base( "Бензиновый V6", 250, 200.0, EngineType.Gasoline ) { }
}

public class DieselEngine : Engine
{
    public DieselEngine() : base( "Дизельный V8", 300, 180.0, EngineType.Diesel ) { }
}

public class ElectricEngine : Engine
{
    public ElectricEngine() : base( "Электрический", 400, 250.0, EngineType.Electric ) { }
}