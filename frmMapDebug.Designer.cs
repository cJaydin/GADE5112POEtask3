namespace GADE5112POE
{
    partial class frmMapDebug
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
            this.txtDebugInfo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtDebugInfo
            // 
            this.txtDebugInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDebugInfo.Location = new System.Drawing.Point(0, 0);
            this.txtDebugInfo.Multiline = true;
            this.txtDebugInfo.Name = "txtDebugInfo";
            this.txtDebugInfo.Size = new System.Drawing.Size(800, 450);
            this.txtDebugInfo.TabIndex = 0;
            // 
            // frmMapDebug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtDebugInfo);
            this.Name = "frmMapDebug";
            this.Text = "frmMapDebug";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDebugInfo;
    }
}