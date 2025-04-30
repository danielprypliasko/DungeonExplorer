namespace DungeonExplorer
{
    /// <summary>
    /// Interface for creatures that have defence capabilities
    /// </summary>
    public interface IDefence
    {

        /// <summary>
        /// Method to calculate damage reduction based on defence
        /// </summary>
        /// <param name="incomingDamage">The incoming damage amount</param>
        /// <returns>The actual damage taken after defence reduction</returns>
        int ReduceDamage(int incomingDamage);
    }
}