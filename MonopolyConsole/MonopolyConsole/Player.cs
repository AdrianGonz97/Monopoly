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
        public int PlayerPosition {get; set;}               // player position on board 0-39
        private int moneyBalance;                           // player's current amount of money
        private List<IProperty> ownedProperty;              // properties owned by player
        //public List<Card> GetOutOfJailCards { get; set; } // amount of get out of jail free cards player owns
        public String PlayerName { get; set; }              // name of player
        public bool IsBankrupt { get; set; }                // is player bankrupt?
        public bool InJail {get; set;}                      // is player in jail?
        public int JailTurnNumber {get; set;}               // player jail turn
        public const int DEFAULT_MONEY_START = 1500;        // default amount of monet at the start of game
        
        /**
         * Player get assigned a name and begins with $1500.
         * @param playerName - name of the player
         */
        public Player(String playerName)
        {
            ownedProperty = new List<IProperty>();  // initializes player's owned property
            PlayerPosition = 0;                     // player starts at GO
            PlayerName = playerName;                // initializes player's name
            moneyBalance = DEFAULT_MONEY_START;     // initializes money 
            InJail = false;                         // initializes player is not in jail
            JailTurnNumber = 0;                     // initializes player's turn number in jail (0, not in jail)
            IsBankrupt = false;                     // initializes player's bankruptcy status (not bankrupt)
        }

        /**
         * Mortgages the player's given property and receives the funds.
         * @param property - the property for which the player wants to mortgage
         */
        public void MortgageProperty(IProperty property)
        {
            if (GetIndexOfProperty(property) != -1) // if player owns the property..
            {
                if (!property.IsMortgaged) // if property is not already mortgaged..
                {
                    property.Mortgage(); // mortgages property
                    ReceiveMoney(property.MortgageValue); // add mortgage value to player balance
                    Console.WriteLine($"PLAYER {PlayerName} has MORTGAGED the PROPERTY {property.Name} for ${property.MortgageValue}");
                }
            }
        }

        /**
         * Unmortgages the player's given property and removes the funds.
         * @param property - the property for which the player wants to unmortgage
         */
        public void UnmortgageProperty(IProperty property)
        {
            if (GetIndexOfProperty(property) != -1) // if player owns the property..
            {
                if (property.IsMortgaged && moneyBalance >= property.MortgageCost) // if property is mortgaged and if they can afford it..
                {
                    property.Unmortgage(); // unmortgages property
                    RemoveMoney(property.MortgageValue); // removes mortgage cost from player balance
                    Console.WriteLine($"PLAYER {PlayerName} has UNMORTGAGED the PROPERTY {property.Name} for ${property.MortgageCost}");
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

            Console.WriteLine($"PLAYER {PlayerName} has PAID {owner.PlayerName} ${rentCost} in rent!");
        }

        /**
         * Money is removed from player.
         * @param amount - amount of money to be removed from the player's total money
         */
        public void RemoveMoney(int amount)
        {
            moneyBalance -= amount;
            Console.WriteLine($"PLAYER {PlayerName} has LOST ${amount}, they now have ${moneyBalance}!");
        }

        /**
         * Player receives money.
         * @param amount - amount of money to be added to the player's total money
         */
        public void ReceiveMoney(int amount)
        {
            moneyBalance += amount;
            Console.WriteLine($"PLAYER {PlayerName} has RECEIVED ${amount}, they now have ${moneyBalance}!");
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

            Console.WriteLine($"PLAYER {PlayerName} is now in Jail.");
        }

        /**
         * Advances jail turn by one.
         */
         public void AdvanceJailTurn()
        {
            JailTurnNumber++;
            Console.WriteLine($"PLAYER {PlayerName} jail turn is now at {JailTurnNumber}");
        }

        /**
         * Player is released from jail and JailTurn counter is reset to 0.
         */
        public void JailRelease()
        {
            InJail = false;
            JailTurnNumber = 0;
            Console.WriteLine($"PLAYER {PlayerName} has been RELEASED from Jail.");
        }

        /**
         * Checks if the player is indeed bankrupt.
         * @return true if player is bankrupt, false if not.
         */
        public bool CheckBankruptcy()
        {
            if (moneyBalance < 0) // if player has no money or is in negatives..
            {
                int i = 0; // counter
                foreach(IProperty tile in ownedProperty) // traverse their owned property to see if all properties are mortgaged
                {
                    if (tile.IsMortgaged == true) // if selected property is mortgaged..
                    {
                        i++; // add to counter
                    }
                }

                if (i == ownedProperty.Count) // if all the properties that player owns are mortgaged..
                {
                    IsBankrupt = true; // player is bankrupt

                    Console.WriteLine($"PLAYER {PlayerName} IS bankrupt!");

                    return IsBankrupt;
                }
            }
            // otherwise, player is not bankrupt
            IsBankrupt = false;

            Console.WriteLine($"PLAYER {PlayerName} is NOT bankrupt!");

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
            Console.WriteLine($"PLAYER {PlayerName} has MOVED {amountToMove} spaces and is now at POSITION {PlayerPosition}");
        }

        /**
         * Trades properties, get out of jail free cards, money.
         */
        public void Trade(Player trader, IProperty[] properties)
         {


            //Console.WriteLine($"Player {PlayerName} has moved {amountToMove} spaces and is now at position {PlayerPosition}");
        }

        /**
         * Checks if whether the player owns a monopoly of properties.
         * @param color - color of the property needed to be found.
         * @return true if player has a monopoly on properties of a given color, false if not.
         */
        public bool MonopolyCheck(String color)
        {
            int num = 0; // counter of how many same colored properties player owns
            bool hasMonopoly = false;

            for(int i = 0; i < ownedProperty.Count; i++)    // traveres owned properties list to find matching colored properties
            {
                if (color.Equals(ownedProperty[i].Color))   // if given color matches the property's color..
                {
                    num++;  // increment counter
                }
            }

            if (color.Equals("Dark Blue") || color.Equals("Brown")) // if color is of properties that only have 2 tiles
            {
                hasMonopoly = (num == 2);

                if (hasMonopoly) // if player has a monopoly..
                {
                    Console.WriteLine($"PLAYER {PlayerName} has a MONOPOLY on COLOR {color}");
                }
                else // otherwise..
                    Console.WriteLine($"PLAYER {PlayerName} does NOT have a MONOPOLY on COLOR {color}");

                return hasMonopoly;    // return true if num of properties of that color found is 2, false if not
            }
            else
            {
                hasMonopoly = (num == 3);

                if(hasMonopoly) // if player has a monopoly..
                {
                    Console.WriteLine($"PLAYER {PlayerName} has a MONOPOLY on COLOR {color}");
                }
                else // otherwise..
                    Console.WriteLine($"PLAYER {PlayerName} does NOT have a MONOPOLY on COLOR {color}");

                return hasMonopoly;    // return true if num of properties of that color found is 3, false if not
            }
        }

        /**
         * Adds a property to the player's owned property list.
         * @param property - the property to be added to the player's owned list.
         */
        public void AddProperty(IProperty property)
        {
            if (GetIndexOfProperty(property) == -1 && !property.IsOwned) // if player does not own the property already..
            {
                ownedProperty.Add(property); // adds the property to the owned list
                Console.WriteLine($"PLAYER {PlayerName} has ADDED PROPERTY {property.Name} to their owned list!");
            }
            else // otherwise, player already owns this property
                Console.WriteLine($"PLAYER {PlayerName} already owns PROPERTY {property.Name}!"); 
        }

        /**
         * Removes a property from the player's owned list.
         * @param property - the property to be removed from the player's owned list.
         */
        public void RemoveProperty(IProperty property)
        {
            if (GetIndexOfProperty(property) != -1) // if player owns the property
            {
                ownedProperty.Remove(property); // remove property from the list
                Console.WriteLine($"PLAYER {PlayerName} has REMOVED PROPERTY {property.Name} from their owned list!");
            }
            else // otherwise, nothing happens 
                Console.WriteLine($"PLAYER {PlayerName} does not own PROPERTY {property.Name}!");
        }

        /**
         * Finds the indicies of the properties that player owns that are monopolized.
         * @param color - the color of the potential monopolized properties.
         * @return a list of index values of the location of the monopolized properties.
         */
        public List<int> GetMonopolizedPropertiesIndexList(String color)
        {
            List<int> index = new List<int>();

            if (MonopolyCheck(color)) // if player has a monopoly of that color..
            {
                for (int i = 0; i < ownedProperty.Count; i++)
                {
                    if (ownedProperty[i].Equals(color))
                    {
                        index.Add(i);
                    }
                }
                return index;
            }
            return index; // impossible to reach this point, but for the sake of compiling..
        }

        /**
         * Overly complicated buy house method that uses logic to determine whether a player can purchase and build a house on given property.
         * @param property - the property that is wanted have a house built upon.
         */
        public void BuyHouse(IProperty property)
        {
            if (MonopolyCheck(property.Color) && moneyBalance >= property.CostOfHouse) // if player has a monopoly on the color and if they can afford it..
            {
                List<int> index = GetMonopolizedPropertiesIndexList(property.Color);
                int propertyIndex = GetIndexOfProperty(property);   // index of property being tested

                index.Remove(propertyIndex); // removes tested property from list

                if (property.NumOfHouses <= GetProperty(index[0]).NumOfHouses) // if second property has the same or more number of houses as the tested one..
                {
                    if (index.Count == 2) // if there are 3 properties total (property being tested was removed from list earlier)..
                    {
                        if (property.NumOfHouses <= GetProperty(index[1]).NumOfHouses) // check the third property for if the number of houses are equal or more..
                        {
                            RemoveMoney(property.CostOfHouse); // charges the player the cost to build a house
                            property.BuildHouse();  // builds house
                            Console.WriteLine($"PLAYER {PlayerName} has built a house on PROPERTY {property.Name}, which now has {property.NumOfHouses} houses!");
                        }
                        else
                            Console.WriteLine($"PLAYER {PlayerName} HOUSE BUILD DENIED! PROPERTY {GetProperty(index[1]).Name} requires a house house before " +
                                $"PLAYER can build another on PROPERTY {property.Name}!");
                    }
                    else // otherwise, monopoly is on a dark blue or a brown..
                    {
                        RemoveMoney(property.CostOfHouse); // charges the player the cost to build a house
                        property.BuildHouse();  // builds house
                        Console.WriteLine($"PLAYER {PlayerName} has built a house on PROPERTY {property.Name}, which now has {property.NumOfHouses} houses!");
                    }
                }
                else // otherwise, player can't build on property because other colored property does not have enough houses built
                    Console.WriteLine($"PLAYER {PlayerName} HOUSE BUILD DENIED! PROPERTY {GetProperty(index[0]).Name} requires a house house before " +
                                $"PLAYER can build another on PROPERTY {property.Name}!");
            }
            else // otherwise, player can't build on property because of funds or lack of monopoly on color
                Console.WriteLine($"PLAYER {PlayerName} HOUSE BUILD DENIED! PLAYER does not have a monopoly on COLOR {property.Color} OR does not " +
                    $"have sufficient funds to build on PROPERTY {property.Name}!");
        }

        /**
         * 
         */
        public void SellHouse(IProperty property)
        {

        }

        /**
         * 
         */
        public void BuyHotel()
        {

        }

        /**
         * 
         */
        public void SellHotel()
        {

        }

        /**
         * Returns property from player's owned property list.
         * @param indexOfProperty - the index at which the property is located.
         * @return the property at the given index.
         */ 
        public IProperty GetProperty(int indexOfProperty)
        {
            IProperty propertyTile = ownedProperty[indexOfProperty];

            return propertyTile;
        }

        /**
         * Finds the index of the Tile in the player's posession.
         * @param property - the property whose index is to be found
         * @return the index of the Tile, otherwise -1 if player does not own the tile.
         */
        public int GetIndexOfProperty(IProperty property)
        {
            // traverses list to find the property
            for(int i = 0; i < ownedProperty.Count; i++)
            {
                if(ownedProperty[i].Equals(property))
                {
                    Console.WriteLine($"The index of PROPERTY {property.Name} is {i}.");
                    return i;
                }
            }
            
            Console.WriteLine($"PLAYER {PlayerName} does not own PROPERTY {property.Name}!");
            return -1; // return -1 if property is not found / player does not
        }

        /**
         * Checks if both player objects are equal to eachother.
         * @param player - player object that is being checked.
         * @return true if the two objects are equal, false if not.
         */
        public bool Equals(Player player)
        {
            if (player.PlayerName.Equals(PlayerName))
                return true;
            else
                return false;
        }
        
    }
}
