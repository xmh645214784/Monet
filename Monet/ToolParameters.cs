using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Monet
{
    class ToolParameters
    {
        Point[] coords
        {
            set
            {
                System.Diagnostics.Debug.Assert(coords.Length <= 2, "nr of coords out of size");
                coords = value;
            }
            get =>  coords;
        }
        public int width;
        public Color color;

    }
}
