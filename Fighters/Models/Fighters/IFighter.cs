using Fighters.Models.Armors;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Models.Fighters
{
    public interface IFighter
    {
        string Name { get; }

        int GetCurrentHealth();
        int GetMaxHealth();
        int CalculateDamage();
        int CalculateArmor();

        void SetArmor( IArmor armor );
        void SetWeapon( IWeapon weapon );

        void TakeDamage( int damage );

        IWeapon GetWeapon();
        IArmor GetArmor();
        IRace GetRace();
    }
}