using Monet.src.ui;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monet.src.shape
{
    class Bezier : Shape
    {
        public List<Point> pointArray = new List<Point>();

        List<MoveableButton> adjustButtons = new List<MoveableButton>();

        MoveButton moveButton;

        public Bezier(List<Point> pointArray)
        {
            this.pointArray = pointArray;
        }

        protected Bezier()
        {
        }

        public override object Clone()
        {
            Bezier bezier = new Bezier();
            bezier.pointArray = new List<Point>(pointArray.ToArray());
            return bezier;
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
