using System;

namespace DungeonExplorer
{

    /// <summary>
    /// Represents an item that can be found and collected in the dungeon
    /// </summary>
    public class Item : ICollectible
    {
        public string Name { get; private set; }
        public string Description { get; private set; }


        /// <summary>
        /// Initializes a new instance of an Item
        /// </summary>
        /// <param name="name">The name of the item</param>
        /// <param name="description">The description of the item</param>
        public Item(string name, string description)
        {
            Name = name;
            Description = description;
        }

        /// <summary>
        /// Method called when the item is used
        /// </summary>
        /// <param name="player">The target of the use method</param>
        /// <returns>Whether it was successful</returns>
        public virtual bool Use(Player player)
        {
            return true;
        }


    }
}