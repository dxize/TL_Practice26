namespace FighterGame.Model.Armors;

public class NoArmor : IArmor
{
    public string Name { get; } = "Без брони";
    public int Armor { get; } = 0;
}