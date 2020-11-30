namespace GADE5112POE
{
    public class MeleeWeapon : Weapon
    {
        public enum Types
        {
            Dagger, LongSword
        }

        public enum MeleeWeaponTypesEnum { Dagger, Longsword };

        private MeleeWeaponTypesEnum meleeWeaponType;

        public MeleeWeaponTypesEnum MeleeWeaponType { get => meleeWeaponType; set => meleeWeaponType = value; }

        public override int Range { get => 1; }

        public MeleeWeapon(int x, int y, int arrayIndex, Types meleeType) : base(x,y,arrayIndex)
        {
            MeleeWeaponType = meleeWeaponType;

            switch (meleeType)
            {
                case Types.Dagger:
                    base.Durability = 10;
                    base.Damage = 3;
                    base.Cost = 3;
                    symbol = 'd';
                    break;
                case Types.LongSword:
                    base.Durability = 6;
                    base.Damage = 4;
                    base.Cost = 5;
                    base.symbol = 'l';
                    break;
            }
        }

        public override string ToString()
        {
            switch (MeleeWeaponType)
            {
                case MeleeWeaponTypesEnum.Dagger:
                    return "Dagger"; // + base.ToString();
                    break;
                case MeleeWeaponTypesEnum.Longsword:
                    return "Longsword"; // + base.ToString();
                    break;
                default: return null; break;
            }
        }

    }
}
