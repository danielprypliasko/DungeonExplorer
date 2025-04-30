using System;

namespace DungeonExplorer
{
    /// <summary>
    /// Interface for creatures that can take damage
    /// </summary>
    public interface IDamageable
    {
        /// <summary>
        /// Method called when the creature takes damage
        /// </summary>
        /// <param name="amount">The amount of damage to take</param>
        int TakeDamage(int amount);
    }
}