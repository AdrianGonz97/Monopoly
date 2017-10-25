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
        String OwnerName { get; set; }   // name of the player who owns the tile
        String Color { get; set; }       // color of the tile
        String Name { get; set; }        // name of the tile
        int Rent { get; set; }           // rent cost of tile
        int NumOfHouses { get; set; }    // number of houses currently on the tile
        int CostOfHouse { get; set; }    // cost of a house
        int CostOfHotel { get; set; }    // cost of a hotel
        int MortgageValue { get; set; }  // mortgage value
        int MortgageCost { get; set; }   // cost to unmortgage
        int PropertyCost { get; set; }   // cost of the property
        bool IsMortgaged { get; set; }   // is the property mortgaged
        bool HasHotel { get; set; }      // does the tile have hotel
        bool IsOwned { get; set; }       // is the tile owned by any player

        void TileAction(Player player);
        int RentCost();
        void BuildHouse();
        void BuildHotel();
        bool Mortgage();
        bool Unmortgage();

    }
}
