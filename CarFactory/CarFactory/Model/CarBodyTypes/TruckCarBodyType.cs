namespace CarFactory.Model.CarBodyTypes
{
    internal class TruckCarBodyType : ICarBodyType
    {
        public string Name => "Грузовик";
        public int SpeedModifier => 1;
    }
}