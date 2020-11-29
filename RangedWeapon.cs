using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE5112POE
{
    public class RangedWeapon : Weapon
    {
        public enum RangedWeaponTypes { Rifle,LongBow };

        private RangedWeaponTypes rangedWeaponType;

        public RangedWeaponTypes RangedWeaponType { get => rangedWeaponType; set => rangedWeaponType = value; }

        public override int Range
        {
            get
            {
                switch (RangedWeaponType)
                {
                    case RangedWeaponTypes.LongBow:
                        return 2;
                        break;
                    case RangedWeaponTypes.Rifle:
                        return 3;
                        break;
                    default: return 0;
                        break;
                }
            }
        }

        public RangedWeapon(int x, int y, char symbol, int arrayIndex, RangedWeaponTypes rangedWeaponType) : base(x, y, symbol, arrayIndex)
        {
            switch (rangedWeaponType)
            {
                case RangedWeaponTypes.LongBow:
                    Durability = 4;
                    Range = 2;
                    Damage = 4;
                    Cost = 6;
                    break;
                case RangedWeaponTypes.Rifle:
                    Durability = 3;
                    Range = 3;
                    Damage = 5;
                    Cost = 7;
                    break;
            }
        }

        public override string ToString()
        {
            switch (rangedWeaponType)
            {
                case RangedWeaponTypes.LongBow:
                    return "Longbow";
                    break;
                case RangedWeaponTypes.Rifle:
                    return "Rifle";
                    break;
                default:
                    return null;
                    break;
            }
        }
    }
}
