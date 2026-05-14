namespace FighterGame.Model.Races
{
    internal class AsianRace : IRace
    {
        public string Name { get; } = "Азиат";
        public int Damage { get; } = 8;
        public int Health { get; } = 70;
        public int Armor { get; } = 10;
    }
}