namespace GADE5112POE
{
    partial class dialoguePickupItem
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
            this.txtItemDescription = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtItemDescription
            // 
            this.txtItemDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtItemDescription.Location = new System.Drawing.Point(0, 0);
            this.txtItemDescription.Multiline = true;
            this.txtItemDescription.Name = "txtItemDescription";
            this.txtItemDescription.ReadOnly = true;
            this.txtItemDescription.Size = new System.Drawing.Size(470, 144);
            this.txtItemDescription.TabIndex = 0;
            // 
            // dialoguePickupItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 144);
            this.Controls.Add(this.txtItemDescription);
            this.Name = "dialoguePickupItem";
            this.Text = "dialoguePickupItem";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtItemDescription;
    }
}