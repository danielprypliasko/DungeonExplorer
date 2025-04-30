namespace DungeonExplorer
{
    /// <summary>
    /// Represents a monster creature within the game
    /// </summary>
    public class Monster : Creature
    {
        /// <summary>
        /// Initializes a new Monster
        /// </summary>
        /// <param name="name">The name of the monster</param>
        /// <param name="health">The health of the monster</param>
        /// <param name="defence">The defence value of the monster</param>
        public Monster(string name, int health, int defence, int strength) : base(name, health, defence, strength)
        {
        }
    }
}