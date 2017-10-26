using System;
using MonopolyConsole.Tiles;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            IProperty boardwalk = new Boardwalk();
            IProperty parkplace = new ParkPlace();

            Player player = new Player("George Clooney");

            Console.WriteLine($"PLAYER {player.PlayerName} has ${player.GetBalance()}. Change his name now!");

            player.AddProperty(boardwalk);
            player.AddProperty(parkplace);

            Console.WriteLine($"PLAYER {player.PlayerName} owns {player.GetProperty(player.GetIndexOfProperty(boardwalk)).Name} and " +
                $"{player.GetProperty(player.GetIndexOfProperty(parkplace)).Name}. Change his name now!");

            player.RemoveProperty(parkplace);
            player.RemoveProperty(parkplace);
            player.RemoveProperty(parkplace);

            player.AddProperty(boardwalk);
            player.AddProperty(boardwalk);

            player.RemoveProperty(boardwalk);

            player.AddProperty(parkplace);
            player.AddProperty(boardwalk);

            player.RemoveProperty(parkplace);
            player.RemoveProperty(boardwalk);

            String input = Console.ReadLine();

            player.PlayerName = input;
            
            Console.WriteLine($"PLAYER {player.PlayerName}");
        }
    }
}
