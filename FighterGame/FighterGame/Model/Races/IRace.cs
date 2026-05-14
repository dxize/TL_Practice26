namespace FighterGame.Model.Races
{
    internal interface IRace
    {
        string Name { get; }
        int Damage { get; }
        int Health { get; }
        int Armor { get; }
    }
}