using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyConsole
{
    class Property
    {
        public Player Owner { get; set; }       // player who owns the tile
        public String Color { get; set; }       // color of the tile
        public String Name { get; set; }        // name of the tile
        public int Rent { get; set; }           // rent cost of tile
        public int NumOfHouses { get; set; }    // number of houses currently on the tile
        public int CostOfHouse { get; set; }    // cost of a house
        public int CostOfHotel { get; set; }    // cost of a hotel
        public int HouseValue { get; set; }     // how much a house is worth sold
        public int HotelValue { get; set; }     // how much a hotel is worth sold
        public int MortgageValue { get; set; }  // mortgage value
        public int MortgageCost { get; set; }   // cost to unmortgage
        public int PropertyCost { get; set; }   // cost of the property
        public bool IsMortgaged { get; set; }   // is the property mortgaged
        public bool HasHotel { get; set; }      // does the tile have hotel
        public bool IsOwned { get; set; }       // is the tile owned by any player

        protected int noHouseRent;              // rent value for an empty property
        protected int oneHouseRent;             // rent value for one house on property
        protected int twoHouseRent;             // rent value for two houses on property
        protected int threeHouseRent;           // rent value for three houses on property
        protected int fourHouseRent;            // rent value for four houses on property
        protected int hotelRent;                // rent value for a hotel on property

        /**
         * What happens when a player lands on the tile.
         * @param player - player who landed on the tile
         */
        public void TileAction(Player player)
        {
            if (!IsOwned) // if property is not owned..
            {
                // temp player action request for player to buy property
                Console.WriteLine($"Would you like to purchase PROPERTY {Name} for ${PropertyCost}?");
                String input = Console.ReadLine();
                if (input.Equals("yes"))
                {
                    if (player.GetBalance() >= PropertyCost) // if player has sufficient funds..
                    {
                        Owner = player;     // player becomes owner
                        player.RemoveMoney(PropertyCost);   // cost of property is removed from player's money balance
                        IsOwned = true; // property now has a owner
                        Console.WriteLine($"PLAYER {player.PlayerName} has purchased ");
                    }
                    else
                        Console.WriteLine($"PLAYER {player.PlayerName} does not have sufficient funds.");
                }
                else
                    Console.WriteLine($"PLAYER {player.PlayerName} did not purchase PROPERTY {Name}"); // player did not want to buy
            }
            else if(!(player.Equals(Owner))) // otherwise, if player is not the owner..
            {
                Console.WriteLine($"PLAYER {player.PlayerName} has landed on PLAYER {Owner.PlayerName}'s PROPERTY, {Name}!");

                player.PayRent(Owner, RentCost()); // player pays owner
            }
            else // otherwise, if player is the owner, nothing happens
                Console.WriteLine($"PLAYER {player.PlayerName} has landed on their own PROPERTY {Name}.");
        }

        /**
         * Adds a house to the property.
         */
        public void BuildHouse()
        {
            if (NumOfHouses != 4 && !HasHotel)
            {
                NumOfHouses++;
                Console.WriteLine($"PROPERTY {Name} has built a house! It now has {NumOfHouses} HOUSES.");
            }
            else
                Console.WriteLine($"PROPERTY {Name} has reached number of houses capacity!");
        }

        /**
         * Removes a house from the property.
         */
        public void RemoveHouse()
        {
            if (NumOfHouses != 0)
            {
                NumOfHouses--;
                Console.WriteLine($"PROPERTY {Name} has REMOVED a house! It now has {NumOfHouses} HOUSES.");
            }
        }

        /**
         * Removes a hotel from the property.
         */
        public void RemoveHotel()
        {
            if (HasHotel)
            {
                HasHotel = false;
                NumOfHouses = 4;
                Console.WriteLine($"PROPERTY {Name} has REMOVED a hotel");
            }
        }

        /**
         * Adds a hotel to the property and removes the houses.
         */
        public void BuildHotel()
        {
            if (!HasHotel) // if property does not have a hotel..
            {
                NumOfHouses = 0;
                HasHotel = true;
                Console.WriteLine($"PROPERTY {Name} now has a hotel");
            }
            else // otherwise..
                Console.WriteLine($"PROPERTY {Name} already has a hotel!");

        }

        /**
         * Property is mortgaged.
         */
        public bool Mortgage()
        {
            if (!IsMortgaged) // if property hasn't been mortgaged..
            {
                IsMortgaged = true;
                Console.WriteLine($"PROPERTY {Name} has been mortgaged!");
                return true;
            }
            else // otherwise..
                Console.WriteLine($"PROPERTY {Name} is already mortgaged!");

            return false;
        }

        /**
         * Property is unmortaged.
         */
        public bool Unmortgage()
        {
            if (IsMortgaged) // if property is mortgaged..
            {
                IsMortgaged = false;
                Console.WriteLine($"PROPERTY {Name} has been unmortgaged!");
                return true;
            }
            else // otherwise..
                Console.WriteLine($"PROPERTY {Name} is already unmortgaged!");
            return false;
        }

        /**
         * Computes the cost of rent of property.
         */
        public int RentCost()
        {
            if (!IsMortgaged)
            {
                if (NumOfHouses == 0)
                {
                    Rent = noHouseRent;
                }
                else if (NumOfHouses == 1)
                {
                    Rent = oneHouseRent;
                }
                else if (NumOfHouses == 2)
                {
                    Rent = twoHouseRent;
                }
                else if (NumOfHouses == 3)
                {
                    Rent = threeHouseRent;
                }
                else if (NumOfHouses == 4)
                {
                    Rent = fourHouseRent;
                }
                else if (HasHotel)
                {
                    Rent = hotelRent;
                }
            }
            else
            {
                Rent = 0;
            }

            return Rent;
        }


        /**
         * Checks to see if properties are the same.
         * @return true if they are the same, false if not.
         */
        public bool Equals(Property property)
        {
            if (Name.Equals(property.Name))
                return true;
            else
                return false;
        }
    }
}
