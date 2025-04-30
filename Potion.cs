using System;

namespace DungeonExplorer
{
    /// <summary>
    /// Represents the type of effect a potion provides
    /// </summary>
    public enum PotionEffect
    {
        Heal,
        Strength,
        Defence,
    }

    /// <summary>
    /// Represents a potion item that can be consumed for an effect
    /// Inherits from the base Item class
    /// </summary>
    public class Potion : Item
    {
        /// <summary>
        /// Gets the effect type of the potion
        /// </summary>
        public PotionEffect Effect { get; private set; }

        /// <summary>
        /// The potency of the potion's effect (e.g amount healed or how much strength/defence it adds)
        /// </summary>
        public int Potency { get; private set; }

        /// <summary>
        /// The amount of uses this potion has left
        /// </summary>
        public int UsesLeft { get; private set; }

        /// <summary>
        /// Initializes a new instance of a Potion
        /// </summary>
        /// <param name="name">The name of the potion</param>
        /// <param name="description">The description of the potion</param>
        /// <param name="effect">The effect type of the potion</param>
        /// <param name="potency">The potency of the potion</param>
        public Potion(string name, string description, PotionEffect effect, int potency)
            : base(name, description)
        {
            Effect = effect;
            Potency = potency;
            UsesLeft = 1;
        }


        /// <summary>
        /// The potion use override from Item
        /// Represents the action of drinking the potion
        /// </summary>
        /// <param name="player">The player consuming the potion</param>
        /// <returns>The success of the operation</returns>
        public override bool Use(Player player)
        {
            if (UsesLeft == 0) { return false; }
            switch (Effect)
            {
                case PotionEffect.Heal:
                    player.Heal(Potency);
                    break;
                case PotionEffect.Strength:
                    player.AddStrength(Potency);
                    break;
                case PotionEffect.Defence:
                    player.AddDefence(Potency);
                    break;
            }
            UsesLeft--;
            return true;
        }


    }
}