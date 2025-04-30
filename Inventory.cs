using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    /// <summary>
    /// A basic class for an inventory, which wraps a List
    /// </summary>
    public class Inventory
    {
        private List<Item> items;

        /// <summary>
        /// Gets all items in the inventory.
        /// </summary>
        /// <returns>A list of all items in the inventory.</returns>
        public List<Item> GetAll()
        {
            return items;
        }

        /// <summary>
        /// Initializes a new instance of the Inventory class.
        /// </summary>
        public Inventory()
        {
            items = new List<Item>();
        }

        /// <summary>
        /// Adds an item to the inventory.
        /// </summary>
        /// <param name="item">The item to add</param>
        public void Add(Item item)
        {
            items.Add(item);
        }

        /// <summary>
        /// Removes an item from the inventory
        /// </summary>
        /// <param name="item">The item to remove</param>
        public void Remove(Item item)
        {
            items.Remove(item);
        }

        /// <summary>
        /// Gets all potions in the inventory
        /// </summary>
        /// <returns>A list of all potions in the inventory</returns>
        public List<Potion> GetAllPotions()
        {
            return items.OfType<Potion>().ToList();
        }

        /// <summary>
        /// Gets all weapons in the inventory
        /// </summary>
        /// <returns>A list of all weapons in the inventory</returns>
        public List<Weapon> GetAllWeapons()
        {
            return items.OfType<Weapon>().ToList();
        }

        /// <summary>
        /// Checks if the inventory contains an item
        /// </summary>
        /// <param name="item">The item to check for</param>
        /// <returns>True if the item is in the inventory, false otherwise</returns>
        public bool Contains(Item item) { return items.Contains(item); }

        /// <summary>
        /// Gets the number of items in the inventory.
        /// </summary>
        /// <returns>The number of items in the inventory</returns>
        public int Count()
        {
            return items.Count;
        }
    }
}
