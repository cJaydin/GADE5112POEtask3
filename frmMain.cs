using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GADE5112POE
{
    public partial class frmMain : Form, IView
    {
        //private GameEngine Game;
        private Queue<string> MessageBuffer = new Queue<string>(35);

        public Button ButtonWeapon1
        {
            get => btnWeapon1;
        }

        public Button ButtonWeapon2
        {
            get => btnWeapon2;
        }

        public Button ButtonWeapon3
        {
            get => btnWeapon3;
        }

        public frmMain()
        {
            Program.ShowCoordinates = false; // Set this to true for grid/map coordinates, useful for debugging.
            Program.MainForm = this;

            InitializeComponent();

            dialogueNewOrLoadGame dlg = new dialogueNewOrLoadGame();
            dlg.ShowDialog();
            if (dlg.DialogResult == DialogResult.OK)
            {
                NewGame();
            }
            else
            {
                Program.Game = new GameEngine();
                Program.Game.Load();
                Program.Game.MapCreate.UpdateVision();
                RenderMap(Program.Game.MapCreate);
            }
        }

        private void NewGame()
        {
            Random random = new Random();
            int itemCount = random.Next(8, 15);

            Program.Game = new GameEngine(20, 43, 20, 30, 10, itemCount);
            RenderMap(Program.Game.MapCreate);
            Application.DoEvents();
            this.Refresh();
            this.Focus();
        }

        public void Output(string text)
        {
            if (MessageBuffer.Count > 34)
            {
                MessageBuffer.Dequeue();
                MessageBuffer.Enqueue(text);

                txtOuput.Text = String.Empty;
                foreach (string s in MessageBuffer.ToArray())
                {
                    txtOuput.Text += s + "\r\n";
                }
            }
            else
            {
                MessageBuffer.Enqueue(text);
                txtOuput.Text += text + "\r\n";
            }
        }

        public void DeleteTile(Tile t)
        {
            this.Controls.Remove(t.Button);
        }

        private void RenderMap(Map map)
        {
            if (Program.ShowCoordinates)
            {
                for (int i = 0; i < map.MapWidth; i++)
                {
                    Button button = new Button();
                    button.Text = i.ToString();
                    button.Width = 20;
                    button.Height = 20;
                    button.Click += ((object sender, EventArgs e) => MessageBox.Show(button.Text));
                    button.Location = new Point(i * 20 + 40, 20);
                    this.Controls.Add(button);
                }

                for (int i = 0; i < map.MapHeight; i++)
                {
                    Button button = new Button();
                    button.Text = i.ToString();
                    button.Width = 40;
                    button.Height = 20;
                    button.Location = new Point(0, i * 20 + 40);
                    this.Controls.Add(button);
                }
            }

            for (int i = 0; i < map.MapWidth; i++)
            {
                for (int j = 0; j < map.MapHeight; j++)
                {
                    Tile tile = map.ArrMap[i, j];

                    if (tile == null || tile.GetType() == typeof(EmptyTile)) { continue; }

                    Button button = new Button();

                    if (tile is Gold)
                    {
                        button.BackColor = Color.FromName("Yellow");
                    }
                    else if (tile is Hero)
                    {
                        button.BackColor = Color.FromName("LightGreen");
                    }
                    else if (tile is Mage)
                    {
                        button.BackColor = Color.FromName("Red");
                    }
                    else if (tile is Goblin)
                    {
                        button.BackColor = Color.FromName("Bisque");
                    }
                    else if (tile is Leader)
                    {
                        button.BackColor = Color.FromName("Cyan");
                    }
                    else if (tile is Weapon)
                    {
                        button.BackColor = Color.FromName("HotPink");
                    }

                    button.Tag = tile;
                    button.Click += CharacterButton_Click;
                    button.Text = tile.Symbol.ToString();
                    button.Width = 20;
                    button.Height = 20;

                    if (Program.ShowCoordinates)
                    {
                        button.Location = new Point((tile.X * 20) + 40, (tile.Y * 20) + 40);
                    }
                    else
                    {
                        button.Location = new Point(tile.X * 20, tile.Y * 20);
                    }

                    tile.Button = button;
                    this.Controls.Add(button);
                }
            }
        }

        private void CharacterButton_Click(object sender, System.EventArgs e)
        {
            if (Program.ShowCoordinates)
            {
                Tile t = (Tile)((Button)sender).Tag;
                StringBuilder sb = new StringBuilder();
                sb.Append(t.GetType().Name + "  X = " + t.X + " Y=" + t.Y);
                sb.AppendLine(" M:" + Program.Game.MapCreate.ArrMap[t.X, t.Y].GetType().Name);

                if (t is Hero || t is Enemy)
                {
                    for (int i = 1; i <= 4; i++)
                    {
                        Tile vt = null;
                        if (t is Hero) { vt = ((Hero)t).Vision[i]; }
                        if (t is Enemy) { vt = ((Enemy)t).Vision[i]; }
                        string direction = ((Hero.Movement)i).ToString();
                        sb.Append("V:" + direction + "=" + vt.GetType().Name + "  X = " + vt.X + "  Y = " + vt.Y);
                        sb.AppendLine(" M: " + Program.Game.MapCreate.ArrMap[vt.X, vt.Y].GetType().Name);
                    }

                    MessageBox.Show(sb.ToString());
                }
            }

            if (((Button)sender).Tag.GetType().BaseType.Name != "Enemy") { return; }

            Enemy enemy = (Enemy)((Button)sender).Tag;

            if (Program.Game.MapCreate.Hero.CheckRange(enemy))
            {
                Program.Game.MapCreate.Hero.Attack(enemy);
                if (!enemy.IsDead())
                {
                    enemy.Attack(Program.Game.MapCreate.Hero);

                    if (enemy is Hero)
                    {
                        ShowEndGame();
                    }
                }
            }
            else
            {
                Program.MainForm.Output(enemy.GetType().Name + " is out of range");
            }

        }

        public void ShowEndGame()
        {
            MessageBox.Show("You are dead !");

            List<Control> controls = new List<Control>();
            // May not modifiy collection using foreach
            foreach (Control c in this.Controls)
            {
                if (c.Name != "txtOuput" && c.Name != "btnUp" && c.Name != "btnDown" && c.Name != "btnLeft" && c.Name != "btnRight")
                {
                    controls.Add(c);
                }
            }

            txtOuput.Text = String.Empty;
            MessageBuffer.Clear();

            foreach (Control c in controls)
            {
                this.Controls.Remove(c);
            }

            Button newGame = new Button();
            newGame.Text = "New Game";
            newGame.Location = new Point(this.Location.X / 2 - 20, this.Location.Y / 2 - 55);
            newGame.Click += NewGame_Click;
            this.Controls.Add(newGame);
        }

        private void NewGame_Click(object sender, System.EventArgs e)
        {
            this.Controls.Remove((Control)sender);
            NewGame();
        }

        private void btnUp_Click(object sender, System.EventArgs e)
        {
            Character.Movement movement = Program.Game.MapCreate.Hero.ReturnMove(Character.Movement.Up);
            Program.Game.MapCreate.Hero.Move(movement);
            Program.Game.MoveEnemies();
            Program.Game.EnemiesAttack();
            //Program.Game.MovePlayer(Character.Movement.Up);
        }

        private void btnRight_Click(object sender, System.EventArgs e)
        {
            Program.Game.MovePlayer(Character.Movement.Right);
        }

        private void btnDown_Click(object sender, System.EventArgs e)
        {
            Program.Game.MovePlayer(Character.Movement.Down);
        }

        private void btnLeft_Click(object sender, System.EventArgs e)
        {
            Program.Game.MovePlayer(Character.Movement.Left);
        }

        private void btnShowHeroStats_Click(object sender, EventArgs e)
        {
            Output(Program.Game.MapCreate.Hero.ToString());
        }

        private void btnShowEnemyStats_Click(object sender, EventArgs e)
        {
            foreach (Enemy enemy in Program.Game.MapCreate.ArrEnemy)
            {
                Output(enemy.ToString());
            }
        }

        private void btnLoadGame_Click(object sender, EventArgs e)
        {
            Program.Game.Load();
            Program.Game.MapCreate.UpdateVision();
            //Program.Game.MainForm = this;
            RenderMap(Program.Game.MapCreate);
        }

        private void btnSaveGame_Click(object sender, EventArgs e)
        {
            Program.Game.Save();
        }

        private void btnWeapon1_Click(object sender, EventArgs e)
        {
            Output("Bought" + Program.Game.GameShop.DisplayWeapon(0));
            Program.Game.GameShop.Buy(0);
        }

        private void btnWeapon2_Click(object sender, EventArgs e)
        {
            Output("Bought" + Program.Game.GameShop.DisplayWeapon(1));
            Program.Game.GameShop.Buy(1);
        }

        private void btnWeapon3_Click(object sender, EventArgs e)
        {
            Output("Bought" + Program.Game.GameShop.DisplayWeapon(2));
            Program.Game.GameShop.Buy(2);
        }

        private void btnShop_Click(object sender, EventArgs e)
        {
            Output(Program.Game.GameShop.DisplayWeapon(0));
            Output(Program.Game.GameShop.DisplayWeapon(1));
            Output(Program.Game.GameShop.DisplayWeapon(2));
        }

        private void btnShowMapElements_Click(object sender, EventArgs e)
        {
            new frmMapDebug(Program.Game.MapCreate).Show();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (Program.ShowCoordinates)
            {
                btnShowMapElements.Visible = true;
            }
            else
            {
                btnShowMapElements.Visible = false;
            }
        }
    }
}
