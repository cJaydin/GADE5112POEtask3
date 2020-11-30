using System;

namespace GADE5112POE
{
    public abstract class Enemy : Character
    {
        protected Random r = new Random();
        public int ArrayIndex { get; internal set; }

        public Enemy(int hp, int damage, int x, int y, char symbol, int arrayIndex) : base(hp, damage, hp, x, y, symbol)
        {
            ArrayIndex = arrayIndex;
        }

        public override string ToString()
        {
            //return base.ToString() + "at [" + X + "," + Y + "] (" + damage + ")";
            return GetType().Name + " at [" + X + "," + Y + "]  HP: " + Hp + "/" + maxHp + " (" + damage + " DMG)";
        }

    }
}
