//Line: Alternative
//  LINE_SYSTEM 
//  LINE_DDA 
//  LINE_MIDPOINT 
//  LINE_BRESENHAM
#define LINE_DDA

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Monet
{
    public sealed class Ref<T>
    {
        private readonly Func<T> getter;
        private readonly Action<T> setter;
        public Ref(Func<T> getter, Action<T> setter)
        {
            this.getter = getter;
            this.setter = setter;
        }
        public T Value { get { return getter(); } set { setter(value); } }
    }
    class Common
    {
        ///-------------------------------------------------------------------------------------------------
        /// \fn static void DrawPix(Graphics g,Point p,Pen pen)
        ///
        /// \brief Draw pix. This is the only drawing method what I use in my implementing across all the assignment.
        ///
        /// \param g   The Graphics to process.
        /// \param p   A Point to process.
        /// \param pen The pen.
        ///-------------------------------------------------------------------------------------------------
        public static void  DrawPix(Graphics g,Point p,Pen pen)
        {
            g.DrawRectangle(pen, p.X, p.Y, 1, 1);
        }
        public static void DrawPix(Graphics g, int px,int py, Pen pen)
        {
            g.DrawRectangle(pen, px, py, 1, 1);
        }

        public static Rectangle Rectangle(Point p1,Point p2)
        {
            int width = Math.Abs(p1.X - p2.X);
            int height = Math.Abs(p1.Y - p2.Y);
            Point start = new Point(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y));
            return new Rectangle(start, new Size(width, height));
        }

        public static Point RotatingPoint(Point a,Point midPoint,double angle)
        {
            double radian = angle / 360 * 2 * Math.PI;
            double cos = Math.Cos(radian);
            double sin = Math.Sin(radian);
            double x = (a.X - midPoint.X) * cos - (a.Y - midPoint.Y) * sin + midPoint.X;
            double y = (a.Y - midPoint.Y) * cos + (a.X - midPoint.X) * sin + midPoint.Y;
            return new Point((int)x, (int)y);
        }
    }
}
