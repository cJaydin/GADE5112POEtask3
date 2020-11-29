using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GADE5112POE
{
    public partial class frmPlayerStats : Form
    {
        public frmPlayerStats(Hero hero)
        {
            InitializeComponent();

            txtPlayerStats.Text = hero.ToString();
        }
    }
}
