namespace FighterGame.Model.Classes;

public class KnightClass : IClass
{
    public string Name { get; } = "Рыцарь";
    public int Damage { get; } = 8;
    public int Health { get; } = 5;
    public int Armor { get; } = 5;
}