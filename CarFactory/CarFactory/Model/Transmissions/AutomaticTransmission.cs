namespace CarFactory.Model.Transmissions
{
    internal class AutomaticTransmission : ITransmission
    {
        public string Name => "Автоматическая коробка передач";
        public int GearCount => 6;
    }
}