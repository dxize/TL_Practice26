namespace FighterGame.Model.Armors
{
    internal class NoArmor : IArmor
    {
        public string Name { get; } = "Без брони";
        public int Armor { get; } = 0;
    }
}