internal class GameManager
{
    private List<IFighter> m_fighters;
    private static Random rnd = new Random();

    public GameManager( List<IFighter> fighters )
    {
        m_fighters = fighters;
    }

    public void StartBattle()
    {
        if ( m_fighters == null || m_fighters.Count <= 1 )
        {
            return;
        }

        RunBattleRounds();
        ShowWinner();
    }

    private void RunBattleRounds()
    {
        bool endBattle = false;
        int countRound = 0;

        while ( !endBattle )
        {
            ++countRound;

            CountRoundInfoForUser( countRound, m_fighters.Count );
            SingleRoundFights();

            m_fighters.RemoveAll( fighter => !fighter.IsAlive );

            if ( m_fighters.Count <= 1 )
            {
                endBattle = true;
            }
        }
    }

    private void SingleRoundFights()
    {
        List<IFighter> orderedFighters = m_fighters.Where( f => f.IsAlive ).OrderByDescending( f => f.Initiative ).ToList();

        foreach ( IFighter attacker in orderedFighters )
        {
            if ( !attacker.IsAlive )
            {
                continue;
            }

            List<IFighter> aliveEnemies = m_fighters.Where( f => f.IsAlive && f != attacker ).ToList();

            if ( aliveEnemies.Count == 0 )
            {
                break;
            }

            IFighter nextFighter = aliveEnemies[ rnd.Next( aliveEnemies.Count ) ];

            int attackDamage = CalculateDamage( attacker, nextFighter );
            nextFighter.TakeDamage( attackDamage );

            BattleLog( attacker, nextFighter, attackDamage );
        }
    }

    private int CalculateDamage( IFighter attacker, IFighter defender )
    {
        int baseDamage = Math.Max( attacker.FullDamage - defender.FullArmor, 0 );

        double modifier = rnd.NextDouble() * ( 1.1 - 0.8 ) + 0.8;
        int finalDamage = ( int )Math.Round( baseDamage * modifier );

        double critChance = 0.1;
        if ( rnd.NextDouble() < critChance )
        {
            finalDamage *= 2;
        }

        return finalDamage;
    }

    private void BattleLog( IFighter currFighter, IFighter nextFighter, int damage )
    {
        Console.WriteLine( $"{currFighter.Name} наносит {damage} урона по {nextFighter.Name}." );
        Console.WriteLine( $"У {nextFighter.Name} осталось {nextFighter.Health} HP" );

        if ( !nextFighter.IsAlive )
        {
            Console.WriteLine( $"{nextFighter.Name} мертв :(" );
        }
    }

    private void CountRoundInfoForUser( int countRound, int countFighters )
    {
        Console.WriteLine( $"\n___ Раунд {countRound} ___" );
        Console.WriteLine( $"Осталось {countFighters} бойцов" );

        if ( countRound > 1 )
        {
            Console.WriteLine( "Д.О.Б.Е.Й.Т.Е   В.Ы.Ж.И.В.Ш.И.Х!" );
        }
    }

    private void ShowWinner()
    {
        IFighter winner = m_fighters.FirstOrDefault( f => f.IsAlive );
        if ( winner != null )
        {
            Console.WriteLine( $"\nПобедитель: {winner.Name}!" );
        }
    }
}