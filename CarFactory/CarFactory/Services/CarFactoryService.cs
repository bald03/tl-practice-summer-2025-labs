using CarFactory.Models;

namespace CarFactory.Services;

public class CarFactoryService
{
    public Car CreateCustomCar( Brand brand, Engine engine, Transmission transmission, Body body, CarColor color )
    {
        return new Car( brand, engine, transmission, body, color );
    }
}