﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monet.src.shape
{
    public class Circle : Shape
    {
        public Point startPoint;
        public Point endPoint;
        public Pen pen;

        public override bool IsSelectMe(Point point)
        {
            return Math.Sqrt(Math.Pow(point.X - startPoint.X, 2) + Math.Pow(point.Y - startPoint.Y, 2))
                < 10 ? true:false ;
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