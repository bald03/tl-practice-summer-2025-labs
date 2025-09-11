using Fighters.Models.Fighters;

namespace Fighters.Services
{
    public class GameManager
    {
        private readonly List<IFighter> _fighters;
        private readonly Random _random;

        public GameManager()
        {
            _fighters = new List<IFighter>();
            _random = new Random();
        }

        public void AddFighter( IFighter fighter )
        {
            _fighters.Add( fighter );
        }

        public bool HasFighters()
        {
            return _fighters.Count > 0;
        }

        public bool CanStartBattle()
        {
            return _fighters.Count >= 2;
        }

        public void StartBattle()
        {
            if ( !CanStartBattle() )
            {
                Console.WriteLine( "Для начала битвы нужно минимум 2 бойца!" );
                return;
            }

            Console.WriteLine( "Битва начинается!" );
            Console.WriteLine();

            int round = 1;
            var aliveFighters = _fighters.ToList();

            while ( aliveFighters.Count > 1 )
            {
                Console.WriteLine( $"Раунд {round}" );

                // Рандомно сортирую бойцов по инициативе
                var shuffledFighters = aliveFighters.OrderBy( x => _random.Next() ).ToList();

                for ( int i = 0; i < shuffledFighters.Count; i++ )
                {
                    var attacker = shuffledFighters[ i ];

                    if ( attacker.GetCurrentHealth() <= 0 )
                        continue;

                    var target = FindTarget( attacker, shuffledFighters );
                    if ( target == null )
                        continue;

                    PerformAttack( attacker, target );

                    // Проверка, не помер ли тип
                    if ( target.GetCurrentHealth() <= 0 )
                    {
                        Console.WriteLine( $"{target.Name} погибает!" );
                        aliveFighters.Remove( target );

                        if ( aliveFighters.Count == 1 )
                            break;
                    }
                }

                round++;
                Console.WriteLine();
            }

            if ( aliveFighters.Count == 1 )
            {
                var winner = aliveFighters[ 0 ];
                Console.WriteLine( $"{winner.Name} выжил и победил!" );
            }
        }

        private IFighter FindTarget( IFighter attacker, List<IFighter> fighters )
        {
            var possibleTargets = fighters.Where( f => f != attacker && f.GetCurrentHealth() > 0 ).ToList();
            return possibleTargets.Count > 0 ? possibleTargets[ _random.Next( possibleTargets.Count ) ] : null;
        }

        private void PerformAttack( IFighter attacker, IFighter target )
        {
            int baseDamage = attacker.CalculateDamage();

            // Случайный множитель урона (-20% кэф 0.8 до +10% кэф 1.1)
            double damageMultiplier = 0.8 + ( _random.NextDouble() * 0.3 );
            int modifiedDamage = ( int )( baseDamage * damageMultiplier );

            // Шанс крита (15%)
            bool isCritical = _random.Next( 100 ) < 15;
            if ( isCritical )
            {
                modifiedDamage = ( int )( modifiedDamage * 1.5 );
                Console.WriteLine( $"{attacker.Name} наносит критический удар!" );
            }

            int finalDamage = Math.Max( modifiedDamage - target.CalculateArmor(), 0 );
            if ( finalDamage == 0 && modifiedDamage > 0 )
            {
                finalDamage = 1;
            }

            target.TakeDamage( finalDamage );

            // Логирование
            string damageInfo = isCritical ? $"критический урон {finalDamage}" : $"урон {finalDamage}";
            Console.WriteLine( $"{attacker.Name} наносит {damageInfo}, {target.Name} получает {finalDamage}" );
        }

        public void ResetBattle()
        {
            var newFighters = new List<IFighter>();

            foreach ( var fighter in _fighters )
            {
                IFighter newFighter = null;

                if ( fighter is Knight )
                {
                    newFighter = new Knight( fighter.Name, fighter.GetRace() );
                }
                else if ( fighter is Mercenary )
                {
                    newFighter = new Mercenary( fighter.Name, fighter.GetRace() );
                }

                if ( newFighter != null )
                {
                    newFighter.SetWeapon( fighter.GetWeapon() );
                    newFighter.SetArmor( fighter.GetArmor() );

                    newFighters.Add( newFighter );
                }
            }

            _fighters.Clear();
            _fighters.AddRange( newFighters );
        }

        public void ClearFighters()
        {
            _fighters.Clear();
        }

        public List<IFighter> GetFighters()
        {
            return _fighters.ToList();
        }
    }
}