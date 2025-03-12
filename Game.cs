using System;
using System.Collections.Generic;
using System.Linq;
using static DungeonExplorer.Utils;

namespace DungeonExplorer
{
    internal class Game
    {
        private Player player;
        private Room currentRoom;

        private IVec2  position = new IVec2(0,0);

        public Game()
        {
            // Initialize the game with one room and one player
            List<string> items = new List<string>();
            items.Add("Sword");
            Room firstRoom = new Room("A dark, rectangular room with brick walls and a wooden floor.", items);

            RoomMap.SetRoomAt(position, firstRoom);

            currentRoom = RoomMap.GetRoomAt(position);


            // Get the player's name safely
            string playerName = string.Empty;
            while (true)
            {
                Console.Write("Your name: ");
                playerName = Console.ReadLine();
                if (string.IsNullOrEmpty(playerName))
                {
                    continue;
                }
                else
                {
                    playerName = playerName.Trim();
                    Console.Write($"Welcome {playerName}.\n\n\n");
                    break;
                }
            }
           
            player = new Player(playerName, 100);

        }
        public void Start()
        {
            bool playing = true;
            while (playing)
            {
                if (player.Health <= 0)
                {
                    playing = false;
                }

                currentRoom.SetVisited();
                string[] choices = {
                    "Inspect the room (Room Description)",
                    "Inspect myself (Health, Inventory)",
                    "Look around (Explore)",
                    "Find an exit (List Doors)",
                    "Give up (Quit)"
                };
                int choice = ChoicePrompt("What would you like to do?", choices);

                switch(choice)
                {
                    case 0:
                        Console.WriteLine($"Room:\n{currentRoom.Description}");
                        break;
                    case 1:
                        Console.WriteLine($"Player '{player.Name}'\n\tHealth: {player.Health}\n\tItems: {player.InventoryContents()}");
                        break;
                    case 2:
                        Console.WriteLine($"You look around the room");
                        bool hasItem = currentRoom.HasItem;
                        if (!hasItem) { Console.WriteLine($"There's nothing of interest"); break; }
                        else {Console.WriteLine($"You find something..");}

                        List<string> itemChoicePrompt = new List<string>(currentRoom.Items.Select((item) => { return $"Take {item}"; }));
                        itemChoicePrompt.Add("Leave");

                        string[] itemChoices = itemChoicePrompt.ToArray();
                        
                        int itemChoice = ChoicePrompt("What would you like to do?", itemChoices);


                        if (itemChoice == itemChoices.Length-1) {
                            Console.WriteLine("You decide there are better things to do");
                            break;
                        }

                        player.PickUpItem(currentRoom.PickUpItem(itemChoice));

                        break;
                    case 3:
                        var surroundingRooms = RoomMap.GetSurroundingRooms(position);

                        List<Room> rooms = surroundingRooms.Item1;
                        List<IVec2> roomsOffsets = surroundingRooms.Item2;

                        List<String> roomChoiceList = new List<String>();

                        for (int i = 0; i < rooms.Count; i++)
                        {
                            
                            Room room = rooms[i];
                            IVec2 offset = roomsOffsets[i];

                            bool visited = room.Visited;

                            string direction = "";
                            if (offset == UP) { direction = "Up"; }
                            else if (offset == DOWN) { direction = "Down"; }
                            else if (offset == LEFT) { direction = "Left"; }
                            else if (offset == RIGHT) { direction = "Right"; }


                            roomChoiceList.Add($"{direction} ({(visited ? "Visited" : "Not visited")})");
                            
                        }

                        roomChoiceList.Add("Cancel");

                        string[] roomChoices = roomChoiceList.ToArray();



                        int roomChoice = ChoicePrompt("Where would you like to go?", roomChoices);

                        if (roomChoice == roomChoices.Length -1)
                        {
                            break;
                        }

                        Room newRoom = rooms[roomChoice];
                        position = position + roomsOffsets[roomChoice];
                        currentRoom = newRoom;
                        Console.WriteLine("You have entered a room");

                        break;
                    case 4:
                        playing = false;
                        break;

                }
    
                
            }
        }

       
    }
}