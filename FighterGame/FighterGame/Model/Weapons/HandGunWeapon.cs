namespace FighterGame.Model.Weapons;

public class HandGunWeapon : IWeapon
{
    public string Name { get; } = "Пистолет";
    public int Damage { get; } = 30;
}