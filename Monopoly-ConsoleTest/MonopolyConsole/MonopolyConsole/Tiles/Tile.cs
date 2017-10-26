using System;

namespace MonopolyConsole
{
    public abstract class Tile
    {
        String propertyName;

        public Tile()
        {
        }

        public override bool Equals(object obj)
        {
            var tile = obj as Tile;
            return tile != null &&
                   propertyName == tile.propertyName;
        }

        //public abstract void TileAction(Player player);
    }
}