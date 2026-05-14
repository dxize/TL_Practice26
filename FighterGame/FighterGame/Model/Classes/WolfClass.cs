namespace FighterGame.Model.Classes
{
    internal class WolfClass : IClass
    {
        public string Name { get; } = "Волк";
        public int Damage { get; } = 10;
        public int Health { get; } = 6;
        public int Armor { get; } = 1;
    }
}