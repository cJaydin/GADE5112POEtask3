using System;
using System.Windows.Forms;

namespace GADE5112POE
{
    public partial class dialogueNewOrLoadGame : Form
    {
        public dialogueNewOrLoadGame()
        {
            InitializeComponent();
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnLoadGame_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
