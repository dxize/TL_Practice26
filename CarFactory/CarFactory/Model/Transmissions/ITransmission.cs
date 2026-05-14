namespace CarFactory.Model.Transmissions
{
    internal interface ITransmission
    {
        string Name { get; }
        int GearCount { get; }
    }
}