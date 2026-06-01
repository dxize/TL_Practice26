namespace FighterGame.Model.Weapons;

public class SwordWeapon : IWeapon
{
    public string Name { get; } = "Меч";
    public int Damage { get; } = 10;
}