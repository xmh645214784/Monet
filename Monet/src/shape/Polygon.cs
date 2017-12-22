using Monet.src.ui;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monet.src.shape
{
    class Polygon: Shape
    {
        public ArrayList pointArray;
        ResizeButton[] resizeButtons;

        public override object Clone()
        {
            Polygon copy = new Polygon();
            copy.pointArray = (ArrayList)pointArray.Clone();
            return copy;
        }

        public override bool IsSelectMe(Point point)
        {
            int length = pointArray.Count;
            for(int i=0;i<length;i++)
            {
                if(Line.DistanceOfPoint2Line((Point)pointArray[i%length],(Point)pointArray[(i+1)%length],point)<10)
                {
                    return true;
                }
            }
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
