using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyConsole
{
    /**
     * Player of the game Monopoly.
     */
    class Player
    {
        private int playerPosition;         // player position on board 0-39
        private int moneyBalance;           // player's current amount of money
        private List<Tile> ownedProperty;   // properties owned by player
        private String playerName;          // name of player
        private bool isBankrupt;            // is player bankrupt?
        private bool inJail;                // is player in jail?
        private int jailTurnNumber;         // player jail turn
        
        /**
         * Player get assigned a name and begins with $1500.
         * @param playerName - name of the player
         */
        public Player(String playerName)
        {
            playerPosition = 0;
            this.playerName = playerName;
            moneyBalance = 1500;
        }

        /**
         * Mortgages the player's given property and receives the funds.
         * @param propertyName - the property for which the player wants to mortgage
         */
        public void MortgageProperty(IProperty property)
        {
            if (GetIndex(property) != -1) // if player owns the property
            {
                if (!property.IsMortgaged)
                {
                    property.Mortgage();
                    moneyBalance += property.MortgageValue;
                }
            }
        }

        /**
         * This player pays rent to the owner of the property for the given cost.
         * @param owner - owner of the property that player landed on
         * @param rentCost - amount of money player has to pay the owner
         */
        public void PayRent(Player owner, int rentCost)
        {
            RemoveMoney(rentCost); // removes money from player's balance

            owner.RemoveMoney(rentCost); // gives owner of property money
        }

        /**
         * Money is removed from player.
         * @param amount - amount of money to be removed from the player's total money
         */
        private void RemoveMoney(int amount)
        {
            moneyBalance -= amount;
        }

        /**
         * Player receives money.
         * @param amount - amount of money to be added to the player's total money
         */
        public void ReceiveMoney(int amount)
        {
            moneyBalance += amount;
        }

        /**
         * Finds the index of the Tile
         * @param property - the property whose index is to be found
         * @return the index of the Tile, otherwise -1 if player does not own the tile.
         */
        public int GetIndex(IProperty property)
        {
            // traverses list to find the property
            foreach (IProperty tile in ownedProperty)
            {
                int i = 0;
                if (property == tile)
                {
                    return i;
                }
                i++;
            }

            return -1;
        }

        /**
         * Gets the player's current money balance.
         * @return the amount of money the player currently holds
         */
         public int GetBalance()
        {
            return moneyBalance;
        }

        /**
         * Gets the player's name.
         * @return name of the player
         */
         public String GetPlayerName()
        {
            return playerName;
        }
    }
}
