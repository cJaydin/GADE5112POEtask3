using System;

namespace GADE5112POE
{
    public class Hero : Character
    {
        public Hero(int x, int y, int hp) : base(hp, 2, 100, x, y, 'H') { }

        public override Movement ReturnMove(Movement move = Movement.Idle)
        {
            if (move==Movement.Idle) { return move; }

            Type visionType = Vision[(int)move].GetType();

            if (visionType==typeof(EmptyTile) || visionType.BaseType==typeof(Item) || visionType.BaseType==typeof(Weapon))
            {
                return move;
            } else
            {
                return Movement.Idle;
            }
        }

        public override void Pickup(Item item)
        {
            if (item.GetType()==typeof(Gold))
            {
                Purse += item.Quantity;
                Program.MainForm.Output("Hero picked up " + item.Quantity + " gold");
                Program.MainForm.Controls.Remove(item.Button);
            } else if (item.GetType().BaseType==typeof(Weapon))
            {
                weapon = (Weapon)item;
                Program.MainForm.Output("Hero has acquired a: " + item.ToString());
            }
        }

        public override string ToString()
        {
            //return "Player Stats: " + Environment.NewLine + "HP: " + Hp + "/" + MaxHp + Environment.NewLine + "Damage: " + Damage + Environment.NewLine + "[" + X + "," + Y + "]";
            return "Player Stats: [" + X + ", " + Y + "] HP: " + Hp + "/" + MaxHp + " (" + Damage + " DMG) " + Purse + " gold";
        }
    }
}
