using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monet.src.shape
{
    public sealed class Ellipse : Shape
    {
        public Point a;
        public Point b;
        public Pen pen;

        public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override bool IsSelectMe(Point point)
        {
            throw new NotImplementedException();
        }

        public override void ShowAsNotSelected()
        {
            throw new NotImplementedException();
        }

        public override void ShowAsSelected()
        {
            throw new NotImplementedException();
        }
    }
}
