namespace FighterGame.Model.Classes;

internal interface IClass
{
    string Name { get; }
    int Damage { get; }
    int Health { get; }
    int Armor { get; }
}