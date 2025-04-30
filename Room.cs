
using System;
using System.Collections.Generic;
using System.Linq;
using static DungeonExplorer.Utils;

namespace DungeonExplorer
{
    /// <summary>
    /// A public class that manages rooms and their properties, such as Items and Description.
    /// </summary>
    public class Room
    {
        public string Description { get; private set; }
        public bool HasItem { get; private set; }
        public List<Item> Items { get; private set; }
        public List<Monster> Monsters { get; private set; }
        public bool Visited { get; private set; }

        /// <summary>
        /// A constructor for the Room class, makes a room with a description containing a list of items and monsters
        /// </summary>
        /// <param name="description">The description of the room</param>
        /// <param name="items">The items in the room</param>
        public Room(string description, List<Item> items, List<Monster> monsters)
        {
            this.Description = description;
            this.Items = items;
            this.HasItem = true;
            this.Visited = false;
            this.Monsters = monsters;
        }

        /// <summary>
        /// This method returns the description property
        /// </summary>
        /// <returns>A string description of the room</returns>
        public string GetDescription()
        {
            return Description;
        }

        /// <summary>
        /// This method marks the room as a visited room
        /// </summary>
        public void SetVisited() { this.Visited = true; }

        /// <summary>
        /// This method takes in an index of an item in the rooms item list,
        /// then it removes it from that and list and returns it
        /// </summary>
        /// <param name="idx">The index of the item in the list</param>
        /// <returns>The removed item</returns>
        public Item PickUpItem(int idx)
        {

            Item item = Items[idx];
            Items.RemoveAt(idx);

            if (Items.Count == 0) { HasItem = false; }
            return item;
        }


        /// <summary>
        /// Gets a string describing the status of monsters in the room, also pointing out the strongest
        /// </summary>
        /// <returns>A string describing the monster status</returns>
        public string GetMonsterStatus()
        {
            var output = "";

            var strongestMonster = Monsters.OrderBy(monster => monster.Stats.Strength).Last();

            foreach (var monster in Monsters)
            {

                output += $"{monster.Name} ({monster.Stats.Health} HP){(monster == strongestMonster ? " (Strongest)" : "")}\n";
            }

            return output;
        }
    }


}

