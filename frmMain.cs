using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GADE5112POE
{
    public partial class frmMain : Form, IView
    {
        private GameEngine Game;
        private Queue<string> MessageBuffer = new Queue<string>(36);

        public frmMain()
        {
            InitializeComponent();

            dialogueNewOrLoadGame dlg = new dialogueNewOrLoadGame();
            dlg.ShowDialog();
            if (dlg.DialogResult == DialogResult.OK)
            {
                NewGame();
            }
            else
            {
                Game = new GameEngine();
                Game.Load();
                Game.MapCreate.UpdateVision();
                Game.MainForm = this;
                RenderMap(Game.MapCreate);
            }
        }

        private void NewGame()
        {
            Random random = new Random();
            int itemCount = random.Next(4, 7);

            Game = new GameEngine(this, 20, 43, 20, 30, 10, itemCount);
            RenderMap(Game.MapCreate);
            Application.DoEvents();
            this.Refresh();
            this.Focus();
        }

        public void Output(string text)
        {
            if (MessageBuffer.Count > 35)
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
            for (int i = 0; i < map.MapWidth; i++)
            {
                for (int j = 0; j < map.MapHeight; j++)
                {
                    Tile tile = map.ArrMap[i, j];

                    if (tile == null || tile.GetType() == typeof(EmptyTile)) { continue; }

                    Button button = new Button();
                    button.Tag = tile;
                    button.Click += CharacterButton_Click;
                    button.Text = tile.Symbol.ToString();
                    button.Width = 20;
                    button.Height = 20;
                    button.Location = new Point(tile.X * 20, tile.Y * 20);
                    tile.Button = button;
                    this.Controls.Add(button);
                }
            }
        }

        private void CharacterButton_Click(object sender, System.EventArgs e)
        {
            if (((Button)sender).Tag.GetType().BaseType.Name != "Enemy") { return; }

            Enemy enemy = (Enemy)((Button)sender).Tag;

            Game.Attack(enemy);

            if (enemy.Hp > 0)
            {
                Game.AttackBack(enemy);
            }

            //frmCharacterStats frm = new frmCharacterStats( (Character)(((Button)sender).Tag) );
            //frm.Show();
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
            newGame.Location = new Point(this.Location.X / 2, this.Location.Y / 2 - 40);
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
            Game.MovePlayer(Character.Movement.Up);
        }

        private void btnRight_Click(object sender, System.EventArgs e)
        {
            Game.MovePlayer(Character.Movement.Right);
        }

        private void btnDown_Click(object sender, System.EventArgs e)
        {
            Game.MovePlayer(Character.Movement.Down);
        }

        private void btnLeft_Click(object sender, System.EventArgs e)
        {
            Game.MovePlayer(Character.Movement.Left);
        }

        private void btnShowHeroStats_Click(object sender, EventArgs e)
        {
            Output(Game.MapCreate.Hero.ToString());
        }

        private void btnShowEnemyStats_Click(object sender, EventArgs e)
        {
            foreach (Enemy enemy in Game.MapCreate.ArrEnemy)
            {
                Output(enemy.ToString());
            }
        }

        private void btnLoadGame_Click(object sender, EventArgs e)
        {
            Game.Load();
            Game.MapCreate.UpdateVision();
            Game.MainForm = this;
            RenderMap(Game.MapCreate);
        }

        private void btnSaveGame_Click(object sender, EventArgs e)
        {
            Game.Save();
        }
    }
}
