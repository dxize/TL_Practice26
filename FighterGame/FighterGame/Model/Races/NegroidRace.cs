namespace FighterGame.Model.Races;

public class NegroidRace : IRace
{
    public string Name { get; } = "Негроид";
    public int Damage { get; } = 12;
    public int Health { get; } = 110;
    public int Armor { get; } = 1;
}