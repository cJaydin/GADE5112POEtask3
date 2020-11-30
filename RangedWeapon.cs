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

        public RangedWeapon(int x, int y, int arrayIndex, RangedWeaponTypes rangedWeaponType) : base(x, y, arrayIndex)
        {
            switch (rangedWeaponType)
            {
                case RangedWeaponTypes.LongBow:
                    base.Durability = 4;
                    base.Range = 2;
                    Damage = 4;
                    Cost = 6;
                    symbol = 'b';
                    break;
                case RangedWeaponTypes.Rifle:
                    Durability = 3;
                    Range = 3;
                    Damage = 5;
                    Cost = 7;
                    symbol = 'r';
                    break;
            }
        }

        public override string ToString()
        {
            switch (rangedWeaponType)
            {
                case RangedWeaponTypes.LongBow:
                    return "Longbow"; // + base.ToString();
                    break;
                case RangedWeaponTypes.Rifle:
                    return "Rifle"; // + base.ToString();
                    break;
                default:
                    return null;
                    break;
            }
        }
    }
}
