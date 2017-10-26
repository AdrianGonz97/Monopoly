using System;

namespace MonopolyConsole.Tiles
{
    class Boardwalk : IProperty
    {
        public Player Owner { get; set; }       // player who owns the tile
        public String OwnerName { get; set; }   // name of the player who owns the tile
        public String Color { get; set; }       // color of the tile
        public String Name { get; set; }        // name of the tile
        public int Rent { get; set; }           // rent cost of tile
        public int NumOfHouses { get; set; }    // number of houses currently on the tile
        public int CostOfHouse { get; set; }    // cost of a house
        public int CostOfHotel { get; set; }    // cost of a hotel
        public int MortgageValue { get; set; }  // mortgage value
        public int MortgageCost { get; set; }   // cost to unmortgage
        public int PropertyCost { get; set; }   // cost of the property
        public bool IsMortgaged { get; set; }   // is the property mortgaged
        public bool HasHotel { get; set; }      // does the tile have hotel
        public bool IsOwned { get; set; }       // is the tile owned by any player



        public Boardwalk()
	    {
            Color = "Dark Blue";
            Name = "Boardwalk";
            PropertyCost = 400;
            CostOfHouse = 200;
            CostOfHotel = 200;
            NumOfHouses = 0;
            MortgageValue = 200;
            MortgageCost = 220;
            HasHotel = false;
            IsOwned = false;
            IsMortgaged = false;
	    }

        public void TileAction(Player player)
        {
            if(!IsOwned) // if property is not owned..
            {
                // temp player action request for player to buy property
                Console.WriteLine($"Would you like to purchase {Name} for {PropertyCost}?");
                String input = Console.ReadLine();
                if (input.Equals("yes"))
                {
                    if (player.GetBalance() >= PropertyCost)
                    {
                        Owner = player;
                    }
                    else
                        Console.WriteLine($"{player.PlayerName} does not have sufficient funds.");
                }
                else
                    Console.WriteLine($"{player.PlayerName} did not purchase {Name}"); // player did not want to buy
            }
            else // otherwise, player pays rent
            {
                player.PayRent(Owner, RentCost());
            }
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
                    Rent = 50;
                }
                else if (NumOfHouses == 1)
                {
                    Rent = 200;
                }
                else if (NumOfHouses == 2)
                {
                    Rent = 600;
                }
                else if (NumOfHouses == 3)
                {
                    Rent = 1400;
                }
                else if (NumOfHouses == 4)
                {
                    Rent = 1700;
                }
                else if (HasHotel)
                {
                    Rent = 2000;
                }
            }
            else
            {
                Rent = 0;
            }

            return Rent;
        }

        /**
         * Adds a house to the property.
         */
        public void BuildHouse()
        {
            if (NumOfHouses != 4 && !HasHotel)
            {
                NumOfHouses++;
                Console.WriteLine($"{Name} has built ");
            }
            else
                Console.WriteLine($"{Name} ");
        }

        /**
         * Adds a hotel to the property and removes the houses.
         */
        public void BuildHotel()
        {
            if(!HasHotel) // if property does not have a hotel..
            {
                NumOfHouses = 0;
                HasHotel = true;
                Console.WriteLine($"{Name} now has a hotel");
            }
            else // otherwise..
                Console.WriteLine($"{Name} already has a hotel!");

        }

        /**
         * Property is mortgaged.
         */
        public bool Mortgage()
        {
            if (!IsMortgaged) // if property hasn't been mortgaged..
            {
                IsMortgaged = true;
                Console.WriteLine($"{Name} has been mortgaged!");
                return true;
            }
            else // otherwise..
                Console.WriteLine($"{Name} is already mortgaged!");

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
                Console.WriteLine($"{Name} has been unmortgaged!");
                return true;
            }
            else // otherwise..
                Console.WriteLine($"{Name} is already unmortgaged!");
            return false; 
        }
    }
}