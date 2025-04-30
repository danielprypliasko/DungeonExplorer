using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DungeonExplorer
{
    /// <summary>
    /// A class that provides testing functionality
    /// </summary>
    public class Testing
    {

        private static List<string> errors = new List<string>();
        private static List<string> successes = new List<string>();

        /// <summary>
        /// Records the result of a test, marking it as either successful or failed
        /// </summary>
        /// <param name="title">The name of the test</param>
        /// <param name="fail">Whether the test failed</param>
        /// <param name="message">Additional message to display if the test failed</param>
        private static void MarkTest(string title, bool fail = false, string message = "")
        {
            if (fail) { errors.Add($"Test '{title}' failed. {message}"); }
            successes.Add($"Test '{title}' ran successfully.");
        }


        /// <summary>
        /// Executes a test and handles any exceptions that may occur
        /// </summary>
        /// <param name="testName">The name of the test to run</param>
        /// <param name="action">The action to execute as the test</param>
        private static void RunTest(string testName, Action action)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                MarkTest(testName, true, e.Message);
                return;
            }

            MarkTest(testName);
        }

        /// <summary>
        /// Runs all the tests
        /// </summary>
        /// <param name="showTests">Whether to print test results</param>
        public static void RunTests(bool showTests = false)
        {

            RunTest("basic room setup", () =>
            {
                // Create a test weapon
                Weapon testWeapon = new Weapon("test sword", "a sword for testing", 7);
                List<Item> items = new List<Item> { testWeapon };

                Room room = new Room(GameMap.GenerateRoomDescription(), items, new List<Monster>());

                Debug.Assert(room != null);
                Debug.Assert(room.Visited == false);

                room.SetVisited();

                Debug.Assert(room.Visited == true);

                // Check if the weapon name matches
                Debug.Assert(room.Items[0].Name == "test sword");
                // Check if it's actually a Weapon and check its damage
                Debug.Assert(room.Items[0] is Weapon);
                Debug.Assert(((Weapon)room.Items[0]).Damage == 7);
                // Check HasItem flag before pickup
                Debug.Assert(room.HasItem == true);

                Item pickedUpItem = room.PickUpItem(0);

                // Check HasItem flag after pickup
                Debug.Assert(room.HasItem == false); // HasItem flag should be false

                // Check if the correct item was returned and the room state updated
                Debug.Assert(pickedUpItem.Name == "test sword");
                Debug.Assert(pickedUpItem is Weapon); // Ensure it's still a Weapon
                Debug.Assert(((Weapon)pickedUpItem).Damage == 7);
                Debug.Assert(room.Items.Count == 0); // List should be empty

            });

            RunTest("basic player test", () =>
            {
                // New player created with 85 health, no strength or defence
                Player player = new Player("test name", 85, 0, 0);

                // Create a test potion
                Potion testPotion = new Potion("test potion", "a potion for testing", PotionEffect.Heal, 15);
                player.PickUpItem(testPotion);

                // Check if the inventory contents string matches the potion name
                Debug.Assert(player.InventoryContents() == "test potion");

                // Use the potion
                testPotion.Use(player);

                // Check if the player was healed
                Debug.Assert(player.Stats.Health == 100);

                // Try use the potion again (no more uses)
                testPotion.Use(player);

                // Make sure the player can't use the potion when it's ran out of uses
                Debug.Assert(player.Stats.Health == 100);
            });

            RunTest("basic monster test", () =>
            {
                // New player created with 85 health, no strength or defence
                Player player = new Player("test name", 85, 0, 0);

                // Create a test potion
                Potion testPotion = new Potion("test potion", "a potion for testing", PotionEffect.Strength, 5);
                player.PickUpItem(testPotion);

                // Create a new goblin with 10 health, 1 defence and 1 strength
                Goblin testGoblin = new Goblin("test goblin", 10, 1, 1);

                // Player hits the goblin, should block it fully
                player.DamageOtherCreature(testGoblin);

                // Goblins defence stat should block attacks from the player with no weapon equipped
                Debug.Assert(testGoblin.Stats.Health == 10);

                // Drink the strength potion
                testPotion.Use(player);

                // Player hits the goblin now should damage it
                player.DamageOtherCreature(testGoblin);

                // Should be 5 damage dealt,
                // 1 damage from fists, 5 strength from the potion
                // 6 damage, but goblin has 1 defence, so 5 damage after reduction
                Debug.Assert(testGoblin.Stats.Health == 5);

                // Goblin hits the player
                testGoblin.DamageOtherCreature(player);

                // Goblin should have done 1 damage because of its strength
                Debug.Assert(player.Stats.Health == 84);
            });


            if (showTests)
            {
                PrintTestStatus();
            }

        }

        /// <summary>
        /// Prints the results of all tests
        /// </summary>
        private static void PrintTestStatus()
        {
            foreach (string error in errors) { Console.WriteLine(error); }
            foreach (string success in successes) { Console.WriteLine(success); }
        }

    }
}
