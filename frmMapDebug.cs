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
    public partial class frmMapDebug : Form
    {
        public frmMapDebug(Map m)
        {
            InitializeComponent();

            for (int i = 0; i < m.MapWidth; i++)
            {
                for (int j = 0; j < m.MapHeight; j++)
                {
                    if (m.ArrMap[i, j] == null)
                    {
                        txtDebugInfo.Text += "[" + i + "," + j + "] = null" + Environment.NewLine;
                            
                    } else
                    {
                        txtDebugInfo.Text += "[" + i + "," + j + "] = " + m.ArrMap[i, j].GetType() + Environment.NewLine;
                    }
                }
            }
        }
    }
}
