using System;
using System.Linq;

namespace DungeonExplorer
{
    public static class Utils
    {
        // Some helpful readonly values
        public readonly static IVec2 UP = new IVec2(0, 1);
        public readonly static IVec2 DOWN = new IVec2(0, -1);
        public readonly static IVec2 LEFT = new IVec2(-1, 0);
        public readonly static IVec2 RIGHT = new IVec2(1, 0);
        public static int ChoicePrompt(string prompt, string[] choices)
        {
            // A simple, reusable and safe choice prompt to get a user choice.

            while (true)
            {
                Console.WriteLine(prompt);
                foreach (var choice in choices.Select((text, idx) => new { idx, text }))
                {
                    Console.WriteLine($"{choice.idx} - {choice.text}");
                }
                Console.Write($"\n> ");
                string userInput = Console.ReadLine();
                bool convertedSuccessfully = Int32.TryParse(userInput, out int userIntegerInput);
                if (!convertedSuccessfully)
                {
                    Console.WriteLine("Couldn't convert choice to integer");
                    continue;
                }

                bool withinBounds = (userIntegerInput >= 0 && userIntegerInput < choices.Length);
                if (!withinBounds)
                {

                    Console.WriteLine("Choice number not within bounds");
                    continue;
                }

                return userIntegerInput;
            }


        }

        public struct IVec2
        {
            // Basic implementation of an integer vector with 2 components, used for 2d positioning of the rooms.

            public int x; public int y;

            public IVec2(int _x, int _y)
            {
                x = _x;
                y = _y;
            }

            public static IVec2 operator +(IVec2 v1, IVec2 v2) => new IVec2(v1.x + v2.x, v1.y + v2.y);
            public static IVec2 operator -(IVec2 v1, IVec2 v2) => new IVec2(v1.x - v2.x, v1.y - v2.y);

            public static bool operator ==(IVec2 v1, IVec2 v2) => (v1.x == v2.x && v1.y == v2.y);
            public static bool operator !=(IVec2 v1, IVec2 v2) => !(v1.x == v2.x && v1.y == v2.y);
        }
    }
}
