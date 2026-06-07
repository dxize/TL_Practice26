using CarFactory.Model.CarBodyTypes;
using CarFactory.Model.CarEngines;
using CarFactory.Model.Transmissions;

namespace CarFactory.Model;

internal class Car
{
    private readonly string _name;
    private readonly string _carBodyTypeName;
    private readonly string _carColor;
    private readonly string _transmissionName;
    private readonly int _gearCount;
    private readonly string _carEngineName;
    private readonly int _maxSpeed;

    public Car( string name, ICarBodyType body, string color,
        ITransmission transmission, ICarEngine engine )
    {
        _name = name;
        _carBodyTypeName = body.Name;
        _carColor = color;
        _transmissionName = transmission.Name;
        _gearCount = transmission.GearCount;
        _carEngineName = engine.Name;

        _maxSpeed = body.SpeedModifier + transmission.GearCount * 20 + engine.BaseMaxSpeed;
    }

    public void Info()
    {
        Console.WriteLine( $"""

            Марка: {_name}
            Кузов: {_carBodyTypeName}
            Двигатель: {_carEngineName}
            Коробка передач: {_transmissionName}
            Цвет: {_carColor}
            Количество передач: {_gearCount}
            Максимальная скорость: {_maxSpeed}

            """ );
    }
}