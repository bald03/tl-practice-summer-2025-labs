using CarFactory.Models;

namespace CarFactory.Models;

public class Car
{
    public Brand Brand { get; private set; }
    public Engine Engine { get; private set; }
    public Transmission Transmission { get; private set; }
    public Body Body { get; private set; }
    public CarColor Color { get; private set; }

    public double MaxSpeed => Engine.MaxSpeed;

    public int Gears => Transmission.Gears;

    public Car( Brand brand, Engine engine, Transmission transmission, Body body, CarColor color )
    {
        Brand = brand;
        Engine = engine;
        Transmission = transmission;
        Body = body;
        Color = color;
    }

    public override string ToString()
    {
        return $"Модель: {Brand}\n" +
               $"Двигатель: {Engine}\n" +
               $"Коробка передач: {Transmission}\n" +
               $"Кузов: {Body}\n" +
               $"Цвет: {Color}\n" +
               $"Максимальная скорость: {MaxSpeed} км/ч\n" +
               $"Количество передач: {Gears}";
    }

    public string GetConfiguration()
    {
        return $"=== КОНФИГУРАЦИЯ АВТОМОБИЛЯ ===\n" +
               $"{ToString()}\n" +
               $"================================";
    }

    /// <summary>
    /// Создает автомобиль из компонентов (Фабричный метод)
    /// </summary>
    /// <param name="brand">Брэнд автомобиля</param>
    /// <param name="engine">Двигатель</param>
    /// <param name="transmission">Коробка передач</param>
    /// <param name="body">Кузов</param>
    /// <param name="color">Цвет</param>
    /// <returns>Новый экземпляр автомобиля</returns>
    public static Car CreateFromParts( Brand brand, Engine engine, Transmission transmission, Body body,
        CarColor color )
    {
        return new Car( brand, engine, transmission, body, color );
    }
}