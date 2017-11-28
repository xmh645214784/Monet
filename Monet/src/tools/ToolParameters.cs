using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Monet
{
    sealed class ToolParameters
    {

        internal Point[] coords;

        public ToolParameters()
        {
            coords = new Point[3];
            pen = new Pen(Color.Black);
        }

        internal Pen pen;
    }
}
