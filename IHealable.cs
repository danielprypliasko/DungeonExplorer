using System;

namespace DungeonExplorer
{
    /// <summary>
    /// Interface for creatures that can be healed
    /// </summary>
    public interface IHealable
    {
        /// <summary>
        /// Method called when the creature is healed
        /// </summary>
        /// <param name="amount">The amount to heal</param>
        void Heal(int amount);
    }
}