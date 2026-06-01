namespace FighterGame.Model.Races;

public interface IRace
{
    string Name { get; }
    int Damage { get; }
    int Health { get; }
    int Armor { get; }
}