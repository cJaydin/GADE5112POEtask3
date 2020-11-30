using System;

namespace GADE5112POE
{
    public class Shop
    {
        private Weapon[] weapons;
        private Random random;
        private Hero hero;

        public Shop(Hero buyer)
        {
            hero = buyer;

            random = new Random();

            Weapon.WeaponTypeEnum weaponTypeEnum = Weapon.WeaponTypeEnum.Melee;

            weapons = new Weapon[3];

            for (int i = 0; i < 3; i++)
            {
                weapons[i] = RandomWeapon();
            }
        }

        private Weapon RandomWeapon()
        {
            Weapon weapon = null;

            Weapon.WeaponTypeEnum weaponType = Weapon.WeaponTypeEnum.Melee;

            switch (random.Next(0, 2))
            {
                case 0:
                    weaponType = (Weapon.WeaponTypeEnum)0; break;
                case 1:
                    weaponType = (Weapon.WeaponTypeEnum)1; break;
            }

            if (weaponType == Weapon.WeaponTypeEnum.Melee)
            {
                switch (random.Next(0, 1))
                {
                    case 0: weapon = new MeleeWeapon(-1, -1, -1, MeleeWeapon.Types.Dagger); break;
                    case 1: weapon = new MeleeWeapon(-1, -1, -1, MeleeWeapon.Types.LongSword); break;
                }
            } else if (weaponType == Weapon.WeaponTypeEnum.Ranged)
            {
                switch (random.Next(0, 2))
                {
                    case 0: weapon = new RangedWeapon(-1, -1, -1, RangedWeapon.RangedWeaponTypes.Rifle); break;
                    case 1: weapon = new RangedWeapon(-1, -1, -1, RangedWeapon.RangedWeaponTypes.LongBow); break;
                }
            }

            return weapon;
        }

        public bool CanBuy(int i)
        {
            if (hero.Purse >= weapons[i].Cost)
            {
                return true;
            } else
            {
                return false;
            }
        }

        public void Buy(int num)
        {
            hero.Purse -= weapons[num].Cost;
            hero.Pickup(weapons[num]);
            weapons[num] = RandomWeapon();
        }

        public String DisplayWeapon(int num)
        {
            return weapons[num].ToString();
        }
    }
}
