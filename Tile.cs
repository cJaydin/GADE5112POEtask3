using System.Windows.Forms;

namespace GADE5112POE
{
    public abstract class Tile
    {
        public enum TileType { Hero, Enemy, Gold, Weapon, Leader }

        protected int x;
        protected int y;
        protected char symbol;
        public char Symbol { get => symbol; set => symbol = value; }
        public Button button;

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public Button Button { get => button; set => button = value; }

        public Tile(int x, int y, char smbl)
        {
            X = x;
            Y = y;
            symbol = smbl;
        }
    }
}
