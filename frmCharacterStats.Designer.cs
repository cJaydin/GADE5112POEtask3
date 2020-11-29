namespace GADE5112POE
{
    partial class frmCharacterStats
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
            this.txtCharacterStats = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtCharacterStats
            // 
            this.txtCharacterStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCharacterStats.Location = new System.Drawing.Point(0, 0);
            this.txtCharacterStats.Multiline = true;
            this.txtCharacterStats.Name = "txtCharacterStats";
            this.txtCharacterStats.Size = new System.Drawing.Size(358, 65);
            this.txtCharacterStats.TabIndex = 0;
            // 
            // frmCharacterStats
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 65);
            this.Controls.Add(this.txtCharacterStats);
            this.Name = "frmCharacterStats";
            this.Text = "frmCharacterStats";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCharacterStats;
    }
}