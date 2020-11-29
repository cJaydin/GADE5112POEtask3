using System;

namespace GADE5112POE
{
    class Goblin : Enemy
    {
        private int purse;

        public int Purse { get => purse; set => purse = value; }

        public Goblin(int x, int y, int arrayIndex) : base(10, 1, x, y, 'G', arrayIndex) { }

        private Random random = new Random();

        public Movement GetRandomMovement()
        {
            int i = random.Next(1, 4);
            return (Movement)i;
        }

        public override Movement ReturnMove(Movement move = Movement.Idle)
        {
            bool movementIsValid = false;

            Movement movement = GetRandomMovement();
            while (!movementIsValid)
            {
                movement = GetRandomMovement();
                Type targetType = Vision[(int)movement].GetType();
                if (targetType!=typeof(Obstacle)) // && targetType!=typeof(Hero) )
                {
                    movementIsValid = true;
                }
            }

            return movement;
        }

        public override void Pickup(Item item, frmMain mainForm)
        {
            if (item.GetType() == typeof(Gold))
            {
                Purse += item.Quantity;
                mainForm.Output("Goblin picked up " + item.Quantity + "gold");
            }
        }

        // Best Handled in Enemy class
        //public override string ToString()
        //{
        //    return "[" + X + "," + Y + "]" + " " + hp + "/" + maxHp;
        //}
    }
}
