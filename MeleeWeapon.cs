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

        public MeleeWeapon(int x, int y, char symbol, int arrayIndex, Types meleeType) : base(x,y,symbol,arrayIndex)
        {
            MeleeWeaponType = meleeWeaponType;

            switch (meleeType)
            {
                case Types.Dagger:
                    base.symbol = 'D';
                    base.Durability = 10;
                    base.Damage = 3;
                    base.Cost = 3;
                    break;
                case Types.LongSword:
                    base.symbol = 'L';
                    base.Durability = 6;
                    base.Damage = 4;
                    base.Cost = 5;
                    break;
            }
        }

        public override string ToString()
        {
            switch (MeleeWeaponType)
            {
                case MeleeWeaponTypesEnum.Dagger:
                    return "Dagger";
                    break;
                case MeleeWeaponTypesEnum.Longsword:
                    return "Longsword";
                    break;
                default: return null; break;
            }
        }

    }
}
