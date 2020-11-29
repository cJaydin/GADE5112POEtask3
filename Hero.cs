using System;

namespace GADE5112POE
{
    public class Hero : Character
    {
        private int purse;

        public int Purse { get => purse; set => purse = value; }

        public Hero(int x, int y, int hp) : base(hp, 2, 10, x, y, 'H') { }

        public override Movement ReturnMove(Movement move = Movement.Idle)
        {
            if (move==Movement.Idle) { return move; }

            Type visionType = Vision[(int)move].GetType();

            if (visionType==typeof(EmptyTile) || visionType.BaseType==typeof(Item))
            {
                return move;
            } else
            {
                return Movement.Idle;
            }
        }

        public override void Pickup(Item item, frmMain mainForm)
        {
            if (item.GetType()==typeof(Gold))
            {
                Purse += item.Quantity;
                mainForm.Output("Hero picked up " + item.Quantity + " gold");
                mainForm.Controls.Remove(item.Button);
            }
        }

        public override string ToString()
        {
            //return "Player Stats: " + Environment.NewLine + "HP: " + Hp + "/" + MaxHp + Environment.NewLine + "Damage: " + Damage + Environment.NewLine + "[" + X + "," + Y + "]";
            return "Player Stats: [" + X + ", " + Y + "] HP: " + Hp + "/" + MaxHp + " (" + Damage + " DMG) " + Purse + " gold";
        }
    }
}
