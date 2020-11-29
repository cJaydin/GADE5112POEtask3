using System.Windows.Forms;

namespace GADE5112POE
{
    public partial class frmCharacterStats : Form
    {
        public frmCharacterStats(Character character)
        {
            InitializeComponent();

            txtCharacterStats.Text = character.ToString();
        }
    }
}
