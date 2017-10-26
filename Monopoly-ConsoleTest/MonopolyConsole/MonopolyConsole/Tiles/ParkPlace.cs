using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyConsole.Tiles
{
    class ParkPlace : Property, IProperty
    {
        public ParkPlace()
        {
            Color = "Dark Blue";
            Name = "Park Place";
            PropertyCost = 350;
            CostOfHouse = 200;
            CostOfHotel = 200;
            HouseValue = 100;
            HotelValue = 100;
            NumOfHouses = 0;
            MortgageValue = 175;
            MortgageCost = 193;
            HasHotel = false;
            IsOwned = false;
            IsMortgaged = false;
            noHouseRent = 35;
            oneHouseRent = 175;
            twoHouseRent = 500;
            threeHouseRent = 1100;
            fourHouseRent = 1300;
            hotelRent = 1500;
        }
    }
}
