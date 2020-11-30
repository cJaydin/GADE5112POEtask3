using System;

namespace GADE5112POE
{
    public class Map
    {
        private Tile[,] arrMap;
        private Enemy[] arrEnemy;
        private Item[] arrItem;
        private Hero hero;
        private Leader leader;
        private int mapHeight;
        private int mapWidth;
        private Random r = new Random();

        public int MapHeight { get => mapHeight; set => mapHeight = value; }
        public int MapWidth { get => mapWidth; set => mapWidth = value; }
        public Tile[,] ArrMap { get => arrMap; set => arrMap = value; }
        public Enemy[] ArrEnemy { get => arrEnemy; set => arrEnemy = value; }
        public Item[] ArrItem { get => arrItem; set => arrItem = value; }
        public Hero Hero { get => hero; set => hero = value; }
        public Leader Leader { get => leader; set => leader = value; }

        // Constructor when game is loaded
        public Map(int mapWidth, int mapHeight)
        {
            MapWidth = mapWidth;
            MapHeight = mapHeight;

            ArrMap = new Tile[mapWidth, mapHeight];
        }

        public Map(int minWidth, int maxWidth, int minHeight, int maxHeight, int enemyCount, int itemCount)
        {
            Random random = new Random();

            MapHeight = r.Next(minHeight, maxHeight);
            MapWidth = r.Next(minWidth, maxWidth);

            ArrMap = new Tile[MapWidth, MapHeight];
            ArrEnemy = new Enemy[enemyCount];
            ArrItem = new Item[itemCount];

            // First fill map with empty tiles
            for (int i = 0; i < mapWidth; i++)
            {
                for (int j = 0; j < mapHeight; j++)
                {
                    ArrMap[i, j] = new EmptyTile(i, j);
                }
            }

            // Walls
            for (int i = 0; i < mapWidth; i++)
            {
                Tile topTile = new Wall(i, 0);
                ArrMap[i, 0] = topTile;
                Tile bottomTile = new Wall(i, mapHeight - 1);
                ArrMap[i, mapHeight - 1] = bottomTile;
            }

            for (int i = 0; i < MapHeight; i++)
            {
                Tile leftTile = new Wall(0, i);
                ArrMap[0, i] = leftTile;
                Tile rightTile = new Wall(mapWidth - 1, i);
                ArrMap[mapWidth - 1, i] = rightTile;
            }

            Hero = (Hero)Create(Tile.TileType.Hero, -1);
            ArrMap[Hero.X, Hero.Y] = Hero;

            for (int i = 0; i < enemyCount; i++)
            {
                arrEnemy[i] = (Enemy)Create(Tile.TileType.Enemy, i);
                //int weaponChance = random.Next(1, 4);
                //if (weaponChance == 3)
                //{
                //    arrEnemy[i].Weapon = (Weapon)Create(Tile.TileType.Weapon, i);
                //}

                arrMap[arrEnemy[i].X, arrEnemy[i].Y] = arrEnemy[i];
            }

            for (int i = 0; i < itemCount; i++)
            {
                int chance = random.Next(0, 3);

                if (chance == 0 || chance == 1)
                {
                    Gold gold = (Gold)Create(Tile.TileType.Gold, i);
                    ArrItem[i] = gold;
                    arrMap[gold.X, gold.Y] = gold;
                } else if (chance==2)
                {
                    Weapon weapon = (Weapon)Create(Tile.TileType.Weapon, i);
                    ArrItem[i] = weapon;
                    arrMap[weapon.X, weapon.Y] = weapon;
                }
            }

            Leader = (Leader)Create(Tile.TileType.Leader, -1);
            Leader.Target = Hero;
            ArrMap[Leader.X, Leader.Y] = Leader;

            //new frmMapDebug(this).Show();
            UpdateVision();
        }

        bool MapContainsCharacterAt(int x, int y)
        {
            if (Hero != null && Hero.X == x && Hero.Y == y) { return true; }
            if (Leader != null && Leader.X == x && Leader.Y == y) { return true; }

            for (int i = 0; i < arrEnemy.Length; i++)
            {
                Tile t = arrEnemy[i];
                if (t == null) { return false; }
                if (t.X == x && t.Y == y) { return true; }
            }

            return false;
        }

        private Weapon CreateWeapon(int x, int y, int arrayIndex)
        {
            Random random = new Random();
            int weaponType = random.Next(0, 2);

            if (weaponType == (int)Weapon.WeaponTypeEnum.Melee)
            {
                int meleeType = random.Next(0, 2);
                if (meleeType == (int)MeleeWeapon.Types.Dagger)
                {
                    return new MeleeWeapon(x, y, arrayIndex, MeleeWeapon.Types.Dagger);
                }
                else if (meleeType == (int)MeleeWeapon.Types.LongSword)
                {
                    return new MeleeWeapon(x, y, arrayIndex, MeleeWeapon.Types.LongSword);
                }
            }
            else if (weaponType == (int)Weapon.WeaponTypeEnum.Ranged)
            {
                int rangedType = random.Next(0, 2);
                if (rangedType == (int)RangedWeapon.RangedWeaponTypes.LongBow)
                {
                    return new RangedWeapon(x, y, arrayIndex, RangedWeapon.RangedWeaponTypes.LongBow);
                }
                else if (rangedType == (int)RangedWeapon.RangedWeaponTypes.Rifle)
                {
                    return new RangedWeapon(x, y, arrayIndex, RangedWeapon.RangedWeaponTypes.Rifle);
                }
            }
            return null;
        }

        public Tile Create(Tile.TileType type, int arrayIndex)
        {
            Random random = new Random();

            int x = 0, y = 0;
            do
            {
                x = random.Next(1, mapWidth - 2);
                y = random.Next(1, mapHeight - 2);
            } while (MapContainsCharacterAt(x, y));

            if (type == Tile.TileType.Hero)
            {
                return new Hero(x, y, 100);
            }
            else if (type == Tile.TileType.Enemy)
            {
                int chance = random.Next(1, 7);

                if (chance == 1)
                {
                    return new Mage(x, y, arrayIndex);
                }
                else
                {
                    return new Goblin(x, y, arrayIndex);
                }
            }
            else if (type == Tile.TileType.Gold)
            {
                return new Gold(x, y, random.Next(1, 10), arrayIndex);
            }
            else if (type == Tile.TileType.Leader)
            {
                return new Leader(x, y);
            }
            else if (type == Tile.TileType.Weapon)
            {
                return CreateWeapon(x, y, arrayIndex);
            }

            return null;
        }

        internal void MoveLeader()
        {
            Character.Movement movement = Leader.ReturnMove();
            arrMap[Leader.X, Leader.Y] = new EmptyTile(Leader.X, Leader.Y);
            switch (movement)
            {
                case Character.Movement.Down:
                    Leader.Y++;
                    break;
                case Character.Movement.Up:
                    Leader.Y--;
                    break;
                case Character.Movement.Left:
                    Leader.X--;
                    break;
                case Character.Movement.Right:
                    Leader.X++;
                    break;
            }

            ArrMap[Leader.X, Leader.Y] = Leader;

            if (Program.ShowCoordinates)
            {
                Leader.Button.Location = new System.Drawing.Point(Leader.X * 20+40, Leader.Y * 20+40);
            } else
            {
                Leader.Button.Location = new System.Drawing.Point(Leader.X * 20, Leader.Y * 20);
            }
        }

        internal void MoveEnemies()
        {
            //Random random = new Random();
            //for (int i = 0; i < arrEnemy.Length; i++)
            //{
            //    if (arrEnemy[i].GetType() == typeof(Mage)) { continue; }

            //    // Allow for less movement on screen
            //    int idleChance = random.Next(0, 3);
            //    if (idleChance==0) { continue; }

            //    Character.Movement direction = (Character.Movement)random.Next(0, 5);
            //    if (direction != Character.Movement.Idle)
            //    {
            //        Enemy enemy = arrEnemy[i];
            //        arrMap[enemy.X, enemy.Y] = new EmptyTile(enemy.X, enemy.Y);
            //        switch (direction)
            //        {
            //            case Character.Movement.Up:
            //                if (enemy.Y > 1 && enemy.Vision[(int)Character.Movement.Up].GetType() != typeof(Hero))
            //                {
            //                    enemy.Y--;
            //                }
            //                break;
            //            case Character.Movement.Down:
            //                if (enemy.Y < (MapHeight - 2) && enemy.Vision[(int)Character.Movement.Down].GetType() != typeof(Hero))
            //                {
            //                    enemy.Y++;
            //                }
            //                break;
            //            case Character.Movement.Left:
            //                if (enemy.X > 2 && enemy.Vision[(int)Character.Movement.Left].GetType() != typeof(Hero))
            //                {
            //                    enemy.X--;
            //                }
            //                break;
            //            case Character.Movement.Right:
            //                if (enemy.X < (mapWidth - 2) && enemy.Vision[(int)Character.Movement.Right].GetType() != typeof(Hero))
            //                {
            //                    enemy.X++;
            //                }
            //                break;
            //        }

            //        if (Program.ShowCoordinates)
            //        {
            //            enemy.Button.Location = new System.Drawing.Point(enemy.X * 20 + 40, enemy.Y * 20 + 40);
            //        } else
            //        {
            //            enemy.Button.Location = new System.Drawing.Point(enemy.X * 20, enemy.Y * 20);
            //        }
            //        arrMap[enemy.X, enemy.Y] = enemy;
            //    }
            //}
        }

        public void UpdateVision()
        {
            Hero.Vision = new Tile[5];

            Hero.Vision[(int)Hero.Movement.Idle] = null;
            Hero.Vision[(int)Hero.Movement.Up] = arrMap[Hero.X, Hero.Y - 1];
            Hero.Vision[(int)Hero.Movement.Down] = arrMap[Hero.X, Hero.Y + 1];
            Hero.Vision[(int)Hero.Movement.Right] = arrMap[Hero.X + 1, Hero.Y];
            Hero.Vision[(int)Hero.Movement.Left] = arrMap[Hero.X - 1, Hero.Y];

            Leader.Vision = new Tile[5];

            Leader.Vision[(int)Leader.Movement.Idle] = null;
            Leader.Vision[(int)Leader.Movement.Up] = arrMap[Leader.X, Leader.Y - 1];
            Leader.Vision[(int)Leader.Movement.Down] = arrMap[Leader.X, Leader.Y + 1];
            Leader.Vision[(int)Leader.Movement.Left] = arrMap[Leader.X - 1, Leader.Y];
            Leader.Vision[(int)Leader.Movement.Right] = arrMap[Leader.X + 1, Leader.Y];

            for (int i = 0; i < arrEnemy.Length; i++)
            {
                arrEnemy[i].Vision = new Tile[5];

                arrEnemy[i].Vision[(int)Hero.Movement.Idle] = null;
                arrEnemy[i].Vision[(int)Hero.Movement.Up] = ArrMap[arrEnemy[i].X, arrEnemy[i].Y - 1];
                arrEnemy[i].Vision[(int)Hero.Movement.Down] = ArrMap[arrEnemy[i].X, arrEnemy[i].Y + 1];
                arrEnemy[i].Vision[(int)Hero.Movement.Left] = ArrMap[arrEnemy[i].X - 1, arrEnemy[i].Y];
                arrEnemy[i].Vision[(int)Hero.Movement.Right] = ArrMap[arrEnemy[i].X + 1, arrEnemy[i].Y];
            }

        }

        public void DeleteItemFromItemArray(Item item)
        {
            Item[] items = new Item[ArrItem.Length - 1];
            int i = 0;
            foreach (Item itm in ArrItem)
            {
                if (itm.ArrayIndex != item.ArrayIndex)
                {
                    items[i++] = itm;
                }
            }
            ArrItem = items;
        }

        public Item GetItemAtPosition(int x, int y)
        {
            foreach (Item item in ArrItem)
            {
                if (item.X == x && item.Y == y)
                {
                    DeleteItemFromItemArray(item);
                    return item;
                }
            }

            return null;
        }
    }
}
