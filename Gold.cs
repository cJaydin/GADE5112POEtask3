using System;

namespace GADE5112POE
{
    class Gold : Item
    {
        private Random random = new Random();

        protected int goldDrop;

        protected int GoldDrop { get => goldDrop; set => goldDrop = value; }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public Gold(int x, int y, int quanity, int arrayIndex) : base(x, y, '$', arrayIndex)
        {
            goldDrop = random.Next(1, 6);
            Quantity = goldDrop;
        }
    }
}
