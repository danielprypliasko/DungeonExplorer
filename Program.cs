using System;

namespace DungeonExplorer
{
    internal class Program
    {
        /// <summary>
        /// The entry point of the program
        /// </summary>
        /// <param name="args">Unused command line arguments that are passed in to the program</param>
        static void Main(string[] args)
        {
            Testing.RunTests();
            Game game = new Game();
            game.Start();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
