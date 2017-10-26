using System;

namespace MonopolyConsole.Tiles
{
    class Boardwalk : Property, IProperty 
    {
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
            HouseValue = 100;
            HotelValue = 100;
            HasHotel = false;
            IsOwned = false;
            IsMortgaged = false;
            noHouseRent = 50;
            oneHouseRent = 200;               
            twoHouseRent = 600;               
            threeHouseRent = 1400;             
            fourHouseRent = 1700;
            hotelRent = 2000;
        }
    }
}