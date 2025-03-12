
using System;
using System.Collections.Generic;
using static DungeonExplorer.Utils;

namespace DungeonExplorer
{
    public class Room
    {
        public string Description { get; private set; }
        public bool HasItem { get; private set; }
        public string Item { get; private set; }
        public bool Visited { get; private set; }

        public Room(string description, string item)
        {
            this.Description = description;
            this.Item = item;
            this.HasItem = true;
            this.Visited = false;
        }


        public void SetVisited() { this.Visited = true; }

        public string PickUpItem()
        {
            this.HasItem = false;
            return this.Item;
        }
    }
    public static class RoomMap
    {
        private static Room[,] rooms;

        private static int roomMapSize = 4;

        private static string[] roomDescriptions = { 
            "A dimly-lit, square room with light walls and a wooden floor.",
            "A well-lit, square room with brick walls and a concrete floor.",
            "A dark, rectangular room with wooden walls and a wooden floor."
        };

        
        private static string[] roomItems = { 
           "Sword",
           "Healing Potion",
            "Strength Potion",
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
            Random random = new Random();
            for (int i = 0; i < roomMapSize; i++)
            {
                for (int j = 0; j < roomMapSize; j++)
                {
                    int randomRoomDescriptionIdx = random.Next(0, roomDescriptions.Length);
                    string randomRoomDescription = roomDescriptions[randomRoomDescriptionIdx];
                    
                    int randomRoomItemIdx = random.Next(0, roomItems.Length);
                    string randomRoomItem = roomItems[randomRoomItemIdx];
                    
                    Room randomRoom = new Room(randomRoomDescription, randomRoomItem);
                    rooms[i, j] = randomRoom;
                }
            }
        }
    }
}

