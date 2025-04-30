using System;
using System.Collections.Generic;
using System.Linq;
using static DungeonExplorer.Utils;

namespace DungeonExplorer
{
    /// <summary>
    /// An internal class that manages game state and is able to start the game
    /// </summary>
    internal class Game
    {
        private Player player;
        private Room currentRoom;

        private IVec2 position = new IVec2(0, 0);

        /// <summary>
        /// This constructor sets everything up before the game starts, for example the rooms and the player
        /// </summary>
        public Game()
        {
            // Initialize the game with a room and a player
            // Create an initial weapon for the first room
            Weapon initialSword = new Weapon("Old Sword", "A rusty, bent sword in questionable shape.", 2);
            List<Item> initialItems = new List<Item> { initialSword };
            Room firstRoom = new Room("A dark, rectangular room with brick walls and a wooden floor.", initialItems, new List<Monster>());

            GameMap.SetRoomAt(position, firstRoom);

            currentRoom = GameMap.GetRoomAt(position);


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

            player = new Player(playerName, 100, 0, 0);

        }

        /// <summary>
        /// This method starts the game and begins the players inputs.
        /// </summary>
        public void Start()
        {
            bool playing = true;
            while (playing)
            {
                if (player.Stats.Health <= 0)
                {
                    playing = false;
                }

                currentRoom.SetVisited();
                string[] choices = {
                    "Inspect the room (Room Description)",
                    "Inspect myself (Health, Inventory)",
                    "Look around (Explore)",
                    "Find an exit (List Doors)",
                    "Equip/Use items (Lists items)",
                    "Give up (Quit)"
                };
                int choice = ChoicePrompt("What would you like to do?", choices);

                switch (choice)
                {
                    case 0:
                        Console.WriteLine($"Room:\n{currentRoom.Description}");
                        break;
                    case 1:
                        Console.WriteLine($"Player '{player.Name}'\n\tHealth: {player.Stats.Health}\n\tItems: {player.InventoryContents()}\n\tEquipped Weapon: {(player.EquippedWeapon == null ? "None" : player.EquippedWeapon.Name)}\n\tAttack damage: {(player.Stats.Strength + (player.EquippedWeapon == null ? 1 : player.EquippedWeapon.Damage))}");
                        break;
                    case 2:
                        Console.WriteLine($"You look around the room");
                        bool hasMonsters = currentRoom.Monsters.Where(monster => !monster.Dead).ToList().Count > 0;
                        var ranFromMonsters = false;
                        if (hasMonsters)
                        {
                            Console.WriteLine("There's monsters!\n");
                            var exitFight = false;
                            while (!exitFight)
                            {
                                string[] fightChoices = {
                                    "Attack",
                                    "Use Potion",
                                    "Equip Weapon",
                                    "Run",
                                };


                                Console.WriteLine(currentRoom.GetMonsterStatus());
                                Console.WriteLine($"{player.Name}'s HP: {player.Stats.Health}");

                                int fightChoice = ChoicePrompt("What would you like to do?", fightChoices);

                                switch (fightChoice)
                                {
                                    case 0:
                                        var allMonstersDead = true;
                                        Console.WriteLine("Attacking the monster!");
                                        foreach (Monster monster in currentRoom.Monsters)
                                        {
                                            if (monster.Dead) continue;
                                            var monsterDamageToPlayer = monster.DamageOtherCreature(player);
                                            Console.WriteLine($"{player.Name} got hit for {monsterDamageToPlayer} damage by {monster.Name}!");
                                            if (player.Dead)
                                            {
                                                exitFight = true;
                                                playing = false;
                                                Console.WriteLine("You died!");
                                                break;
                                            }
                                            ;
                                            var playerDamageToCreature = player.DamageOtherCreature(monster);
                                            Console.WriteLine($"You hit the {monster.Name} for {playerDamageToCreature} damage!");

                                            if (monster.Dead)
                                            {
                                                Console.WriteLine($"You killed the {monster.Name}!");
                                            }
                                            else
                                            {
                                                allMonstersDead = false;
                                            }
                                        }
                                        if (allMonstersDead)
                                        {
                                            exitFight = true;
                                        }
                                        break;
                                    case 1:

                                        var playerPotions = player.Inv.GetAllPotions();
                                        List<string> potionChoicePrompt = new List<string>(playerPotions.Select(item => $"Use {item.Name} ({item.UsesLeft} uses left)"));
                                        potionChoicePrompt.Add("Exit");

                                        string[] potionChoices = potionChoicePrompt.ToArray();

                                        int potionChoice = ChoicePrompt("Which potion do you want to use?", potionChoices);


                                        if (potionChoice == potionChoices.Length - 1)
                                        {
                                            Console.WriteLine("You decide there are better things to do");
                                            break;
                                        }


                                        var potion = playerPotions[potionChoice];

                                        var success = potion.Use(player);
                                        if (success)
                                        {
                                            Console.WriteLine($"Used the {potion.Name}");
                                        }
                                        else
                                        {
                                            Console.WriteLine($"This potion has no uses left");
                                        }
                                        break;
                                    case 2:
                                        var playerWeapons = player.Inv.GetAllWeapons();
                                        List<string> weaponChoicePrompt = new List<string>(playerWeapons.Select(item => $"Equip {item.Name}{(item == player.EquippedWeapon ? " (Equipped)" : "")}"));
                                        weaponChoicePrompt.Add("Exit");

                                        string[] weaponChoices = weaponChoicePrompt.ToArray();

                                        int weaponChoice = ChoicePrompt("Which weapon do you want to equip?", weaponChoices);


                                        if (weaponChoice == weaponChoices.Length - 1)
                                        {
                                            Console.WriteLine("You decide there are better things to do");
                                            break;
                                        }


                                        var weapon = playerWeapons[weaponChoice];

                                        player.EquipWeapon(weapon);
                                        break;
                                    case 3:
                                        ranFromMonsters = true;
                                        exitFight = true;
                                        break;
                                }
                            }
                            if (ranFromMonsters)
                            {
                                Console.WriteLine("You managed to escape the fight");
                            }

                        }

                        if (ranFromMonsters) break;
                        if (!playing) break;

                        bool hasItem = currentRoom.HasItem;
                        if (!hasItem) { Console.WriteLine($"There's nothing of interest"); break; }
                        else { Console.WriteLine($"You find something.."); }

                        // Display item names for the choice prompt
                        List<string> itemChoicePrompt = new List<string>(currentRoom.Items.Select(item => $"Take {item.Name}"));
                        itemChoicePrompt.Add("Leave");

                        string[] itemChoices = itemChoicePrompt.ToArray();

                        int itemChoice = ChoicePrompt("What would you like to do?", itemChoices);


                        if (itemChoice == itemChoices.Length - 1)
                        {
                            Console.WriteLine("You decide there are better things to do");
                            break;
                        }

                        player.PickUpItem(currentRoom.PickUpItem(itemChoice));

                        break;
                    case 3:
                        var surroundingRooms = GameMap.GetSurroundingRooms(position);

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

                        if (roomChoice == roomChoices.Length - 1)
                        {
                            break;
                        }

                        Room newRoom = rooms[roomChoice];
                        position = position + roomsOffsets[roomChoice];
                        currentRoom = newRoom;
                        Console.WriteLine("You have entered a room");

                        break;
                    case 4:
                        var allPlayerItems = player.Inv.GetAll();

                        var weapons = allPlayerItems.OfType<Weapon>();
                        var potions = allPlayerItems.OfType<Potion>();
                        var otherItems = allPlayerItems.Where(item => !(item is Weapon) && !(item is Potion));

                        var orderedItems = ((IEnumerable<Item>)weapons).Concat(potions).Concat(otherItems).ToList();

                        List<string> playerItemChoicePrompt = orderedItems.Select(item =>
                        {
                            if (item is Weapon weapon)
                            {
                                return $"Equip {weapon.Name}{(player.EquippedWeapon == weapon ? " (Equipped)" : "")}";
                            }
                            else if (item is Potion potion)
                            {
                                return $"Drink {potion.Name}{(potion.UsesLeft == 0 ? " (No uses left)" : "")}";
                            }
                            else
                            {
                                return $"Use {item.Name}";
                            }
                        }).ToList();

                        playerItemChoicePrompt.Add("Exit");

                        string[] playerItemChoices = playerItemChoicePrompt.ToArray();

                        int playerItemChoice = ChoicePrompt("Which item do you want to use?", playerItemChoices);


                        if (playerItemChoice == playerItemChoices.Length - 1)
                        {
                            Console.WriteLine("You decide there are better things to do");
                            break;
                        }

                        Item itemOfChoice = orderedItems[playerItemChoice];

                        itemOfChoice.Use(player);

                        break;
                    case 5:
                        playing = false;
                        break;

                }


            }
        }


    }
}