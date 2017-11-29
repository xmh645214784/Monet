using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Monet
{
    sealed class ActionParameters
    {

        internal Point[] coords;

        public ActionParameters()
        {
            coords = new Point[3];
            pen = new Pen(Color.Black);
        }

        internal Pen pen;

        internal Color color;
        internal Color backgroundColor;
    }
}
