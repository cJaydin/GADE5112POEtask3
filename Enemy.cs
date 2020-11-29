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

        internal bool Attack(Enemy goblin, frmMain formMain)
        {
            formMain.Output("Mage attacks goblin");
            goblin.Hp -= this.damage;
            formMain.Output(goblin.ToString());

            if (goblin.Hp < 1)
            {
                formMain.Output("Goblin has died");
                return true;
            }

            return false;
        }
    }
}
