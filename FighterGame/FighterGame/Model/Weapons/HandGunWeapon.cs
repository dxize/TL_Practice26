namespace FighterGame.Model.Weapons
{
    internal class HandGunWeapon : IWeapon
    {
        public string Name { get; } = "Пистолет";
        public int Damage { get; } = 30;
    }
}