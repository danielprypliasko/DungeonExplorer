using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    /// <summary>
    /// A basic class for creature statistics, including health, defence and strength
    /// </summary>
    public class Statistics
    {
        public int Health { get; set; }
        public int Defence { get; set; }
        public int Strength { get; set; }

        /// <summary>
        /// Creates a new instance of the Statistics class
        /// </summary>
        /// <param name="health">The health value</param>
        /// <param name="defence">The defence value</param>
        /// <param name="strength">The strength value</param>
        public Statistics(int health, int defence, int strength)
        {
            Health = health;
            Defence = defence;
            Strength = strength;
        }
    }
}
