namespace FighterGame.Model.Classes;

internal class PeasantClass : IClass
{
    public string Name { get; } = "Крестьянин";
    public int Damage { get; } = 6;
    public int Health { get; } = 2;
    public int Armor { get; } = 2;
}