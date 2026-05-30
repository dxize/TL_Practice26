namespace CarFactory.Model.CarEngines;

internal class PetrolEngine : ICarEngine
{
    public string Name => "Бензиновый двигатель";
    public int BaseMaxSpeed => 180;
}