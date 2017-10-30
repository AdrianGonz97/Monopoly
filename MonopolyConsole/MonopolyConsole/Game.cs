class Game
{
    private const int go = 200;
    private const int jailFee = 50;

    public void PlayerTurn(Player player, bool turn)
    {
        int numDoubles = 0;
        int roll1;
        int roll2;
        bool notFinishedRolling = true;
        int previousPosition;
     
        /*
         * Case 1 rolls Dice.
         * Case 2
        */
        while (turn)
        {
            Console.WriteLine("Player choice");
            int decision = Console.Read();
            switch (decision)
            {
                case 1:
                    /*
                     * Checks if the player has not finsihed rolling then if they have not it checks
                     * If the player is not in jail the dice are rolled. If the play rolls doubles the numDoubles
                     * is incremented. If three doubles are rolled the player is sent directly to go.
                     * The player is then moved forward based on what they rolled, if they passed go they recieve
                     * $200. If the player did not roll doubles their turn is marked as over.
                     *
                     * In the case the player is in jail
                     */
                    if (notFinishedRolling)
                    {
                        if (!player.InJail())
                        {
                            notFinishedRolling = PlayerRollsDice(Player player, numDoubles);
                            if (notFinishedRolling)
                                ++numDoubles;

                        }
                        else
                        {
                            Console.WriteLine("Player is in Jail");
                            roll1 = Dice.Roll();
                            roll2 = Dice.Roll();
                            /*
                             *If the player has been in jail for three turns the jail fee is removed from the player, the
                             * player is removed from jail, and then the player rolls his dice. 
                            */
                            if (player.jailTurnNumber == 3)
                            {
                                player.RemoveMoney(jailFee); //Need to check if player is bankrupt
                                //If not bankrupt?
                                player.JailRelease();
                                notFinishedRolling = PlayerRollsDice(Player player, numDoubles);
                                if (notFinishedRolling)
                                    ++numDoubles;
                            }
                            else
                            {
                                /*
                                 * Player does not roll doubles advances jail turn
                                */
                                if (!Doubles(roll1, roll2))
                                {
                                    player.AdvanceJailTurn();
                                    notFinishedRolling = false;
                                }
                                /*
                                 * Player rolls doubles gets out of jail 
                                */
                                else if (Doubles(roll1, roll2))
                                {
                                    player.MovePlayer(roll1 + roll2);
                                    notFinishedRolling = false;
                                }
                            }
                        }
                    }
                    break;
                    
                /*
                 * Player Chooses to buy houses
                */
                case 2:
                    PlayerBuyHouses(player);
                    break;

            }
        }
        // switch and while loop
        /*
         * if player rolls dice (checks for doubles (also third    
         * double)for go to to jail option 
         * Will not unlock end turn option unless rolled tile action 
         * finished and the roll was not doubles.
         */

        // if player trades

        // if player declares bankruptcy

        // if player builds houses/hotels

        // if player sells

        // if player mortgages

        // if player unmortgages

        // if player rolls dice call check if numDoubles != 3 if it does send player to jail else method move_piece

        // if player ends turn must complete tile action and dice rolls

    }

    private bool PlayerRollsDice(Player player, int numDoubles, bool turn)
    {
        int roll1 = Dice.Roll();
        int roll2 = Dice.Roll();
        if (Doubles(roll1, roll2))
            ++numDoubles;
        if (numDoubles == 3)
        {
            player.goToJail();
            turn = false;
        }
        else
        {
            int previousPosition = player.PlayerPosition;
            player.MovePlayer(roll1 + roll2);
            //Before Tile action. If previous position is greater than their new position they passed go
            if (previousPosition > player.PlayerPosition)
                player.ReceiveMoney(go);
            if (!Doubles(roll1, roll2))
                turn = false;
        }
        return turn;
    }
    /*
     * Returns the corresponding property to the entered name
    */
    private IProperty FindProperty(String property, List<IProperty> propertyList)
    {
        for(int i = 0; i < propertyList.Count; ++i) //Runs through every property in the list and returns if the property corresponds to the entered property
        {
            if (property.Equals(propertyList[i].Name))
                return propertyList[i];
        }
        return null;
    }
    public bool Doubles(int roll1, int roll2)
    {
        return roll1 == roll2;
    }
    /*
     * Requests the player selected monopoly, the number of houses, and eventually which properties to add houses too.
     * It checks if the player can add houses to certain properties.
     * It adds the houses that must be added and then allows for the player to select which property to add houses to
    */
    private bool PlayerBuyHouses(Player player)
    {
        Console.WriteLine("What color property would you like to buy a house on?");
        String color = Console.ReadLine();//Needs a way to convert to a type property
        if (!player.MonopolyCheck(color)) //Checks if 
        {
            Console.WriteLine("Player does not have a monopoly on this color");
            return false;
        }
        List<int> monopolyIndexesList = GetMonopolizedPropertiesIndexList(color); //List of the monopoly properties index
        List<IProperty> monopolyList;
        for (int i = 0; i < monopolyIndexesList.Count; ++i)//Intializes List monopoly list with all of the properties in the monopoly
        {
            MonopolyList.add(GetProperty(monopolyIndexesList[i]));
        }
        Console.WriteLine("How many houses would you like to buy (Distributes houses evenly allows user to select leftover houses");

        int houseNum = Console.Read();
        if (monopolyList[0].Rent * houseNum > player.GetBalance)
        {
            Console.WriteLine("Insufficient funds to purchase all houses");
            return false;
        }
        else//Checks if any of the properties are mortgaged
        {
            for (int i = 0; i < monopolyList.Count; ++i)
            {
                if (monopolyList[i].IsMortgaged)
                {
                    Console.WriteLine("Can not build houses when on one of the monopoly houses is mortgaged");
                    return false;
                }
            }
            IProperty toAddHouse;
            while (houseNum > 2) //When there are more than two houses to add
            {
                for (int i = 0; i < monopolyList.Count; ++i)
                {
                    if (toAddHouse.NumOfHouses >= monopolyList[i].NumOfHouses) //Selects the House with the fewest or equal to fewest houses
                        toAddHouse = monopolyList[i];
                }
                player.BuyHouse(toAddHouse); //Player buys a house (it checks if he can purchase it, removes the correct money)
                --houseNum; //Player only needs to buy one fewer house
            }
            //Player adds the remaining houses to the eligble properties he desires to.
            while (houseNum != 0)
            {
                Console.WriteLine("Enter which property you would like to add a property to");
                String prop = Console.ReadLine();
                toAddHouse = FindProperty(prop, monopolyList); //Returns the corresponding property to the string or null
                if (toAddHouse != null)
                {
                    int temp = toAddHouse.NumOfHouses;
                    player.BuyHouse(toAddHouse);
                    if (temp < toAddHouse.NumOfHouses) //If the player added a house to the corresponding property
                        --houseNum;
                    else
                        Console.WriteLine("Can not add a house to that property");
                }
            }
        }
        return true;
    }
}
