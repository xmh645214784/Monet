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
        internal Point[] Coords
        {
            set
            {
                System.Diagnostics.Debug.Assert(Coords.Length <= 2, "nr of coords out of size");
                Coords = value;
            }
            get => Coords;
        }

        internal Pen Pen { get => Pen; set => Pen = value; }
    }
}
