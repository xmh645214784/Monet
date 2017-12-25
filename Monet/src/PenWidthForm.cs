using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monet.src
{
    public partial class PenWidthForm : Form
    {
        public PenWidthForm()
        {
            InitializeComponent();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = string.Format("笔宽为{0}", trackBar1.Value);
        }

        public int RetValue() => trackBar1.Value;

    }
}
