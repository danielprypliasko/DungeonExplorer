namespace DungeonExplorer
{
    /// <summary>
    /// Represents a weapon item that can be used in combat
    /// Inherits from the base Item class.
    /// </summary>
    public class Weapon : Item
    {
        /// <summary>
        /// The damage value of the weapon
        /// </summary>
        public int Damage { get; private set; }

        /// <summary>
        /// Initializes a new instance of a weapon
        /// </summary>
        /// <param name="name">The name of the weapon</param>
        /// <param name="description">The description of the weapon</param>
        /// <param name="damage">The damage value of the weapon</param>
        public Weapon(string name, string description, int damage)
            : base(name, description)
        {
            Damage = damage;
        }

        /// <summary>
        /// Weapon use override from Item
        /// Represents the action of equipping a weapon
        /// </summary>
        /// <param name="player">The player to equip the weapon for</param>
        /// <returns>The success of the operation</returns>
        public override bool Use(Player player)
        {
            player.EquipWeapon(this);
            return true;
        }
    }
}