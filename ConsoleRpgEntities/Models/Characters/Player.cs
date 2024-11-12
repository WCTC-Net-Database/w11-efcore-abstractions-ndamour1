using ConsoleRpgEntities.Models.Abilities.PlayerAbilities;
using ConsoleRpgEntities.Models.Attributes;

namespace ConsoleRpgEntities.Models.Characters
{
    public class Player : ITargetable, IPlayer
    {
        public int Experience { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Health { get; set; }
        public virtual IEnumerable<Ability> Abilities { get; set; }

        public async void Attack(ITargetable target)
        {
            // "Dice roll"
            Random rand = new Random();
            int roll = rand.Next(1, 7);
        
            // Equipment stats
            int attack = 3;
            int defense = 2;
        
            // Damage calculations
            decimal damage = (roll + attack) / ((0 + 100) / 100);
        
            // Total damage
            int totalDamage = (int)Math.Round(damage, MidpointRounding.AwayFromZero);
            int overkill = totalDamage + (target.Health - totalDamage);
        
            // Player-specific attack logic
            Console.WriteLine($"{Name} attacks {target.Name} with a sword!");
            target.Health -= totalDamage;
            if (target.Health > 0)
            {
                Console.WriteLine($"{target.Name} took {totalDamage} damage.");
            }
            else
            {
                if (target.Health - totalDamage < 0)
                {
                    Console.WriteLine($"{target.Name} took {overkill} damage.\n{target.Name} has been defeated.");
                }
                else if (target.Health - totalDamage == 0)
                {
                    Console.WriteLine($"{target.Name} took {totalDamage} damage.\n{target.Name} has been defeated.");
                }
            }
        }

        public void UseAbility(IAbility ability, ITargetable target)
        {
            if (Abilities.Contains(ability))
            {
                ability.Activate(this, target);
            }
            else
            {
                Console.WriteLine($"{Name} does not have the ability {ability.Name}!");
            }
        }
    }
}
