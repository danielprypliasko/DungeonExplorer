using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DungeonExplorer.Utils;

namespace DungeonExplorer
{
    /// <summary>
    /// A static class that manages a 2D grid of rooms and provides methods for room generation/navigation
    /// </summary>
    public class GameMap
    {
        private static Room[,] rooms;

        private static readonly int roomMapSize = 4;

        private static readonly Random random = new Random();


        private static readonly string[] roomShapeDescriptions = {
            "square", "rectangular", "circular", "hexagonal", "L-shaped", "triangular", "narrow", "vast", "compact"
        };

        private static readonly string[] roomLightingDescriptions = {
            "dimly-lit", "well-lit", "dark", "sunlit", "moonlit", "candlelit", "pitch-black", "flickering", "muted"
        };

        private static readonly string[] roomWallDescriptions = {
            "stone", "brick", "wooden paneled", "marble", "concrete", "metal-plated", "moss-covered", "ivy-covered"
        };

        private static readonly string[] roomFloorDescriptions = {
            "wooden", "stone", "marble", "carpeted", "tiled", "dirt", "metal grating", "moss-covered", "concrete", "checkered", "cracked"
        };

        private static readonly string[] roomFeatureDescriptions = {
            "with a crackling fireplace", "containing an ornate fountain", "with strange symbols etched into the floor", "with a massive chandelier", "featuring a mysterious altar", "with bookshelves lining the walls", "with cobwebs in every corner",
            "", "", "", "", "", "", "", "",
        };

        /// <summary>
        /// Generates a random room description by combining different room attributes
        /// </summary>
        /// <returns>A string containing a randomly generated room description</returns>
        public static string GenerateRoomDescription()
        {
            // Generates a random room description

            string shape = roomShapeDescriptions[random.Next(roomShapeDescriptions.Length)];
            string light = roomLightingDescriptions[random.Next(roomLightingDescriptions.Length)];
            string wall = roomWallDescriptions[random.Next(roomWallDescriptions.Length)];
            string floor = roomFloorDescriptions[random.Next(roomFloorDescriptions.Length)];
            string feature = roomFeatureDescriptions[random.Next(roomFeatureDescriptions.Length)];

            string description = $"A {light}, {shape} room with {wall} walls and {floor} flooring";

            if (!string.IsNullOrEmpty(feature))
            {
                description += " " + feature;
            }

            description += ".";

            return description;
        }

        // Predefined list of possible items
        private static readonly List<Item> possibleItems = new List<Item>
        {
            new Weapon("Iron Sword", "A basic but reliable iron sword", 5),
            new Weapon("Steel Sword", "A sharp, steel sword", 8),
            new Weapon("Diamond Sword", "A sword, made out of diamond?", 12),
            new Potion("Minor Healing Potion", "Restores 10 health", PotionEffect.Heal, 10),
            new Potion("Minor Strength Potion", "Increases strength by 2", PotionEffect.Strength, 2),
            new Potion("Minor Defence Potion", "Increases defence by 2", PotionEffect.Defence, 2),
        };

        // Predefined list of possible monsters
        private static readonly List<Monster> possibleMonsters = new List<Monster>
        {
            new Goblin("Goblin", 25, 0, 5),
            new Goblin("Goblin Knight", 30, 0, 7),
            new Goblin("Goblin Brute", 30, 1, 6),
            new Goblin("Armoured Goblin", 30, 2, 5),
            new Dragon("Boss Dragon", 50, 3, 8),
        };

        // Predefined list of possible monsters
        private static readonly List<int> possibleMonsterAmounts = new List<int>
        {
            0,
            0,
            0,
            0,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            2,
            2,
            2,
            3,
        };

        /// <summary>
        /// Returns the room at the specified position in the map
        /// </summary>
        /// <param name="position">The 2D position vector of the room</param>
        /// <returns>The Room at the position or null if position is out of bounds</returns>
        public static Room GetRoomAt(IVec2 position)
        {
            if (position.x < 0 || position.x >= roomMapSize || position.y < 0 || position.y >= roomMapSize) { return null; }
            return rooms[position.x, position.y];
        }

        /// <summary>
        /// Sets a room at the specified position in the map
        /// </summary>
        /// <param name="position">The 2D position vector where the room should be placed</param>
        /// <param name="room">The Room object to place at the position</param>
        public static void SetRoomAt(IVec2 position, Room room)
        {
            rooms[position.x, position.y] = room;
        }

        /// <summary>
        /// Gets all accessible rooms surrounding a given position and their relative offset directions
        /// </summary>
        /// <param name="position">The 2D position vector to check around</param>
        /// <returns>A tuple containing a list of accessible rooms and a list of their offset direction vectors</returns>
        public static (List<Room>, List<IVec2>) GetSurroundingRooms(IVec2 position)
        {
            // Gets all the rooms that are possible to travel to and also the offset of where they are, to display the location (left, right , etc)

            // Makes two lists, one for the rooms and another for the offsets
            List<Room> foundRooms = new List<Room>();
            List<IVec2> foundRoomsOffsets = new List<IVec2>();

            // The next lines simply take the position, and add a unit vector labelled by its direction
            // in order to get the room in that direction, then it checks if it exists and adds it to our lists

            IVec2 upPosition = position + UP;
            Room aboveRoom = GetRoomAt(upPosition);
            if (aboveRoom != null) { foundRooms.Add(aboveRoom); foundRoomsOffsets.Add(UP); }

            IVec2 downPosition = position + DOWN;
            Room downRoom = GetRoomAt(downPosition);
            if (downRoom != null) { foundRooms.Add(downRoom); foundRoomsOffsets.Add(DOWN); }

            IVec2 rightPosition = position + RIGHT;
            Room rightRoom = GetRoomAt(rightPosition);
            if (rightRoom != null) { foundRooms.Add(rightRoom); foundRoomsOffsets.Add(RIGHT); }

            IVec2 leftPosition = position + LEFT;
            Room leftRoom = GetRoomAt(leftPosition);
            if (leftRoom != null) { foundRooms.Add(leftRoom); foundRoomsOffsets.Add(LEFT); }



            return (foundRooms, foundRoomsOffsets);

        }

        /// <summary>
        /// Static constructor that initializes the game map with randomly generated rooms
        /// </summary>
        static GameMap()
        {
            rooms = new Room[roomMapSize, roomMapSize];
            // Fill up the room map with random rooms

            for (int i = 0; i < roomMapSize; i++)
            {
                for (int j = 0; j < roomMapSize; j++)
                {
                    // Select a random item from the predefined list
                    int randomItemIndex = random.Next(possibleItems.Count);
                    Item randomItem = possibleItems[randomItemIndex];

                    // Create a list containing the selected item
                    List<Item> roomItemsList = new List<Item> { randomItem };

                    // Select a random amount of monsters from the predefined list
                    int randomMonsterAmountIndex = random.Next(possibleMonsterAmounts.Count);
                    int randomAmountOfMonsters = possibleMonsterAmounts[randomMonsterAmountIndex];

                    List<Monster> randomMonstersList = new List<Monster> { };

                    for (int k = 0; k < randomAmountOfMonsters; k++)
                    {
                        // Select a random monster from the predefined list
                        int randomMonsterIndex = random.Next(possibleMonsters.Count);
                        Monster randomMonster = possibleMonsters[randomMonsterIndex];
                        randomMonstersList.Add(randomMonster);
                    }


                    // Create the room with the item list
                    Room randomRoom = new Room(GenerateRoomDescription(), roomItemsList, randomMonstersList);
                    rooms[i, j] = randomRoom;
                }
            }

        }
    }
}
