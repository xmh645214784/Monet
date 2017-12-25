using Monet.src.ui;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monet.src.shape
{
    class BSpline : Shape
    {
        public List<Point> pointArray = new List<Point>();

        List<MoveableButton> adjustButtons=new List<MoveableButton>();

        MoveButton moveButton;

        public  BSpline(List<Point> pointArray)
        {
            this.pointArray = pointArray;
        }

        protected BSpline()
        {
        }

        public override object Clone()
        {
            BSpline bSpline = new BSpline();
            bSpline.pointArray = new List<Point>(pointArray.ToArray());
            return bSpline;
        }

        public override bool IsSelectMe(Point point)
        {
            return false;
        }

        public override void ShowAsNotSelected()
        {
            base.ShowAsNotSelected();
        }

        public override void ShowAsSelected()
        {
            base.ShowAsSelected();
        }
    }
}
