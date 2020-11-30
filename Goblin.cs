using System;

namespace GADE5112POE
{
    class Goblin : Enemy
    {
        public Goblin(int x, int y, int arrayIndex) : base(10, 1, x, y, 'G', arrayIndex)
        {
            purse = 1;
        }

        private Random random = new Random();

        public Movement GetRandomMovement()
        {
            int i = random.Next(1, 5);
            return (Movement)i;
        }

        public override Movement ReturnMove(Movement move = Movement.Idle)
        {
            bool movementIsValid = false;

            Movement movement = GetRandomMovement();
            while (!movementIsValid)
            {
                movement = GetRandomMovement();
                Tile target = Vision[(int)movement];
                Type targetType = target.GetType();
                if (targetType==typeof(EmptyTile) || target is Item) // && targetType!=typeof(Hero) )
                {
                    movementIsValid = true;
                }
            }

            return movement;
        }

        public override void Pickup(Item item)
        {
            if (item.GetType() == typeof(Gold))
            {
                Purse += item.Quantity;
                Program.MainForm.Output("Goblin picked up " + item.Quantity + " gold, it now has: " + purse);
                Program.MainForm.Controls.Remove(item.Button);
            }
            else if (item.GetType().BaseType == typeof(Weapon))
            {
                weapon = (Weapon)item;
                Program.MainForm.Output("Goblin has acquired a: " + item.ToString());
            }
        }

        // Best Handled in Enemy class
        //public override string ToString()
        //{
        //    return "[" + X + "," + Y + "]" + " " + hp + "/" + maxHp;
        //}
    }
}
