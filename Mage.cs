namespace GADE5112POE
{
    class Mage : Enemy
    {
        private int purse;

        public int Purse { get => purse; set => purse = value; }

        public Mage(int x, int y, int arrayIndex) : base(5, 5, x, y, 'M', arrayIndex)
        {
            this.hp = 5;
            this.damage = 5;
            purse = 3;
            Weapon = new MeleeWeapon(-1, -1, -1, MeleeWeapon.Types.Dagger);
        }

        public override void Pickup(Item item) { }

        public override Movement ReturnMove(Movement move = 0)
        {
            return move;
        }

        // Best Handled in Enemy class
        //public override string ToString()
        //{
        //    return "[" + X + "," + Y + "]" + " " + hp + "/" + maxHp;
        //}

        public override bool CheckRange(Character target)
        {
            if (target.X == X + 1 && target.Y == Y)
            {
                return true;
            }
            else if (target.X == X - 1 && target.Y == Y)
            {
                return true;
            }
            else if (target.X == X && target.Y == Y - 1)
            {
                return true;
            }
            else if (target.X==X && target.Y == Y + 1)
            {
                return true;
            }
            else if (target.X == X + 1 && target.Y == Y + 1)
            {
                return true;
            }
            else if (target.X == X - 1 && target.Y == Y - 1)
            {
                return true;
            }
            else if (target.X == X + 1 && target.Y == Y - 1)
            {
                return true;
            }
            else if (target.X == X - 1 && target.Y == Y + 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
