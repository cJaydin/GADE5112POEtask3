using System;
using System.Text;

namespace GADE5112POE
{
    public abstract class Character : Tile
    {
        protected int hp;
        protected int maxHp;
        protected int damage;
        protected Tile[] vision;
        protected int purse;
        protected Weapon weapon;

        public int Hp { get => hp; set => hp = value; }
        public int MaxHp { get => maxHp; set => maxHp = value; }
        public int Damage { get => damage; set => damage = value; }
        public Tile[] Vision { get => vision; set => vision = value; }
        public Weapon Weapon { get => weapon; set => weapon = value; }
        public int Purse { get => purse; set => purse = value; }

        public enum Movement { Idle, Up, Down, Right, Left }

        public Character(int hp, int damage, int maxHp, int x, int y, char symbol) : base(x, y, symbol)
        {
            this.hp = hp;
            this.damage = damage;
            this.maxHp = maxHp;
        }

        public virtual void Attack(Character target)
        {
            int dmg = weapon == null ? 1 : weapon.Damage;
            string weaponName = weapon?.ToString();

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(GetType().Name + " attacks " + target.GetType().Name + " with ");

            if (weapon==null)
            {
                stringBuilder.Append("bare hands");
            } else
            {
                stringBuilder.Append(weapon.ToString());
            }

            stringBuilder.Append(" (Dmg: " + dmg + ")");

            Program.MainForm.Output(stringBuilder.ToString());

            target.Hp -= dmg;

            Program.MainForm.Output(target.GetType().Name + " " + target.Hp + "/" + target.MaxHp);

            if (target.IsDead())
            {
                Program.MainForm.Output(target.GetType().Name + " has died");
                GetLoot(target);
                Program.Game.MapCreate.ArrMap[target.X, target.Y] = new EmptyTile(target.X, target.Y);
                if (target is Enemy)
                {
                    Program.Game.DeleteEnemyFromEnemyArray((Enemy)target);
                }
                Program.MainForm.DeleteTile(target);

                if (target is Hero)
                {
                    Program.MainForm.ShowEndGame();
                }
            }
        }

        public bool IsDead()
        {
            return hp < 1;
        }

        public virtual Item[] Loot()
        {
            int itemCount = ((purse > 0) ? 1 : 0) + ((Weapon != null) ? 1 : 0);
            Item[] items = new Item[itemCount];
            int i = 0;
            if (purse > 0) { items[i++] = new Gold(X, Y, purse, -1); }
            if (weapon != null) { items[i] = Weapon; }
            return items;
        }

        public virtual void GetLoot(Character target)
        {
            Item[] items = target.Loot();
            foreach (Item item in items)
            {
                if (item.GetType() == typeof(Gold))
                {
                    purse += item.Quantity;
                    Program.MainForm.Output(GetType().Name + " has looted " + item.Quantity + " gold from " + target.GetType().Name);
                }
                else if (Weapon == null && item.GetType().BaseType == typeof(Weapon))
                {
                    Program.MainForm.Output(GetType().Name + " has looted " + item.ToString() + " from " + target.GetType().Name);
                    Weapon = (Weapon)item;
                }
            }
        }

        public virtual bool CheckRange(Character target)
        {
            int range;

            if (weapon != null)
            {
                range = weapon.Range;
            }
            else
            {
                range = 1;
            }

            if (DistanceTo(target) <= range)
            {
                Program.MainForm.Output("Dst from " + GetType().Name + " [" + X + "," + Y + "]" + " to " + target.GetType().Name + "=" + DistanceTo(target) + " (Rng="+range+")");
                return true;
            }
            else
            {
                return false;
            }
        }

        public int DistanceTo(Character target)
        {
            return (int)Math.Sqrt(Math.Abs(X - target.X) * Math.Abs(X - target.X) + Math.Abs(Y - target.Y) * Math.Abs(Y - target.Y));
        }

        public void Move(Movement move)
        {
            int x = X;
            int y = Y;

            switch (move)
            {
                case Movement.Idle: return; break;
                case Movement.Up: Y--; break;
                case Movement.Down: Y++; break;
                case Movement.Right: X++; break;
                case Movement.Left: X--; break;
            }

            if (Program.Game.MapCreate.ArrMap[X, Y] is Item)
            {
                Pickup((Item)Program.Game.MapCreate.ArrMap[X, Y]);
                Program.Game.MapCreate.DeleteItemFromItemArray((Item)Program.Game.MapCreate.ArrMap[X, Y]);
            }

            Program.Game.MapCreate.ArrMap[x, y] = new EmptyTile(x, y);

            if (Program.ShowCoordinates)
            {
                button.Location = new System.Drawing.Point(X * 20 + 40, Y * 20 + 40);
            }
            else
            {
                button.Location = new System.Drawing.Point(X * 20, Y * 20);
            }

            Program.Game.MapCreate.ArrMap[X, Y] = this;
            Program.Game.MapCreate.UpdateVision();
        }

        public abstract Movement ReturnMove(Movement move = 0);

        public abstract void Pickup(Item item);

        public abstract override string ToString();
    }
}
