using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
namespace Monet
{
    public partial class MainWin : Form
    {
        Tool currentTool;

        ///-------------------------------------------------------------------------------------------------
        /// \fn public MainWin()
        ///
        /// \brief Default constructor
        ///-------------------------------------------------------------------------------------------------

        public MainWin()
        {
            InitializeComponent();
            //some initialize progress
        }

        private void panel_MouseMove(object sender, MouseEventArgs e)
        {
            statusLabelText1.Text = String.Format("({0},{1})pix",e.X.ToString(),e.Y.ToString());
        }

        private void lineButton_Click(object sender, EventArgs e)
        {

        }

        private void MainWin_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
