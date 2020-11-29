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
    public partial class dialoguePickupItem : Form
    {
        public dialoguePickupItem(Item item)
        {
            InitializeComponent();

            if (item.GetType()==typeof(Weapon))
            {

            }
        }
    }
}
