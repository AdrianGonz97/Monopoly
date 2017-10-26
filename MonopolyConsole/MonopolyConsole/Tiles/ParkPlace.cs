using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyConsole
{
    class ParkPlace
    {
        private Player owner;
        private String color;
        private String name;
        private int rent;
        private int numOfHouses;
        private int costOfHouse;
        private int costOfHotel;
        private int mortgageValue;
        private int mortgageCost;
        private int propertyCost;
        private bool isMortgaged;
        private bool hasHotel;
        private bool isOwned;



        public ParkPlace()
        {
            color = "Dark Blue";
            name = "Boardwalk";
            propertyCost = 400;
            costOfHouse = 200;
            costOfHotel = 200;
            numOfHouses = 0;
            mortgageValue = 200;
            mortgageCost = 220;
            hasHotel = false;
            isOwned = false;

        }

        public override void TileAction(Player player)
        {
            if (!isOwned)
            {

                if () // if player wants to buy the property
                {
                    // fdsf
                    owner = player;
                }
            }
            else
            {
                player.PayRent(owner, RentCost());
            }
        }

        /**
         * Computes the cost of rent of property.
         */
        public int RentCost()
        {
            if (!isMortgaged)
            {
                if (numOfHouses == 0)
                {
                    rent = 50;
                }
                else if (numOfHouses == 1)
                {
                    rent = 200;
                }
                else if (numOfHouses == 2)
                {
                    rent = 600;
                }
                else if (numOfHouses == 3)
                {
                    rent = 1400;
                }
                else if (numOfHouses == 4)
                {
                    rent = 1700;
                }
                else if (hasHotel)
                {
                    rent = 2000;
                }
            }
            else
            {
                rent = 0;
            }

            return rent;
        }

        /**
         * Adds a house to the property.
         */
        public void BuildHouse()
        {

        }

        /**
         * Adds a hotel to the property.
         */
        public void BuildHotel()
        {
            hasHotel = true;
        }
    }
}
