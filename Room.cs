
using System;
using System.Collections.Generic;
using static DungeonExplorer.Utils;

namespace DungeonExplorer
{
    public class Room
    {
        public string Description { get; private set; }
        public bool HasItem { get; private set; }
        //public string Item { get; private set; }
        public List<string> Items { get; private set; }
        public bool Visited { get; private set; }

        public Room(string description, List<string> items)
        {
            this.Description = description;
            //this.Item = item;
            this.Items = items;
            this.HasItem = true;
            this.Visited = false;
        }

        public string GetDescription()
        {
            return Description;
        }

        public string ListItems()
        {
            string output = "";
            foreach (var item in Items)
            {
                output += ($"{item}\n");
            }
            return output;
        }

        public void SetVisited() { this.Visited = true; }

        public string PickUpItem(int idx)
        {
            
            string item = Items[idx];
            Items.RemoveAt(idx);

            if (Items.Count == 0) { HasItem = false; }
            return item;
            //this.HasItem = false;
            //this.Item = null;
            //return this.Item;
        }
    }
    public static class RoomMap
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
        
        private static string[] roomItems = { 
            "Sword",
            "Healing Potion",
            "Strength Potion",
            "Shield",
            "Bow",
            "Map",
            "Key",
        };

        public static Room GetRoomAt(IVec2 position)
        {
            if (position.x < 0 || position.x  >= roomMapSize || position.y < 0 || position.y >= roomMapSize) { return null; }
            return rooms[position.x, position.y];
        }

        public static void SetRoomAt(IVec2 position, Room room)
        {
            rooms[position.x, position.y] = room;
        }

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
            if (aboveRoom != null) { foundRooms.Add(aboveRoom); foundRoomsOffsets.Add(UP);  }

            IVec2 downPosition = position + DOWN;
            Room downRoom = GetRoomAt(downPosition);
            if (downRoom != null) { foundRooms.Add(downRoom); foundRoomsOffsets.Add(DOWN); }

            IVec2 rightPosition = position + RIGHT;
            Room rightRoom = GetRoomAt(rightPosition);
            if (rightRoom != null) { foundRooms.Add(rightRoom);foundRoomsOffsets.Add(RIGHT); }

            IVec2 leftPosition = position + LEFT;
            Room leftRoom = GetRoomAt(leftPosition);
            if (leftRoom != null) { foundRooms.Add(leftRoom); foundRoomsOffsets.Add(LEFT); }



            return (foundRooms, foundRoomsOffsets);

        }

        static RoomMap()
        {
            rooms = new Room[roomMapSize, roomMapSize];
            // Fill up the room map with random rooms
            
            for (int i = 0; i < roomMapSize; i++)
            {
                for (int j = 0; j < roomMapSize; j++)
                {
                    int randomRoomItemIdx = random.Next(0, roomItems.Length);
                    string randomRoomItem = roomItems[randomRoomItemIdx];

                    List<string> randomRoomItems = new List<string>();
                    randomRoomItems.Add(randomRoomItem);
                    
                    Room randomRoom = new Room(GenerateRoomDescription(), randomRoomItems);
                    rooms[i, j] = randomRoom;
                }
            }
        }
    }
}

