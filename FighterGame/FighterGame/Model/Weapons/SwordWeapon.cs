namespace FighterGame.Model.Weapons
{
    internal class SwordWeapon : IWeapon
    {
        public string Name { get; } = "Меч";
        public int Damage { get; } = 10;
    }
}