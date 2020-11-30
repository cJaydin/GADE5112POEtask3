namespace GADE5112POE
{
    public abstract class Weapon : Item
    {
        public enum WeaponTypeEnum { Melee,Ranged };

        protected int damage;
        protected int range;
        protected int durability;
        protected int cost;
        protected WeaponTypeEnum weaponType;

        public int Damage { get => damage; set => damage = value; }
        virtual public int Range { get => range; set => range = value; }
        public int Durability { get => durability; set => durability = value; }
        public int Cost { get => cost; set => cost = value; }
        public WeaponTypeEnum WeaponType { get => weaponType; set => weaponType = value; } // NB: Replace with enum

        public Weapon(int x, int y, int arrayIndex) : base(x, y, '.', arrayIndex) { }

        public override string ToString()
        {
            return "Damage: " + damage + " Range: " + range + " Durability: " + durability + " Cost: " + cost;
        }
    }
}
