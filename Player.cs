using System.Collections.Generic;

namespace DungeonExplorer
{
    /// <summary>
    /// An public class that manages the players state such as Name and inventory
    /// </summary>
    public class Player
    {
        public string Name { get; private set; }
        public int Health { get; private set; }

        private List<string> inventory = new List<string>();

        /// <summary>
        /// This constructor sets up the Player object with its name and health
        /// </summary>
        public Player(string name, int health) 
        {
            Name = name;
            Health = health;
        }

        /// <summary>
        /// This method simply adds an item to the players inventory that is provided as an argument
        /// </summary>
        /// <param name="item">The item to pick up</param>
        public void PickUpItem(string item)
        {
            inventory.Add(item);
        }
        /// <summary>
        /// This method returns a string describing the players inventory
        /// </summary>
        /// <returns>A string, describing what items the players inventory holds</returns>
        public string InventoryContents()
        {
            if (inventory.Count == 0) return "No items";
            return string.Join(", ", inventory);
        }
    }
}