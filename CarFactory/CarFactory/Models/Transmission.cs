using CarFactory.Enums;

namespace CarFactory.Models;

public abstract class Transmission
{
    public string Name { get; protected set; } = string.Empty;
    public int Gears { get; protected set; }
    public string TypeName { get; protected set; } = string.Empty;
    public TransmissionType Type { get; protected set; }

    protected Transmission( string name, int gears, string typeName, TransmissionType type )
    {
        Name = name;
        Gears = gears;
        TypeName = typeName;
        Type = type;
    }

    public override string ToString()
    {
        return $"{Name} ({Gears} передач)";
    }
}

public class ManualTransmission : Transmission
{
    public ManualTransmission() : base( "Механическая КПП", 6, "Механическая", TransmissionType.Manual ) { }
}

public class AutomaticTransmission : Transmission
{
    public AutomaticTransmission() : base( "Автоматическая КПП", 8, "Автоматическая", TransmissionType.Automatic ) { }
}

public class CvtTransmission : Transmission
{
    public CvtTransmission() : base( "Вариатор CVT", 1, "Бесступенчатая", TransmissionType.Cvt ) { }
}