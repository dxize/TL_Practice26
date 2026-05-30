namespace CarFactory.Model.CarEngines;

internal class DieselEngine : ICarEngine
{
    public string Name => "Дизельный двигатель";
    public int BaseMaxSpeed => 160;
}