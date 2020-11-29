namespace GADE5112POE
{
    public abstract class Weapon : Item
    {
        protected int damage;
        protected int range;
        protected int durability;
        protected int cost;
        protected int weaponType; // NB: Change type to enum

        public int Damage { get => damage; set => damage = value; }
        public int Range { get => range; set => range = value; }
        public int Durability { get => durability; set => durability = value; }
        public int Cost { get => cost; set => cost = value; }
        public int WeaponType { get => weaponType; set => weaponType = value; } // NB: Replace with enum

        public Weapon(int x, int y, char symbol, int arrayIndex) : base(x, y, symbol, arrayIndex) { }
    }
}
