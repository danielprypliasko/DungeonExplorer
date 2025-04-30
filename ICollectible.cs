using System;

namespace DungeonExplorer
{
    /// <summary>
    /// Interface for items that can be collected by the player
    /// </summary>
    public interface ICollectible
    {
        /// <summary>
        /// The name of the collectible item
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The description of the collectible item
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Method called when the item is used
        /// </summary>
        bool Use(Player player);

    }
}