using Fighters.Models.Armors;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Models.Fighters
{
    public class Mercenary : IFighter
    {
        private readonly IRace _race;
        private IArmor _armor = new NoArmor();
        private IWeapon _weapon = new Firsts();

        private int _currentHealth;

        public string Name { get; private set; }

        public Mercenary(string name, IRace race)
        {
            Name = name;
            _race = race;

            _currentHealth = GetMaxHealth();
        }

        public int GetCurrentHealth() => _currentHealth;

        public int GetMaxHealth() => _race.Health + 20; // Наемник получает +20 к здоровью

        public int CalculateDamage() => _weapon.Damage + _race.Damage + 2; // Наемник получает +2 к урону

        public int CalculateArmor() => _armor.Armor + _race.Armor;

        public void SetArmor(IArmor armor)
        {
            _armor = armor;
        }

        public void SetWeapon(IWeapon weapon)
        {
            _weapon = weapon;
        }

        public void TakeDamage(int damage)
        {
            int newHealth = _currentHealth - damage;
            if (newHealth < 0)
            {
                newHealth = 0;
            }

            _currentHealth = newHealth;
        }

        public IWeapon GetWeapon() => _weapon;
        public IArmor GetArmor() => _armor;
        public IRace GetRace() => _race;
    }
}
