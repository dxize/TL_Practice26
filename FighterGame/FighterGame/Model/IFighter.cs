namespace FighterGame.Model;

internal interface IFighter
{
    string Name { get; }
    int Health { get; }
    int FullDamage { get; }
    int FullArmor { get; }
    int Initiative { get; }
    bool IsAlive { get; }

    void TakeDamage( int damage );
}