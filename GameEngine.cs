using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace GADE5112POE
{
    class GameEngine
    {
        private Map map;
        private frmMain mainForm;

        public Map MapCreate { get => map; set => map = value; }
        public frmMain MainForm { get => mainForm; set => mainForm = value; }

        // Constructor when loading new game
        public GameEngine() { }

        public GameEngine(frmMain form, int minWidth, int maxWidth, int minHeight, int maxHeight, int enemyCount, int itemCount)
        {
            mainForm = form;
            MapCreate = new Map(minWidth, maxWidth, minHeight, maxHeight, enemyCount, itemCount);
        }

        public bool Save()
        {
            BinaryWriter bw;

            try
            {
                bw = new BinaryWriter(new FileStream("game.save", FileMode.Create));

                bw.Write(MapCreate.MapWidth);
                bw.Write(MapCreate.MapHeight);

                for (int i = 0; i < MapCreate.MapWidth; i++)
                {
                    for (int j = 0; j < MapCreate.MapHeight; j++)
                    {
                        Tile t = MapCreate.ArrMap[i, j];
                        bw.Write(t.GetType().Name);
                        bw.Write(t.X);
                        bw.Write(t.Y);

                        switch (t.GetType().Name)
                        {
                            case "Wall": break;
                            case "EmptyTile": break;
                            case "Hero":
                                bw.Write(((Hero)t).Hp);
                                bw.Write(((Hero)t).MaxHp);
                                bw.Write(((Hero)t).Damage);
                                bw.Write(((Hero)t).Purse);
                                break;
                            case "Goblin":
                                bw.Write(((Goblin)t).Hp);
                                bw.Write(((Goblin)t).MaxHp);
                                bw.Write(((Goblin)t).Damage);
                                bw.Write(((Goblin)t).Purse);
                                bw.Write(((Goblin)t).ArrayIndex);
                                break;
                            case "Mage":
                                bw.Write(((Mage)t).Hp);
                                bw.Write(((Mage)t).MaxHp);
                                bw.Write(((Mage)t).Damage);
                                bw.Write(((Mage)t).Purse);
                                bw.Write(((Mage)t).ArrayIndex);
                                break;
                            case "Gold":
                                bw.Write(((Gold)t).Quantity);
                                bw.Write(((Gold)t).ArrayIndex);
                                break;
                        }
                    }
                }
                bw.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed creating file for saving. Exception = " + ex.Message);
                return false;
            }

            return true;
        }

        public bool Load()
        {
            try
            {
                BinaryReader br;
                br = new BinaryReader(new FileStream("game.save", FileMode.Open));

                int mapWidth = br.ReadInt32();
                int mapHeight = br.ReadInt32();

                Map m = new Map(mapWidth,mapHeight);

                List<Enemy> lstEnemy = new List<Enemy>();
                List<Item> lstItem = new List<Item>();
 
                for (int i=0; i < mapWidth; i++)
                {
                    for (int j=0; j < mapHeight; j++)
                    {
                        string tileType = br.ReadString();
                        int x = br.ReadInt32();
                        int y = br.ReadInt32();

                        switch (tileType)
                        {
                            case "Wall":
                                Wall w = new Wall(x, y);
                                m.ArrMap[i, j] = w;
                                break;
                            case "EmptyTile":
                                EmptyTile e = new EmptyTile(x, y);
                                m.ArrMap[i, j] = e;
                                break;
                            case "Hero":
                                int hp = br.ReadInt32();
                                int maxHp = br.ReadInt32();
                                int damage = br.ReadInt32();
                                int purse = br.ReadInt32();
                                Hero h = new Hero(x, y, hp);
                                h.Damage = damage;
                                h.Purse = purse;
                                m.ArrMap[i, j] = h;
                                m.Hero = h;
                                break;
                            case "Goblin":
                                int hpG = br.ReadInt32();
                                int maxHpG = br.ReadInt32();
                                int damageG = br.ReadInt32();
                                int purseG = br.ReadInt32();
                                int arrayIndexG = br.ReadInt32();
                                Goblin g = new Goblin(x, y, arrayIndexG);
                                g.Damage = damageG;
                                g.Purse = purseG;
                                m.ArrMap[i, j] = g;
                                lstEnemy.Add(g);
                                break;
                            case "Mage":
                                int hpM = br.ReadInt32();
                                int maxHpM = br.ReadInt32();
                                int damageM = br.ReadInt32();
                                int purseM = br.ReadInt32();
                                int arrayIndexM = br.ReadInt32();
                                Mage mg = new Mage(x, y, arrayIndexM);
                                mg.Damage = damageM;
                                mg.Purse = purseM;
                                m.ArrMap[i, j] = mg;
                                lstEnemy.Add(mg);
                                break;
                            case "Gold":
                                int quantity = br.ReadInt32();
                                int arrayIndexGld = br.ReadInt32();
                                Gold gld = new Gold(x, y, quantity, arrayIndexGld);
                                m.ArrMap[i, j] = gld;
                                lstItem.Add(gld);
                                break;
                        }
                    }
                }
                m.ArrEnemy = lstEnemy.ToArray();
                m.ArrItem = lstItem.ToArray();
                map = m;
                br.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed loading game. Exception = " + ex.Message);
            }
            return true;
        }

        private void DeleteEnemyFromEnemyArray(Enemy enemy)
        {
            Enemy[] enemies = new Enemy[MapCreate.ArrEnemy.Length - 1];
            int i = 0;
            foreach (Enemy enm in MapCreate.ArrEnemy)
            {
                if (enm.ArrayIndex != enemy.ArrayIndex)
                {
                    enemies[i++] = enm;
                }
            }
            MapCreate.ArrEnemy = enemies;
        }



        public bool MovePlayer(Character.Movement move)
        {
            int x = MapCreate.Hero.X;
            int y = MapCreate.Hero.Y;
            MapCreate.ArrMap[x, y] = new EmptyTile(x, y);

            move = MapCreate.Hero.ReturnMove(move);

            switch (move)
            {
                case Character.Movement.Idle:
                    break;
                case Character.Movement.Up:
                    //if (MapCreate.Hero.Vision[(int)Hero.Movement.Up].GetType() != typeof(EmptyTile)) break;
                    if (MapCreate.ArrMap[x, y - 1].GetType().BaseType == typeof(Item))
                    {
                        if (MapCreate.ArrMap[x, y - 1].GetType() == typeof(Gold))
                        {
                            MapCreate.Hero.Pickup((Item)MapCreate.ArrMap[x, y - 1], mainForm);
                            MapCreate.DeleteItemFromItemArray((Item)MapCreate.ArrMap[x, y - 1]);
                        }
                        else
                        {
                            ////Item item = (Item)MapCreate.ArrMap[x, y - 1];
                            //if (DialogResult.OK == new dialoguePickupItem(item).Show())
                            //{
                            //    Item item = MapCreate.GetItemAtPosition(x, y - 1);
                            //    //Hero.Weapon = item;
                            //}
                        }
                    }
                    MapCreate.Hero.Button.Location = new System.Drawing.Point(x * 20, (y - 1) * 20);
                    MapCreate.Hero.X = x;
                    MapCreate.Hero.Y = y - 1;
                    MapCreate.ArrMap[x, y - 1] = MapCreate.Hero;
                    break;
                case Character.Movement.Down:
                    //if (MapCreate.Hero.Vision[(int)Hero.Movement.Down].GetType() != typeof(EmptyTile)) break;
                    if (MapCreate.ArrMap[x, y + 1].GetType().BaseType == typeof(Item))
                    {
                        Item item = (Item)MapCreate.ArrMap[x, y + 1];
                        MapCreate.Hero.Pickup((Item)MapCreate.ArrMap[x, y + 1], mainForm);
                        MapCreate.DeleteItemFromItemArray(item);
                    }
                    MapCreate.Hero.Button.Location = new System.Drawing.Point(x * 20, (y + 1) * 20);
                    MapCreate.Hero.X = x;
                    MapCreate.Hero.Y = y + 1;
                    MapCreate.ArrMap[x, y + 1] = MapCreate.Hero;
                    break;
                case Character.Movement.Right:
                    //if (MapCreate.Hero.Vision[(int)Hero.Movement.Right].GetType() != typeof(EmptyTile)) break;
                    if (MapCreate.ArrMap[x + 1, y].GetType().BaseType == typeof(Item))
                    {
                        Item item = (Item)MapCreate.ArrMap[x + 1, y];
                        MapCreate.Hero.Pickup((Item)MapCreate.ArrMap[x + 1, y], mainForm);
                        MapCreate.DeleteItemFromItemArray(item);
                    }
                    MapCreate.Hero.Button.Location = new System.Drawing.Point((x + 1) * 20, y * 20);
                    MapCreate.Hero.X = x + 1;
                    MapCreate.Hero.Y = y;
                    MapCreate.ArrMap[x + 1, y] = MapCreate.Hero;
                    break;
                case Character.Movement.Left:
                    //if (MapCreate.Hero.Vision[(int)Hero.Movement.Left].GetType() != typeof(EmptyTile)) break;
                    if (MapCreate.ArrMap[x - 1, y].GetType().BaseType == typeof(Item))
                    {
                        Item item = (Item)MapCreate.ArrMap[x - 1, y];
                        MapCreate.Hero.Pickup((Item)MapCreate.ArrMap[x - 1, y], mainForm);
                        MapCreate.DeleteItemFromItemArray(item);
                    }
                    MapCreate.Hero.Button.Location = new System.Drawing.Point((x - 1) * 20, y * 20);
                    MapCreate.Hero.X = x - 1;
                    MapCreate.Hero.Y = y;
                    MapCreate.ArrMap[x - 1, y] = MapCreate.Hero;
                    break;
                default:
                    break;
            }

            MapCreate.UpdateVision();
            MapCreate.MoveEnemies();
            MapCreate.UpdateVision();
            MageAttacks();
            MapCreate.UpdateVision();
            mainForm.Refresh();

            return false;
        }

        private void MageAttacks()
        {
            foreach (Enemy enemy in MapCreate.ArrEnemy)
            {
                if (enemy.GetType() == typeof(Mage))
                {
                    MageAttack(enemy);
                }
            }
        }

        private void MageAttack(Enemy mage)
        {
            foreach (Enemy goblin in MapCreate.ArrEnemy)
            {
                if (goblin.GetType() == typeof(Goblin))
                {
                    if (mage.CheckRange(goblin))
                    {
                        // Attacks goblin, return true if goblin died
                        if (mage.Attack(goblin, mainForm))
                        {
                            MapCreate.ArrMap[goblin.X, goblin.Y] = new EmptyTile(goblin.X, goblin.Y);
                            DeleteEnemyFromEnemyArray(goblin);
                            mainForm.DeleteTile(goblin);
                        }
                    }
                }
            }
        }

        public void Attack(Enemy enemy)
        {
            mainForm.Output("Hero attacks " + enemy.GetType().Name + " [" + enemy.X + "," + enemy.Y + "]");

            enemy.Hp -= MapCreate.Hero.Damage;

            mainForm.Output("Enemy: " + enemy.Hp + "/" + enemy.MaxHp + " [" + enemy.X + "," + enemy.Y + "]");

            if (enemy.Hp < 1)
            {
                mainForm.Output(enemy.GetType().Name + " has died");
                MapCreate.ArrMap[enemy.X, enemy.Y] = new EmptyTile(enemy.X, enemy.Y);
                DeleteEnemyFromEnemyArray(enemy);
                mainForm.DeleteTile(enemy);
                //frmMain.
            }
        }

        internal void AttackBack(Enemy enemy)
        {
            mainForm.Output(enemy.GetType().Name + " attacks Hero");
            MapCreate.Hero.Hp -= enemy.Damage;
            //Hero h = MapCreate.Hero;
            //mainForm.Output("Hero: " + h.Hp + "/" + h.MaxHp + " [" + h.X + "," + h.Y + "]");
            mainForm.Output(MapCreate.Hero.ToString());

            if (MapCreate.Hero.Hp < 1)
            {
                mainForm.Output("Your hero has died");
                mainForm.ShowEndGame();
            }
        }
    }
}
