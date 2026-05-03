internal class Fighter : IFighter
{
    private int m_health;

    private IRace m_race;
    private IClass m_classType;
    private IWeapon m_weapon;
    private IArmor m_armor;

    public string Name { get; private set; }
    public int Initiative { get; private set; }

    public Fighter( string name, IRace race, IClass @class, IWeapon weapon, IArmor armor )
    {
        Name = name;
        m_race = race;
        m_classType = @class;
        m_weapon = weapon;
        m_armor = armor;

        m_health = m_race.Health + m_classType.Health;

        Initiative = new Random().Next( 1, 21 );
    }

    public bool IsAlive => m_health > 0;
    public int Health => m_health;
    public int FullDamage => m_race.Damage + m_classType.Damage + m_weapon.Damage;
    public int FullArmor => m_race.Armor + m_classType.Armor + m_armor.Armor;

    public void SetArmor( IArmor armor )
    {
        m_armor = armor;
    }

    public void SetWeapon( IWeapon weapon )
    {
        m_weapon = weapon;
    }

    public void TakeDamage( int damage )
    {
        int newHealth = m_health - damage;
        if ( newHealth < 0 )
        {
            newHealth = 0;
        }

        m_health = newHealth;
    }
}