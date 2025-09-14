using CarFactory.Enums;

namespace CarFactory.Models;

public class CarColor
{
    public string Name { get; private set; }
    public CarColorType Type { get; private set; }

    public CarColor( string name, CarColorType type )
    {
        Name = name;
        Type = type;
    }

    public override string ToString()
    {
        return Name;
    }

    public static CarColor Black => new( "Черный", CarColorType.Black );
    public static CarColor White => new( "Белый", CarColorType.White );
    public static CarColor Red => new( "Красный", CarColorType.Red );
    public static CarColor Blue => new( "Синий", CarColorType.Blue );
    public static CarColor Silver => new( "Серебристый", CarColorType.Silver );
    public static CarColor Green => new( "Зеленый", CarColorType.Green );
}