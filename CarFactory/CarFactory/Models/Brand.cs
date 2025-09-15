using CarFactory.Enums;

namespace CarFactory.Models;

public class Brand
{
    public string Name { get; private set; }
    public CarBrand Type { get; private set; }

    public Brand( string name, CarBrand type )
    {
        Name = name;
        Type = type;
    }

    public override string ToString()
    {
        return Name;
    }

    public static Brand BMW => new( "BMW", CarBrand.BMW );
    public static Brand Ford => new( "Ford", CarBrand.Ford );
    public static Brand Tesla => new( "Tesla", CarBrand.Tesla );
    public static Brand Lada => new( "Lada", CarBrand.Lada );
}