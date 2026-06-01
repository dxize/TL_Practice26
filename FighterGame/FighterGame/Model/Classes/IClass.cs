namespace FighterGame.Model.Classes;

public interface IClass
{
    string Name { get; }
    int Damage { get; }
    int Health { get; }
    int Armor { get; }
}