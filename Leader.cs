using System;

namespace GADE5112POE
{
    public class Leader : Enemy
    {
        private Tile target;

        public Tile Target { get => target; set => target = value; }

        public Leader(int x, int y) : base(20, 2, x, y, '+', 0)
        {
            purse = 2;
            Weapon = new MeleeWeapon(-1, -1, -1, MeleeWeapon.Types.LongSword);
        }

        public override void Pickup(Item item)
        {
            if (item.GetType() == typeof(Gold))
            {
                Purse += item.Quantity;
                Program.MainForm.Output("Leader picked up " + item.Quantity + " gold, he now has: " + purse);
                Program.MainForm.Controls.Remove(item.Button);
            }
            else if (item.GetType().BaseType == typeof(Weapon))
            {
                weapon = (Weapon)item;
                Program.MainForm.Output("Leader has acquired a: " + item.ToString());
            }
        }

        public override Movement ReturnMove(Movement move = Movement.Idle)
        {
            if (X > target.X)
            {
                Tile visionTile = Vision[(int)Movement.Left];
                if (visionTile is EmptyTile || visionTile is Item)
                {
                    return Movement.Left;
                }
            }
            else if (X < target.X)
            {
                Tile visionTile = Vision[(int)Movement.Right];
                if (visionTile is EmptyTile || visionTile is Item)
                {
                    return Movement.Right;
                }
            }
            else if (Y < target.Y)
            {
                Tile visionTile = Vision[(int)Movement.Down];
                if (visionTile is EmptyTile || visionTile is Item)
                {
                    return Movement.Down;
                }
            }
            else if (Y > target.Y)
            {
                Tile visionTile = Vision[(int)Movement.Up];
                if (visionTile is EmptyTile || visionTile is Item)
                {
                    return Movement.Up;
                }
            }

            bool validDirection = false;

            Random random = new Random();
            while (!validDirection)
            {
                int direction = random.Next(1, 5);
                Tile visionTile = Vision[direction];
                if (visionTile is EmptyTile || visionTile is Item)
                {
                    validDirection = true;
                    return (Movement)(direction);
                }
            }

            return Movement.Idle;
        }

        public override string ToString()
        {
            return "Leader";
        }
    }

}
