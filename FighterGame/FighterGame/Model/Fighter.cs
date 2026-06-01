using FighterGame.Model.Armors;
using FighterGame.Model.Classes;
using FighterGame.Model.Races;
using FighterGame.Model.Weapons;
using FighterGame.Utilities;

namespace FighterGame.Model;

public class Fighter : IFighter
{
    private int _health;

    private IRace _race;
    private IClass _classType;
    private IWeapon _weapon;
    private IArmor _armor;

    public string Name { get; private set; }
    public int Initiative { get; private set; }

    public Fighter( string name, IRace race, IClass @class, IWeapon weapon, IArmor armor )
    {
        Name = name;
        _race = race;
        _classType = @class;
        _weapon = weapon;
        _armor = armor;

        _health = _race.Health + _classType.Health;

        Initiative = Randomizer.GetInt( 1, 21 );
    }

    public bool IsAlive => _health > 0;
    public int Health => _health;
    public int FullDamage => _race.Damage + _classType.Damage + _weapon.Damage;
    public int FullArmor => _race.Armor + _classType.Armor + _armor.Armor;

    public void TakeDamage( int damage )
    {
        int newHealth = _health - damage;
        if ( newHealth < 0 )
        {
            newHealth = 0;
        }

        _health = newHealth;
    }
}