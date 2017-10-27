using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyConsole
{
    interface IProperty 
    {
        Player Owner { get; set; }       // player who owns the tile
        String Color { get; set; }       // color of the tile
        String Name { get; set; }        // name of the tile
        int Rent { get; set; }           // rent cost of tile
        int NumOfHouses { get; set; }    // number of houses currently on the tile
        int CostOfHouse { get; set; }    // cost of a house
        int CostOfHotel { get; set; }    // cost of a hotel
        int HouseValue { get; set; }     // how much a house is worth sold
        int HotelValue { get; set; }     // how much a hotel is worth sold
        int MortgageValue { get; set; }  // mortgage value
        int MortgageCost { get; set; }   // cost to unmortgage
        int PropertyCost { get; set; }   // cost of the property
        bool IsMortgaged { get; set; }   // is the property mortgaged
        bool HasHotel { get; set; }      // does the tile have hotel
        bool IsOwned { get; set; }       // is the tile owned by any player

        void TileAction(Player player);
        bool Equals(Property property);
        int RentCost();
        bool Mortgage();  
        bool Unmortgage();
        void RemoveHouse();
        void RemoveHotel();
        void BuildHouse();
        void BuildHotel();
    }
}
