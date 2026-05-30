namespace CarFactory.Model.Transmissions;

internal class ManualTransmission : ITransmission
{
    public string Name => "Механическая коробка передач";
    public int GearCount => 5;
}
