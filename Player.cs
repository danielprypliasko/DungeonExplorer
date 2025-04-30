using System;
using System.Collections.Generic;
using System.Linq;

namespace DungeonExplorer
{
    /// <summary>
    /// An public class that manages the players state such as Name and inventory
    /// </summary>
    public class Player : Creature
    {
        /// <summary>
        /// The player's inventory.
        /// </summary>
        public Inventory Inv { get; private set; } = new Inventory();


        /// <summary>
        /// This constructor sets up the Player object with its name, health, defence and strength
        /// </summary>
        /// <param name="name">The name of the player</param>
        /// <param name="health">The health of the player</param>
        /// <param name="defence">The defence value of the player</param>
        /// <param name="strength">The strength value of the player</param>
        public Player(string name, int health, int defence, int strength) : base(name, health, defence, strength)
        {
        }

        /// <summary>
        /// The currently equipped weapon, null if no weapon is equipped
        /// </summary>
        public Weapon EquippedWeapon { get; private set; }

        /// <summary>
        /// This method equips a weapon for the player
        /// or if null, unequips the weapon
        /// </summary>
        /// <param name="weaponToEquip">The weapon to equip</param>
        public void EquipWeapon(Weapon weaponToEquip)
        {
            // Check if the item exists in the inventory
            if (weaponToEquip != null && Inv.Contains(weaponToEquip))
            {
                Console.WriteLine("Equipping");
                EquippedWeapon = weaponToEquip;
            }
            else if (weaponToEquip == null)
            {
                EquippedWeapon = null;
            }
        }

        /// <summary>
        /// This is a simple helper method to get the weapons damage
        /// or if null, returns 1 (fist damage, no weapon)
        /// </summary>
        /// <param name="weaponToEquip">The weapon to equip</param>
        private int GetWeaponDamage()
        {
            if (EquippedWeapon == null) return 1;
            return EquippedWeapon.Damage;
        }

        /// <summary>
        /// This method simply adds defence to the player
        /// </summary>
        /// <param name="defence">The defence to add</param>
        public void AddDefence(int defence)
        {
            Stats.Defence += defence;
        }

        /// <summary>
        /// This method simply adds strength to the player
        /// </summary>
        /// <param name="strength">The strength to add</param>
        public void AddStrength(int strength)
        {
            Stats.Strength += strength;
        }

        /// <summary>
        /// This method simply adds an item to the players inventory that is provided as an argument
        /// </summary>
        /// <param name="item">The item to pick up</param>
        public void PickUpItem(Item item)
        {
            Inv.Add(item);
        }

        /// <summary>
        /// This method returns a string describing the players inventory
        /// </summary>
        /// <returns>A string, describing what items the players inventory holds</returns>
        public string InventoryContents()
        {
            if (Inv.Count() == 0) return "Empty";
            // Use LINQ to get the names of the items
            return string.Join(", ", Inv.GetAll().Select(item => item.Name));
        }

        /// <summary>
        /// Damages another creature based on it's strength value
        /// Modified to use player's weapon damage too
        /// </summary>
        /// <param name="other">The creature to damage</param>
        /// <returns>An int, telling what damage it did to the other creature</returns>
        public override int DamageOtherCreature(Creature other)
        {
            var damage = Stats.Strength + GetWeaponDamage();

            return other.TakeDamage(damage);


        }
    }
}