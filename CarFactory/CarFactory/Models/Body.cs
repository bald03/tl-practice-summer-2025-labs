using CarFactory.Enums;

namespace CarFactory.Models;

public abstract class Body
{
    public string Name { get; protected set; } = string.Empty;
    public string Shape { get; protected set; } = string.Empty;
    public int Doors { get; protected set; }
    public BodyType Type { get; protected set; }

    protected Body( string name, string shape, int doors, BodyType type )
    {
        Name = name;
        Shape = shape;
        Doors = doors;
        Type = type;
    }

    public override string ToString()
    {
        return $"{Name} ({Shape}, {Doors} дверей)";
    }
}

public class SedanBody : Body
{
    public SedanBody() : base( "Седан", "Классический", 4, BodyType.Sedan ) { }
}

public class HatchbackBody : Body
{
    public HatchbackBody() : base( "Хэтчбек", "Компактный", 5, BodyType.Hatchback ) { }
}

public class CrossoverBody : Body
{
    public CrossoverBody() : base( "Кроссовер", "Высокий", 5, BodyType.Crossover ) { }
}

public class CoupeBody : Body
{
    public CoupeBody() : base( "Купе", "Спортивный", 2, BodyType.Coupe ) { }
}