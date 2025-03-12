using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class Testing
    {

        private static List<string> errors = new List<string>();
        private static List<string> successes = new List<string>();

        private static Random random = new Random();

        private static void MarkTest(string title, bool fail = false, string message = "")
        {
            if (fail) { errors.Add($"Test '{title}' failed. {message}"); }
            successes.Add($"Test '{title}' ran successfully.");
        }

        private static void RunTest(string testName, Action action)
        {

            try
            {
                action();
            }
            catch (Exception e)
            {
                MarkTest(testName,true,  e.Message);
                return;
            }

            MarkTest(testName);
        }


        public static void RunTests(bool showTests = false)
        {



            RunTest("basic room setup", () =>
            {
                List<string> items = new List<string>();
                items.Add("Test Item");

                Room room = new Room(RoomMap.GenerateRoomDescription(), items);
                
                Debug.Assert(room != null);
                Debug.Assert(room.Visited == false);

                room.SetVisited();

                Debug.Assert(room.Visited == true);
                Debug.Assert(room.Items[0] == "Test Item");

                room.PickUpItem(0);

                Debug.Assert(room.Items[0] == null);
            });

            RunTest("basic player test", () =>
            {
                Player player = new Player("Test Name", 99);

                player.PickUpItem("Test Item");

                Debug.Assert(player.InventoryContents() == "Test Item");
            });


            if (showTests)
            {
                PrintTestStatus();
            }

        }

        private static void PrintTestStatus()
        {
            foreach (string error in errors) { Console.WriteLine(error); }
            foreach (string success in successes) { Console.WriteLine(success); }
        }

    
    }
}
