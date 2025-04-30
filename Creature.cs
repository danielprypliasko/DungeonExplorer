using System;

namespace DungeonExplorer
{
    /// <summary>
    /// Represents a base class for creatures in the game
    /// </summary>
    public abstract class Creature : IDamageable, IHealable, IDefence
    {
        public string Name { get; protected set; }
        public bool Dead { get; protected set; }
        public Statistics Stats { get; protected set; }


        /// <summary>
        /// Initializes a new Creature
        /// </summary>
        /// <param name="name">The name of the creature</param>
        /// <param name="health">The health of the creature</param>
        /// <param name="defence">The defence value of the creature</param>
        /// <param name="strength">The strength value of the creature</param>
        public Creature(string name, int health, int defence, int strength)
        {
            Stats = new Statistics(health, defence, strength);
            Name = name;
            Dead = false;
        }

        /// <summary>
        /// Reduces incoming damage based on defence
        /// </summary>
        /// <param name="incomingDamage">The incoming damage amount</param>
        /// <returns>The actual damage taken after defence reduction</returns>
        public virtual int ReduceDamage(int incomingDamage)
        {
            return Math.Max(0, incomingDamage - Stats.Defence);
        }

        /// <summary>
        /// Applies damage to the creature after defence reduction
        /// </summary>
        /// <param name="amount">The amount of damage to attempt to take</param>
        /// <returns>The actual damage taken after defence reduction and taking the health into account</returns>
        public virtual int TakeDamage(int amount)
        {
            int actualDamage = ReduceDamage(amount);
            Stats.Health = Math.Max(0, Stats.Health - actualDamage);
            if (Stats.Health <= 0)
            {
                Die();
            }
            return actualDamage;
        }

        /// <summary>
        /// Heals the creature
        /// </summary>
        /// <param name="amount">The amount to heal</param>
        public virtual void Heal(int amount)
        {
            Stats.Health += amount;
        }

        /// <summary>
        /// Damages another creature based on it's strength value
        /// Used by Monsters to damage the player
        /// </summary>
        /// <param name="other">The creature to damage</param>
        public virtual int DamageOtherCreature(Creature other)
        {
            return other.TakeDamage(Stats.Strength);
        }

        /// <summary>
        /// Handles the creature's death
        /// </summary>
        protected virtual void Die()
        {
            Dead = true;
        }
    }
}