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
        public int PlayerPosition {get; set;}       // player position on board 0-39
        private int moneyBalance;                   // player's current amount of money
        private List<Tile> ownedProperty;           // properties owned by player
        public String PlayerName { get; set; }      // name of player
        public bool IsBankrupt { get; set; }        // is player bankrupt?
        public bool InJail {get; set;}              // is player in jail?
        public int JailTurnNumber {get; set;}       // player jail turn
        public const int DEFAULT_MONEY_START = 1500;// default amount of monet at the start of game
        
        /**
         * Player get assigned a name and begins with $1500.
         * @param playerName - name of the player
         */
        public Player(String playerName)
        {
            PlayerPosition = 0;                 // player starts at GO
            PlayerName = playerName;            // initializes player's name
            moneyBalance = DEFAULT_MONEY_START; // initializes money 
            InJail = false;                     // initializes player is not in jail
            JailTurnNumber = 0;                 // initializes player's turn number in jail (0, not in jail)
            IsBankrupt = false;                 // initializes player's bankruptcy status (not bankrupt)
        }

        /**
         * Mortgages the player's given property and receives the funds.
         * @param propertyName - the property for which the player wants to mortgage
         */
        public void MortgageProperty(IProperty property)
        {
            if (GetIndexOfProperty(property) != -1) // if player owns the property
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

            owner.ReceiveMoney(rentCost); // gives owner of property money

            Console.WriteLine($"Player {PlayerName} has paid {owner.PlayerName} ${rentCost} in rent!");
        }

        /**
         * Money is removed from player.
         * @param amount - amount of money to be removed from the player's total money
         */
        private void RemoveMoney(int amount)
        {
            moneyBalance -= amount;
            Console.WriteLine($"Player {PlayerName} has lost ${amount}, they now have ${moneyBalance}!");
        }

        /**
         * Player receives money.
         * @param amount - amount of money to be added to the player's total money
         */
        public void ReceiveMoney(int amount)
        {
            moneyBalance += amount;
            Console.WriteLine($"Player {PlayerName} has received ${amount}, they now have ${moneyBalance}!");
        }

        /**
         * Finds the index of the Tile in the player's posession.
         * @param property - the property whose index is to be found
         * @return the index of the Tile, otherwise -1 if player does not own the tile.
         */
        public int GetIndexOfProperty(IProperty property)
        {
            // traverses list to find the property
            foreach (IProperty tile in ownedProperty)
            {
                int i = 0; // counter to find index
                if (property == tile) // if the property is found...
                {
                    Console.WriteLine($"{property.Name} is in position {i} in Player {PlayerName}'s property list!");
                    return i; // return index
                }
                i++; // otherwise, check next index
            }

            Console.WriteLine($"Player {PlayerName} does not own {property.Name}!");
            return -1; // return -1 if property is not found
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
         * Sends the player to jail. Modifies the player's player position and begins the player JailTurn counter.
         */
        public void GoToJail()
        {
            PlayerPosition = 10;    // player goes to jail tile position num
            InJail = true;          // player is now incarcerated
            JailTurnNumber = 1;     // JailTurn counter begins

            Console.WriteLine($"Player {PlayerName} is now in Jail.");
        }

        /**
         * Player is released from jail and JailTurn counter is reset to 0.
         */
        public void JailRelease()
        {
            InJail = false;
            JailTurnNumber = 0;
            Console.WriteLine($"Player {PlayerName} has been released from Jail.");
        }

        /**
         * Checks if the player is indeed bankrupt.
         * @return true if player is bankrupt, false if not.
         */
        public bool CheckBankruptcy()
        {
            if (moneyBalance == 0) // if player has no money
            {
                int i = 0; // counter..
                foreach(IProperty tile in ownedProperty) // traverse their owned property
                {
                    if (tile.IsMortgaged == true) // if selected property is mortgaged
                    {
                        i++; // add to counter
                    }
                }

                if (i == ownedProperty.Count) // if all the properties that player owns are mortgaged
                {
                    IsBankrupt = true; // player is bankrupt

                    Console.WriteLine($"Player {PlayerName} is bankrupt!");

                    return IsBankrupt;
                }
            }
            // otherwise, player is not bankrupt
            IsBankrupt = false;

            Console.WriteLine($"Player {PlayerName} is not bankrupt!");

            return IsBankrupt;

        }
         
        /**
         * Moves player a given number of spaces.
         * @param amountToMove - number of spaces player gets to move
         */
        public void MovePlayer(int amountToMove)
        {
            PlayerPosition = (PlayerPosition + amountToMove) % 40;  // when PlayerPosition reaches 40, position resets
                                                                    // back to 0 (0 being GO and 39 being BoardWalk)
            Console.WriteLine($"Player {PlayerName} has moved {amountToMove} spaces and is now at position {PlayerPosition}");
        }
    }
}
