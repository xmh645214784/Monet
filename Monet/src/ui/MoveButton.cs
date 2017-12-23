using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Monet.src.shape;

namespace Monet.src.ui
{
    public class MoveButton : MoveableButtonWithDoubleBuffering
    {
        public MoveButton(PictureBox mainView, Shape shape, Point location, Cursor cursor) : base(mainView, shape, location, cursor)
        {
        }
    }
}
