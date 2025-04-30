using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    /// <summary>
    /// Represents a Dragon creature within the game
    /// </summary>
    public class Dragon : Monster
    {
        /// <summary>
        /// Initializes a new instance of a Dragon
        /// </summary>
        /// <param name="name">The name of the dragon</param>
        /// <param name="health">The health of the dragon</param>
        /// <param name="defence">The defence value of the dragon</param>
        /// <param name="strength">The strength value of the dragon</param>
        public Dragon(string name, int health, int defence, int strength) : base(name, health, defence, strength)
        {
        }

        /// <summary>
        /// Damages another creature based on it's strength value
        /// Used by Monsters to damage the player
        /// </summary>
        /// <param name="other">The creature to damage</param>
        public override int DamageOtherCreature(Creature other)
        {
            // Dragons are able to pierce the users defence
            return other.TakeDamage(Stats.Strength + other.Stats.Defence);
        }
    }
}
