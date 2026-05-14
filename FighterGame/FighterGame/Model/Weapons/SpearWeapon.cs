namespace FighterGame.Model.Weapons
{
    internal class SpearWeapon : IWeapon
    {
        public string Name { get; } = "Копьё";
        public int Damage { get; } = 15;
    }
}