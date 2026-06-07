namespace CarFactory.Model.CarEngines;

internal interface ICarEngine
{
    string Name { get; }
    int BaseMaxSpeed { get; }
}