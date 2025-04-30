namespace DungeonExplorer
{
    /// <summary>
    /// Represents a Goblin creature within the game
    /// </summary>
    public class Goblin : Monster
    {
        /// <summary>
        /// Initializes a new instance of a Goblin
        /// </summary>
        /// <param name="name">The name of the goblin</param>
        /// <param name="health">The health of the goblin</param>
        /// <param name="defence">The defence value of the goblin</param>
        /// <param name="strength">The strength value of the goblin</param>
        public Goblin(string name, int health, int defence, int strength) : base(name, health, defence, strength)
        {
        }
    }
}
