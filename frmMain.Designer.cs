namespace GADE5112POE
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.txtOuput = new System.Windows.Forms.TextBox();
            this.btnShowHeroStats = new System.Windows.Forms.Button();
            this.btnShowEnemyStats = new System.Windows.Forms.Button();
            this.btnLoadGame = new System.Windows.Forms.Button();
            this.btnSaveGame = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnWeapon3 = new System.Windows.Forms.Button();
            this.btnWeapon2 = new System.Windows.Forms.Button();
            this.btnWeapon1 = new System.Windows.Forms.Button();
            this.btnShop = new System.Windows.Forms.Button();
            this.btnShowMapElements = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnUp
            // 
            this.btnUp.Location = new System.Drawing.Point(991, 564);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(44, 38);
            this.btnUp.TabIndex = 0;
            this.btnUp.Text = "Up";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.Location = new System.Drawing.Point(991, 621);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(44, 38);
            this.btnDown.TabIndex = 1;
            this.btnDown.Text = "Down";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnRight
            // 
            this.btnRight.Location = new System.Drawing.Point(1041, 592);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(44, 38);
            this.btnRight.TabIndex = 2;
            this.btnRight.Text = "Right";
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // btnLeft
            // 
            this.btnLeft.Location = new System.Drawing.Point(941, 592);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(44, 38);
            this.btnLeft.TabIndex = 3;
            this.btnLeft.Text = "Left";
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // txtOuput
            // 
            this.txtOuput.BackColor = System.Drawing.Color.Black;
            this.txtOuput.ForeColor = System.Drawing.Color.White;
            this.txtOuput.Location = new System.Drawing.Point(883, 13);
            this.txtOuput.Multiline = true;
            this.txtOuput.Name = "txtOuput";
            this.txtOuput.ReadOnly = true;
            this.txtOuput.Size = new System.Drawing.Size(247, 503);
            this.txtOuput.TabIndex = 4;
            // 
            // btnShowHeroStats
            // 
            this.btnShowHeroStats.Location = new System.Drawing.Point(883, 522);
            this.btnShowHeroStats.Name = "btnShowHeroStats";
            this.btnShowHeroStats.Size = new System.Drawing.Size(72, 23);
            this.btnShowHeroStats.TabIndex = 5;
            this.btnShowHeroStats.Text = "Show Hero Stats";
            this.btnShowHeroStats.UseVisualStyleBackColor = true;
            this.btnShowHeroStats.Click += new System.EventHandler(this.btnShowHeroStats_Click);
            // 
            // btnShowEnemyStats
            // 
            this.btnShowEnemyStats.Location = new System.Drawing.Point(1041, 522);
            this.btnShowEnemyStats.Name = "btnShowEnemyStats";
            this.btnShowEnemyStats.Size = new System.Drawing.Size(89, 23);
            this.btnShowEnemyStats.TabIndex = 6;
            this.btnShowEnemyStats.Text = "Show Enemies";
            this.btnShowEnemyStats.UseVisualStyleBackColor = true;
            this.btnShowEnemyStats.Click += new System.EventHandler(this.btnShowEnemyStats_Click);
            // 
            // btnLoadGame
            // 
            this.btnLoadGame.Location = new System.Drawing.Point(13, 635);
            this.btnLoadGame.Name = "btnLoadGame";
            this.btnLoadGame.Size = new System.Drawing.Size(75, 23);
            this.btnLoadGame.TabIndex = 7;
            this.btnLoadGame.Text = "Load Game";
            this.btnLoadGame.UseVisualStyleBackColor = true;
            this.btnLoadGame.Click += new System.EventHandler(this.btnLoadGame_Click);
            // 
            // btnSaveGame
            // 
            this.btnSaveGame.Location = new System.Drawing.Point(94, 636);
            this.btnSaveGame.Name = "btnSaveGame";
            this.btnSaveGame.Size = new System.Drawing.Size(75, 23);
            this.btnSaveGame.TabIndex = 8;
            this.btnSaveGame.Text = "Save Game";
            this.btnSaveGame.UseVisualStyleBackColor = true;
            this.btnSaveGame.Click += new System.EventHandler(this.btnSaveGame_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnWeapon3);
            this.groupBox1.Controls.Add(this.btnWeapon2);
            this.groupBox1.Controls.Add(this.btnWeapon1);
            this.groupBox1.Location = new System.Drawing.Point(329, 592);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(429, 81);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Shop";
            // 
            // btnWeapon3
            // 
            this.btnWeapon3.Location = new System.Drawing.Point(300, 26);
            this.btnWeapon3.Name = "btnWeapon3";
            this.btnWeapon3.Size = new System.Drawing.Size(108, 42);
            this.btnWeapon3.TabIndex = 2;
            this.btnWeapon3.Text = "Buy Item 3";
            this.btnWeapon3.UseVisualStyleBackColor = true;
            this.btnWeapon3.Click += new System.EventHandler(this.btnWeapon3_Click);
            // 
            // btnWeapon2
            // 
            this.btnWeapon2.Location = new System.Drawing.Point(164, 26);
            this.btnWeapon2.Name = "btnWeapon2";
            this.btnWeapon2.Size = new System.Drawing.Size(105, 42);
            this.btnWeapon2.TabIndex = 1;
            this.btnWeapon2.Text = "Buy Item 2";
            this.btnWeapon2.UseVisualStyleBackColor = true;
            this.btnWeapon2.Click += new System.EventHandler(this.btnWeapon2_Click);
            // 
            // btnWeapon1
            // 
            this.btnWeapon1.Location = new System.Drawing.Point(21, 27);
            this.btnWeapon1.Name = "btnWeapon1";
            this.btnWeapon1.Size = new System.Drawing.Size(101, 40);
            this.btnWeapon1.TabIndex = 0;
            this.btnWeapon1.Text = "Buy Item 1";
            this.btnWeapon1.UseVisualStyleBackColor = true;
            this.btnWeapon1.Click += new System.EventHandler(this.btnWeapon1_Click);
            // 
            // btnShop
            // 
            this.btnShop.Location = new System.Drawing.Point(961, 522);
            this.btnShop.Name = "btnShop";
            this.btnShop.Size = new System.Drawing.Size(72, 23);
            this.btnShop.TabIndex = 10;
            this.btnShop.Text = "Show Shop";
            this.btnShop.UseVisualStyleBackColor = true;
            this.btnShop.Click += new System.EventHandler(this.btnShop_Click);
            // 
            // btnShowMapElements
            // 
            this.btnShowMapElements.Location = new System.Drawing.Point(175, 636);
            this.btnShowMapElements.Name = "btnShowMapElements";
            this.btnShowMapElements.Size = new System.Drawing.Size(87, 23);
            this.btnShowMapElements.TabIndex = 11;
            this.btnShowMapElements.Text = "Map Elements";
            this.btnShowMapElements.UseVisualStyleBackColor = true;
            this.btnShowMapElements.Visible = false;
            this.btnShowMapElements.Click += new System.EventHandler(this.btnShowMapElements_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1142, 674);
            this.Controls.Add(this.btnShowMapElements);
            this.Controls.Add(this.btnShop);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSaveGame);
            this.Controls.Add(this.btnLoadGame);
            this.Controls.Add(this.btnShowEnemyStats);
            this.Controls.Add(this.btnShowHeroStats);
            this.Controls.Add(this.txtOuput);
            this.Controls.Add(this.btnLeft);
            this.Controls.Add(this.btnRight);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnUp);
            this.Name = "frmMain";
            this.Text = "GADE5112POE";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.TextBox txtOuput;
        private System.Windows.Forms.Button btnShowHeroStats;
        private System.Windows.Forms.Button btnShowEnemyStats;
        private System.Windows.Forms.Button btnLoadGame;
        private System.Windows.Forms.Button btnSaveGame;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnWeapon3;
        private System.Windows.Forms.Button btnWeapon2;
        private System.Windows.Forms.Button btnWeapon1;
        private System.Windows.Forms.Button btnShop;
        private System.Windows.Forms.Button btnShowMapElements;
    }
}

