namespace FighterGame.Model.Races;

public class EuropeanRace : IRace
{
    public string Name { get; } = "Европеец";
    public int Damage { get; } = 10;
    public int Health { get; } = 90;
    public int Armor { get; } = 4;
}