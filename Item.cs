namespace GADE5112POE
{
    public abstract class Item: Tile
    {
        private int quantity;
        private int arrayIndex;

        public int Quantity { get => quantity; set => quantity = value; }
        public int ArrayIndex { get => arrayIndex; set => arrayIndex = value; }

        public Item(int x, int y, char symbol, int arrayIndex) : base(x, y, symbol)
        {
            ArrayIndex = arrayIndex;
        }

        public abstract override string ToString();
    } 
}
