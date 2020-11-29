using System;

namespace GADE5112POE
{
    public class Map
    {
        private Tile[,] arrMap;
        private Enemy[] arrEnemy;
        private Item[] arrItem;
        private Hero hero;
        private int mapHeight;
        private int mapWidth;
        private Random r = new Random();

        public int MapHeight { get => mapHeight; set => mapHeight = value; }
        public int MapWidth { get => mapWidth; set => mapWidth = value; }
        public Tile[,] ArrMap { get => arrMap; set => arrMap = value; }
        public Enemy[] ArrEnemy { get => arrEnemy; set => arrEnemy = value; }
        public Item[] ArrItem { get => arrItem; set => arrItem = value; }
        public Hero Hero { get => hero; set => hero = value; }

        // Constructor when game is loaded
        public Map(int mapWidth, int mapHeight)
        {
            MapWidth = mapWidth;
            MapHeight = mapHeight;

            ArrMap = new Tile[mapWidth, mapHeight];
        }

        public Map(int minWidth, int maxWidth, int minHeight, int maxHeight, int enemyCount, int itemCount)
        {
            MapHeight = r.Next(minHeight, maxHeight);
            MapWidth = r.Next(minWidth, maxWidth);

            ArrMap = new Tile[MapWidth, MapHeight];
            ArrEnemy = new Enemy[enemyCount];
            ArrItem = new Item[itemCount];

            // First fill map with empty tiles
            for (int i=0; i < mapWidth; i++)
            {
                for (int j=0; j < mapHeight; j++)
                {
                    ArrMap[i, j] = new EmptyTile(i, j);
                }
            }

            // Walls
            for (int i=0; i < mapWidth; i++)
            {
                Tile topTile = new Wall(i, 0);
                ArrMap[i, 0] = topTile;
                Tile bottomTile = new Wall(i, mapHeight - 1);
                ArrMap[i, mapHeight - 1] = bottomTile;
            }

            for (int i=0; i < MapHeight; i++)
            {
                Tile leftTile = new Wall(0, i);
                ArrMap[0, i] = leftTile;
                Tile rightTile = new Wall(mapWidth-1, i);
                ArrMap[mapWidth - 1, i] = rightTile;
            }

            Hero = (Hero)Create(Tile.TileType.Hero,-1);
            ArrMap[Hero.X, Hero.Y] = Hero;

            for (int i = 0; i < enemyCount; i++)
            {
                arrEnemy[i] = (Enemy)Create(Tile.TileType.Enemy,i);

                arrMap[arrEnemy[i].X, arrEnemy[i].Y] = arrEnemy[i];
            }

            for (int i = 0; i < itemCount; i++)
            {
                Gold gold = (Gold)Create(Tile.TileType.Gold, i);
                ArrItem[i] = gold;
                arrMap[gold.X, gold.Y] = gold;
            }

            new frmMapDebug(this).Show();
            UpdateVision();
        }

        bool MapContainsCharacterAt(int x, int y)
        {
            if (Hero.X==x && Hero.Y==y) { return true; }

            for (int i=0; i < arrEnemy.Length; i++)
            {
                Tile t = arrEnemy[i];
                if (t==null) { return false; }
                if (t.X==x && t.Y==y) { return true; }
            }

            return false;
        }

        private Tile Create(Tile.TileType type, int arrayIndex)
        {
            Random random = new Random();

            if (Hero==null)
            {
                Hero = new Hero(0, 0, 10); // Dummy Hero for MapContainsCharacterAt to work
            }

            int x = 0, y = 0;
            do
            {
                x = random.Next(1, mapWidth - 2);
                y = random.Next(1, mapHeight - 2);
            } while (MapContainsCharacterAt(x, y));

            //if (x >= mapWidth-2) { x -= 2; }
            //if (y >= mapHeight-2) { y-=2; }

            if (type == Tile.TileType.Hero)
            {
                return new Hero(x, y, 10);
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
            } else if (type == Tile.TileType.Gold)
            {
                return new Gold(x, y, random.Next(1,10),arrayIndex);
            }

            return null;
        }

        internal void MoveEnemies()
        {
            Random random = new Random();
            for (int i=0; i < arrEnemy.Length; i++)
            {
                if (arrEnemy[i].GetType()==typeof(Mage)) { continue; }

                Character.Movement direction = (Character.Movement)random.Next(0,5);
                if (direction!=Character.Movement.Idle)
                {
                    Enemy enemy = arrEnemy[i];
                    arrMap[enemy.X, enemy.Y] = new EmptyTile(enemy.X,enemy.Y);
                    switch (direction)
                    {
                        case Character.Movement.Up:
                            if (enemy.Y > 1 && enemy.Vision[(int)Character.Movement.Up].GetType()!=typeof(Hero))
                            {
                                enemy.Y--;
                            }
                            break;
                        case Character.Movement.Down:
                            if (enemy.Y < (MapHeight-2) && enemy.Vision[(int)Character.Movement.Down].GetType() != typeof(Hero))
                            {
                                enemy.Y++;
                            }
                            break;
                        case Character.Movement.Left:
                            if (enemy.X > 2 && enemy.Vision[(int)Character.Movement.Left].GetType() != typeof(Hero))
                            {
                                enemy.X--;
                            }
                            break;
                        case Character.Movement.Right:
                            if (enemy.X < (mapWidth-2) && enemy.Vision[(int)Character.Movement.Right].GetType() != typeof(Hero))
                            {
                                enemy.X++;
                            }
                            break;
                    }
                    enemy.Button.Location = new System.Drawing.Point(enemy.X * 20, enemy.Y * 20);
                    arrMap[enemy.X, enemy.Y] = enemy;
                }
            }
        }

        public void UpdateVision()
        {
            Hero.Vision = new Tile[5];

            Hero.Vision[(int)Hero.Movement.Idle] = null;
            Hero.Vision[(int)Hero.Movement.Up] = arrMap[Hero.X, Hero.Y - 1];
            Hero.Vision[(int)Hero.Movement.Down] = arrMap[Hero.X, Hero.Y + 1];
            Hero.Vision[(int)Hero.Movement.Left] = arrMap[Hero.X-1, Hero.Y];
            Hero.Vision[(int)Hero.Movement.Right] = arrMap[Hero.X+1, Hero.Y];

            for (int i = 0; i < arrEnemy.Length; i++)
            {
                arrEnemy[i].Vision = new Tile[5];

                arrEnemy[i].Vision[(int)Hero.Movement.Idle] = null;
                arrEnemy[i].Vision[(int)Hero.Movement.Up] = ArrMap[arrEnemy[i].X, arrEnemy[i].Y - 1];
                arrEnemy[i].Vision[(int)Hero.Movement.Down] = ArrMap[arrEnemy[i].X, arrEnemy[i].Y + 1];
                arrEnemy[i].Vision[(int)Hero.Movement.Left] = ArrMap[arrEnemy[i].X-1, arrEnemy[i].Y];
                arrEnemy[i].Vision[(int)Hero.Movement.Right] = ArrMap[arrEnemy[i].X+1, arrEnemy[i].Y];
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
                if (item.X==x && item.Y==y)
                {
                    DeleteItemFromItemArray(item);
                    return item;
                } 
            }

            return null;
        }
    }
}
